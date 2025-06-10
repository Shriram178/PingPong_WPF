using System.Collections.ObjectModel;
using BounceBall.Command;
using BounceBall.Manager;
using BounceBall.Models;
using Caliburn.Micro;

namespace BounceBall.ViewModels
{
    public class LeaderboardViewModel : Screen
    {
        private readonly GameDataManager _gameDataManager;
        private readonly IEventAggregator _events;

        public ObservableCollection<LeaderboardEntry> LeaderboardEntries { get; set; } = new();

        public List<string> SortColumns { get; } = new() { "BestScore", "AverageScore", "TotalGamesPlayed" };
        public List<string> SortOrders { get; } = new() { "ASC", "DESC" };

        private string _selectedSortColumn = "BestScore";
        public string SelectedSortColumn
        {
            get => _selectedSortColumn;
            set
            {
                _selectedSortColumn = value;
                // not using discard operator here just for refernce (shows warning)
                RefreshLeaderboardAsync();
            }
        }

        private string _selectedSortOrder = "DESC";
        public string SelectedSortOrder
        {
            get => _selectedSortOrder;
            set { _selectedSortOrder = value; RefreshLeaderboardAsync(); }
        }

        private int _pageIndex = 0;
        public int PageIndex
        {
            get => _pageIndex;
            set { _pageIndex = value; NotifyOfPropertyChange(); }
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set { _pageSize = value; RefreshLeaderboardAsync(); }
        }

        private string _filterText = string.Empty;
        public string FilterText
        {
            get => _filterText;
            set { _filterText = value; RefreshLeaderboardAsync(); }
        }

        public LeaderboardViewModel(IEventAggregator events, GameDataManager gameDataManager)
        {
            _events = events;
            _gameDataManager = gameDataManager;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await RefreshLeaderboardAsync();
            await base.OnActivateAsync(cancellationToken);
        }

        public async Task RefreshLeaderboardAsync()
        {
            var allEntries = await _gameDataManager.GetLeaderboardsAsync(PageIndex, PageSize, SelectedSortColumn, SelectedSortOrder);
            var filtered = string.IsNullOrWhiteSpace(FilterText)
                ? allEntries
                : allEntries.Where(e => e.Username.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList();

            LeaderboardEntries.Clear();
            foreach (var entry in filtered)
                LeaderboardEntries.Add(entry);

            NotifyOfPropertyChange(nameof(LeaderboardEntries));
        }

        public async Task NextPage()
        {
            PageIndex++;
            await RefreshLeaderboardAsync();
        }

        public async Task PrevPage()
        {
            if (PageIndex > 0)
            {
                PageIndex--;
                await RefreshLeaderboardAsync();
            }
        }

        public async Task GoBack() => await _events.PublishOnUIThreadAsync(new NavigateToMenuMessage());
    }

}
