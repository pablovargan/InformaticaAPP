using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using InformaticaAPP.ViewModels;

using System.Net;
using System.Linq;
//Debemos añadirla desde agregar referencia en el proyecto
using System.Xml.Linq;


namespace InformaticaAPP
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<DlsiVM>();
            this.Posts = new ObservableCollection<PostVM>();
        }

        /// <summary>
        /// Colección para objetos ItemViewModel.
        /// </summary>
        public ObservableCollection<DlsiVM> Items { get; private set; }
        public ObservableCollection<PostVM> Posts { get; private set; }

        private string _rssUrl_DLSI = Constantes.RssURL_DLSI;
        private string _postUrl_Blog = Constantes.RssURL_Blog;
        /// <summary>
        /// Url de la fuente RSS de la que se cargan datos
        /// </summary>
        /// <returns></returns>
        public string RssUrl
        {
            get
            {
                return _rssUrl_DLSI;
            }
            set
            {
                if (value != _rssUrl_DLSI)
                {
                    _rssUrl_DLSI = value;
                    NotifyPropertyChanged("RssUrl");
                }
            }
        }

        public string PostUrl
        {
            get
            {
                return _postUrl_Blog;
            }
            set
            {
                if (value != _postUrl_Blog)
                {
                    _postUrl_Blog = value;
                    NotifyPropertyChanged("PostUrl");
                }
            }
        }

        private bool _isDataLoaded;
        /// <summary>
        /// Determina si se han cargado los datos desde la fuente RSS
        /// </summary>
        public bool IsDataLoaded
        {
            get { return _isDataLoaded; }
            set 
            { 
                _isDataLoaded = value;
                NotifyPropertyChanged("IsDataLoaded");    
            }
        }

        /// <summary>
        /// Descarga y añade los elementos de DlsiViewModel a partir de la fuente XML
        /// </summary>
        public void LoadDataDLSI()
        {
            try
            {
                WebClient wc = new WebClient();
                //Muestra los acentos correctamente
                wc.Encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                //Llama al metodo para realizar la select de todos los items del RSS
                wc.DownloadStringCompleted += wc_DownloadDlsiXMLCompleted;
                wc.DownloadStringAsync(new Uri(this._rssUrl_DLSI));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected Exception", MessageBoxButton.OK);
            }
        }

        public void LoadDataBLOG()
        {
            try
            {
                WebClient wc = new WebClient();
                //Muestra los acentos correctamente
                wc.Encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                //Llama al metodo para realizar la select de todos los items del RSS
                wc.DownloadStringCompleted += wc_DownloadBlogXMLCompleted;
                wc.DownloadStringAsync(new Uri(this._postUrl_Blog));
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message, "Unexpected Exception", MessageBoxButton.OK);
            }
        }

        //Realiza la select de todos los items, y coloca en cada item con sus propiedades del DlsiVM
        private void wc_DownloadDlsiXMLCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                {
                    XDocument postRSS_Dlsi = XDocument.Parse(e.Result, LoadOptions.None);
                    var postCollection = from itemDlsi in postRSS_Dlsi.Descendants("item")
                                         select new DlsiVM
                                         {
                                            Titulo = itemDlsi.Element("title").Value,
                                            LinkURL = itemDlsi.Element("link").Value,
                                            Descripcion = itemDlsi.Element("description").Value,
                                            Fecha = itemDlsi.Element("pubDate").Value
                                         };
                    //Vuelvo lo recogido en los items
                    //TODO: PARSEAR PARA ASIGNATURAS, PRIMERO-SEGUNDO-TERCERO-TODAS
                    this.Items = new ObservableCollection<DlsiVM>(postCollection);

                    this.IsDataLoaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected Exception", MessageBoxButton.OK);
            }
        }

        private void wc_DownloadBlogXMLCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                {
                    XDocument postRSS_Blog = XDocument.Parse(e.Result, LoadOptions.None);
                    var postCollection = from itemBlog in postRSS_Blog.Descendants("item")
                                         select new PostVM
                                         {
                                             Titulo = itemBlog.Element("title").Value,
                                             LinkURL = itemBlog.Element("link").Value,
                                             //Autor = itemBlog.Element("dc:creator").Value
                                             Fecha = DateTime.Parse(itemBlog.Element("pubDate").Value),
                                             Descripcion = itemBlog.Element("description").Value
                                         };
                    //Vuelvo lo recogido en los items
                    //TODO: PARSEAR PARA ASIGNATURAS, PRIMERO-SEGUNDO-TERCERO-TODAS
                    this.Posts = new ObservableCollection<PostVM>(postCollection);

                    this.IsDataLoaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected Exception", MessageBoxButton.OK);
            }
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