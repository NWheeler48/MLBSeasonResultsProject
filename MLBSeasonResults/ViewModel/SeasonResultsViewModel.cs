using MLBSeasonResults.Helpers;
using MLBSeasonResults.Model;
using MLBSeasonResults.Model.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MLBSeasonResults.ViewModel
{
    public class SeasonResultsViewModel: INotifyPropertyChanged
    {
        #region Fields
        private SeasonResultsDataModel _model;
        #endregion

        #region Properties

        // Observable collection of the American League teams grouped by there division (East, Centeral, West)
        private ObservableCollection<List<TeamSeasonResults>> _alSeasonResults = new ObservableCollection<List<TeamSeasonResults>>();
        public ObservableCollection<List<TeamSeasonResults>> ALSeasonResults
        {
            get { return _alSeasonResults; }
            set
            {
                if (value != null)
                {
                    _alSeasonResults = value;
                    OnPropertyChanged("ALSeasonResults");
                }
            }
        }

        // Observable collection of the National League teams grouped by there division (East, Centeral, West)
        private ObservableCollection<List<TeamSeasonResults>> _nlSeasonResults = new ObservableCollection<List<TeamSeasonResults>>();
        public ObservableCollection<List<TeamSeasonResults>> NLSeasonResults
        {
            get { return _nlSeasonResults; }
            set
            {
                if (value != null)
                {
                    _nlSeasonResults = value;
                    OnPropertyChanged("NLSeasonResults");
                }
            }
        }
        #endregion

        #region Constructors
        public SeasonResultsViewModel(SeasonResultsDataModel model)
        {
            _model = model;
        }
        #endregion

        #region Public Methods
        public void InitializeViewModel()
        {
            var leagueResults = _model.GetTeamsSeasonResultsList();

            // The first step is to group the results by the leauge AL and NL
            var leagueGrouping = (from result in leagueResults group result by result.league);

            // Loop through the resulting collection, if there is an erronous leauge then it will be ignored.
            foreach (var league in leagueGrouping)
            {
                if (league.Key == "AL")
                {
                    var alDivisionGrouping = (from result in league group result by result.division into g orderby g.Key select new { Items = g});

                    foreach (var division in alDivisionGrouping)
                    {
                        ALSeasonResults.Add(new List<TeamSeasonResults>(division.Items));
                    }
                }
                if (league.Key == "NL")
                {
                    var nlDivisionGrouping = (from result in league group result by result.division into g orderby g.Key select new { Items = g });

                    foreach (var division in nlDivisionGrouping)
                    {
                        NLSeasonResults.Add(new List<TeamSeasonResults>(division.Items));
                    }
                }
            }



            //var group = (from result in allResults group result by result.division into g orderby g.Key select new { Name = g.Key, Items = g });
            //foreach (var division in group)
            //{
            //    GroupedList list = new GroupedList();
            //    list.Key = division.Name;

            //    foreach(var item in division.Items)
            //    {
            //        list.Add(item);
            //    }

            //    ALSeasonResults.Add(list);
            //}
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

    }
}
