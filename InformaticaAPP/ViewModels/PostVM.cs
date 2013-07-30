using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformaticaAPP.ViewModels
{
    public class PostVM : INotifyPropertyChanged
    {
        private string _titulo;
        public string Titulo
        {
            get
            {
                return _titulo;
            }
            set
            {
                if (_titulo != value)
                {
                    _titulo = value;
                    NotifyPropertyChanged("Titulo");
                }
            }
        }

        private string _linkURL;
        public string LinkURL
        {
            get
            {
                return _linkURL;
            }
            set
            {
                if (value != _linkURL)
                {
                    _linkURL = value;
                    NotifyPropertyChanged("LinkURL");
                }
            }
        }

        private string _autor;
        public string Autor
        {
            get
            {
                return _autor;
            }
            set
            {
                if (_autor != value)
                {
                    _autor = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }

        private DateTime _fecha;
        public DateTime Fecha
        {
            get
            {
                return _fecha;
            }
            set
            {
                if (value != _fecha)
                {
                    _fecha = value;
                    NotifyPropertyChanged("Fecha");
                }
            }
        }

        private string _descripcion;
        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                if (value != _descripcion)
                {
                    _descripcion = value;
                    NotifyPropertyChanged("Descripcion");
                }
            }
        }

        private string parsearDescripcion(string valor) 
        {
            // Obtiene el accent color del sistema
            Color accentColor = (Color)Application.Current.Resources["PhoneAccentColor"];
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
