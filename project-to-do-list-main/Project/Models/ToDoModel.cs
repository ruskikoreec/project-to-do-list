using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
   
    internal class ToDoModel : INotifyPropertyChanged

    {
   
        private string _text;
        private bool _isDone;
        private DateTime _deadline = DateTime.Now;
        private string _importance;



        public bool IsDone
        {
            get { return _isDone; } 
            set
            {
                if (_isDone == value)
                    return;
                _isDone = value;
                OnPropertyChanged("IsDone");
            }
        }
        public string Importance
        {
            get { return _importance; }
            set
            {
                if (_importance == value)
                    return;
                _importance = value;
                OnPropertyChanged("Importance");
            }
        }
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                    return;
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        public DateTime Deadline
        {
            get { return _deadline; }
            set
            {
                if (_deadline == value)
                    return;
                _deadline = value;
                OnPropertyChanged("Deadline");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName ="")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
