using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace InformaticaAPP
{
    public class DlsiVM : INotifyPropertyChanged
    {
        
        private string _titulo;
        /// <summary>
        /// DlsiVM property property; Esta propiedad representa las siglas de las notas
        /// </summary>
        /// <returns></returns>
        public string Titulo
        {
            get
            {
                return _titulo;
            }
            set
            {
                if (value != _titulo)
                {
                    _titulo = parseadoSiglas(value);
                    NotifyPropertyChanged("Titulo");
                }
            }
        }
        //Parsea las siglas para que se muestre unicamente las siglas
        private string parseadoSiglas(string valor)
        {
            //Entre [ ]
            string aux = valor;
            int first = aux.IndexOf("[");
            int last = aux.LastIndexOf("]");

            string parseado = aux.Substring(first + 1, last - (first + 1));
            return parseado;
        }

        private string _descripcion;
        /// <summary>
        /// DlsiVM property property; Esta propiedad representa la descripción de la nota
        /// </summary>
        /// <returns></returns>
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
                    _descripcion = parseadoDescripcion(value);
                    NotifyPropertyChanged("Descripcion");
                }
            }
        }
        //Parsea la descripcion para que se muestre unicamente sin las fechas
        private string parseadoDescripcion(string valor)
        {
            //Despues de [ y hasta (
            string aux = valor;
            int first = aux.IndexOf("]");
            int last = aux.LastIndexOf("(");

            string parseado = aux.Substring(first + 2, last - (first + 2));
            return parseado;
        }

        private string _fecha;
        /// <summary>
        /// DlsiVM property; Esta propiedad representa la fecha de publicación de la nota
        /// </summary>
        /// <returns></returns>
        public string Fecha
        {
            get
            {
                return _fecha;
            }
            set
            {
                if (value != _fecha)
                {
                    _fecha = parseadoFecha(value);
                    NotifyPropertyChanged("Fecha");
                }
            }
        }
        //Parsea la descripcion para que se muestre unicamente la nota sin el tiempo de publicacion
        private string parseadoFecha(string valor)
        {
            //Hasta que encuentre un 0 --> ej 08:00
            string aux = valor;
            int last = aux.LastIndexOf("0");
            //PARCHE POWER!!
            string parseado = aux.Substring(0, last - 7);
            return parseado;
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

        private string _linkUrl;
        /// <summary>
        /// DlsiVM property; Esta propiedad representa el link donde esta publicada la nota
        /// </summary>
        /// <returns></returns>
        public string LinkURL
        {
            get
            {
                return _linkUrl;
            }
            set
            {
                if (value != _fecha)
                {
                    _linkUrl = value;
                    NotifyPropertyChanged("LinkURL");
                }    
            } 
        }
    }
}