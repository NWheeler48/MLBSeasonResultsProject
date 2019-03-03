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
    /// <summary>
    /// Class: SeasonResultsViewModel
    /// Purpose: View Model class for the season results application. This class takes the raw data from the model and organizes it into American League, National League and into those leagues
    /// respective divisions.
    /// </summary>
    public class SeasonResultsViewModel
    {
        #region Fields
        private SeasonResultsDataModel _model;
        #endregion

        #region Properties

        // Observable collection of the American League teams grouped by there division (East, Centeral, West)
        private ObservableCollection<GroupedByDivisionList> _alSeasonResults = new ObservableCollection<GroupedByDivisionList>();
        public ObservableCollection<GroupedByDivisionList> ALSeasonResults
        {
            get { return _alSeasonResults; }
            set
            {
                if (value != null)
                {
                    _alSeasonResults = value;
                }
            }
        }

        // Observable collection of the National League teams grouped by there division (East, Centeral, West)
        private ObservableCollection<GroupedByDivisionList> _nlSeasonResults = new ObservableCollection<GroupedByDivisionList>();
        public ObservableCollection<GroupedByDivisionList> NLSeasonResults
        {
            get { return _nlSeasonResults; }
            set
            {
                if (value != null)
                {
                    _nlSeasonResults = value;
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

        /// <summary>
        /// Intialize the view model for the application, this method will take the list of data from the model and 
        /// seperated it into leagues then from there it will populate the two collections with the leagues division results.
        /// </summary>
        public void InitializeViewModel()
        {
            var leagueResults = _model.GetTeamsSeasonResultsList();

            // The first step is to group the results by the leauge AL and NL
            var leagueGrouping = (from result in leagueResults group result by result.league);

            // Loop through the resulting collection, if there is an erronous league then it will be ignored.
            foreach (var league in leagueGrouping)
            {
                // Add the list of teams in each division to there respective collections.
                if (league.Key == "AL")
                {
                    var alDivisionGrouping = (from result in league group result by result.division into g orderby g.Key select new { Items = g, Division = g.Key});

                    foreach (var division in alDivisionGrouping)
                    {
                        GroupedByDivisionList list = new GroupedByDivisionList();
                        list.Division = division.Division;
                        foreach(var item in division.Items)
                        {
                            list.Add(item);
                        }
                        list.Division = division.Division;
                        ALSeasonResults.Add(list);
                    }
                }
                if (league.Key == "NL")
                {
                    var nlDivisionGrouping = (from result in league group result by result.division into g orderby g.Key select new { Items = g, Division = g.Key});

                    foreach (var division in nlDivisionGrouping)
                    {
                        GroupedByDivisionList list = new GroupedByDivisionList();
                        list.Division = division.Division;
                        foreach (var item in division.Items)
                        {
                            list.Add(item);
                        }
                        NLSeasonResults.Add(list);
                    }
                }
            }
        }
        #endregion
    }
}
