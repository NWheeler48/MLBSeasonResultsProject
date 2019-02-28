using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLBSeasonResults.Model.Utility
{
    public class TeamSeasonResults : INotifyPropertyChanged
    {
        #region Fields
        #endregion

        #region Properties
        private string _teamName;
        public string TeamName
        {
            get
            {
                return _teamName;
            }
            set
            {
                if (value != null)
                {
                    _teamName = value;
                    OnPropertyChanged("TeamName");
                }
            }
        }

        private string _wins;
        public string Wins
        {
            get { return _wins; }
            set
            {
                if (value != null)
                {
                    _wins = value;
                    OnPropertyChanged("Wins");
                }
            }
        }

        private string _losses;
        public string Losses
        {
            get { return _losses; }
            set
            {
                if (value != null)
                {
                    _losses = value;
                    OnPropertyChanged("Losses");
                }
            }
        }
            
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
