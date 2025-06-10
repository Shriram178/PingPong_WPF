using System.Collections.ObjectModel;
using BounceBall.Command;
using BounceBall.Manager;
using BounceBall.Models;
using Caliburn.Micro;

namespace BounceBall.ViewModels
{
    public class ProfileViewModel : Screen
    {
        public ObservableCollection<GameData> GameHistory { get; set; } = new ObservableCollection<GameData>();
        public BindableCollection<string> SortColumns { get; } = new BindableCollection<string> { "Score", "PlayedAt", "Duration" };
        public BindableCollection<string> SortOrders { get; } = new BindableCollection<string> { "ASC", "DESC" };

        private string _selectedSortColumn = "PlayedAt";
        public string SelectedSortColumn
        {
            get => _selectedSortColumn;
            set
            {
                Set(ref _selectedSortColumn, value);
                _ = LoadGameHistoryAsync();
            }
        }

        private string _selectedSortOrder = "DESC";
        public string SelectedSortOrder
        {
            get => _selectedSortOrder;
            set
            {
                Set(ref _selectedSortOrder, value);
                _ = LoadGameHistoryAsync();
            }
        }

        private int _pageIndex = 0;
        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                Set(ref _pageIndex, value);
                _ = LoadGameHistoryAsync();
            }
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                Set(ref _pageSize, value);
                _ = LoadGameHistoryAsync();
            }
        }

        private string _filterText;
        public string FilterText
        {
            get => _filterText;
            set
            {
                Set(ref _filterText, value);
                _ = LoadGameHistoryAsync();
            }
        }

        public User CurrentUser { get; }

        public GameDataManager _gameDataManager { get; }

        private GameData _highScore;

        public GameData HighScore
        {
            get => _highScore;
            set => Set(ref _highScore, value);
        }

        private readonly IEventAggregator _events;

        public ProfileViewModel(IEventAggregator events, UserManager userManager, GameDataManager gameDataManager)
        {
            _events = events;
            CurrentUser = userManager.CurrentUser;
            _gameDataManager = gameDataManager;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await LoadGameHistoryAsync();

            var highScoreList = await _gameDataManager.GetGameDataAsync(
                pageSize: 1,
                sortColumn: "Score",
                sortOrder: "DESC");

            HighScore = highScoreList.FirstOrDefault();

            await base.OnActivateAsync(cancellationToken);
        }

        public async Task LoadGameHistoryAsync()
        {
            var data = await _gameDataManager.GetGameDataAsync(
                pageIndex: PageIndex,
                pageSize: PageSize,
                sortColumn: SelectedSortColumn,
                sortOrder: SelectedSortOrder);

            var filtered = string.IsNullOrWhiteSpace(FilterText)
                ? data
                : data.Where(d =>
                       d.Score.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                       d.PlayedAt.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                       d.Duration.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList();

            GameHistory.Clear();
            foreach (var item in filtered)
                GameHistory.Add(item);
        }

        public async Task GoBack()
        {
            _events.PublishOnUIThreadAsync(new NavigateToMenuMessage());
        }

        public void NextPage()
        {
            PageIndex++;
        }

        public void PrevPage()
        {
            if (PageIndex > 0)
                PageIndex--;
        }
    }
}
