using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Facebook;
using System.Windows.Controls.Primitives;
using Newtonsoft.Json;
using System.IO;
using mshtml;
using System.Collections.Specialized;
using System.Xml;
using System.Diagnostics;
using System.Windows.Navigation;

using System.Configuration;
using System.Configuration.Assemblies;
using System.Security.Principal;

using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Xml;

using TweetSharp;
using TweetSharp.Model;
using TweetSharp.Serialization;
using Microsoft.CSharp;
using System.Reflection;
using System.Net;
using System.Collections;
using System.Threading;
using System.Web;
using System.ComponentModel;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Plus.v1;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Plus.v1.Data;

using Diamto.Authentication;
using Diamto_Google_plus_sample;
using Google.Apis.Plus.v1.Data;
using Microsoft.Office;
using office = Microsoft.Office.Interop.Word;

using MySql.Data.MySqlClient;
//using Novacode;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// //////////////////////////////////////////////////////////////
        /// // declaring variables...
        /// </summary>
        private List<TabItem> _tabItems;
        private TabItem _tabAdd;

        public Process p1,p2;

        static MainWindow myStaticObject;

        struct twitterUserSearchData
        {
            String profileImageUrl;
            String userId;
            String userName;
        };

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////
        /// </summary> *** variables for encryption...
        public static UInt32 ipId;
        
        public static UInt64 macId;

        public static String sId;

        //////////////////////////////////////////////////////////////////////////////

        Thread searchInParallelThread;//=new Thread();

        public static object buttonSender = null;

        public int tabIndexiiii = -1;

        public static String tabMaxIndex = "0";

        double screenWidth = 0.0f;

        long temp123 = 0;

        //***** Custom FB search class object
        static String fbSessionId = "CAACEdEose0cBAKkxyocuHiY1BC01vcBhZB7b97GEINa6prt9S0VatGw1ZCL0DZCEnswBQ3pqSZBz6lAlsVLeP4cHKm3BqPYfvoDVjaU3S5wiST2SpFrJZCDQk8zLrnPg3yVrR9ZC4v54c1sXpfGTLj806CFTZBHFYLtcoBW7bpQUmZCGsQpREdysPd5bPTX1CrACUQtfDkDGUwIivqigQvZAF";
        //fs contains data of searched result and data related to them
        FacebookSearch fs ;//= new FacebookSearch(fbSessionId);

        //fs contains data of searched result and data related to them
        FacebookSearch fs1 ;//= new FacebookSearch(fbSessionId);

        //fs contains data of searched result and data related to them
        FacebookSearch fs2 ;//= new FacebookSearch(fbSessionId);
        
        //ps contains data of more information regarding the page/user 
        FacebookSearch ps ;//= new FacebookSearch(fbSessionId);

        //ls contains details of user who liked some post 
        FacebookSearch ls;// = new FacebookSearch(fbSessionId);

        String searchTextBoxText = "", searchTextBoxText1 = "";

        DockPanel dPGlobal;

        String MyConnectionString = "Server=localhost;Database=google;Uid=root;charset=utf8;";

        MySqlConnection connection;

        MySqlCommand cmd;

        String reportFileLocation = "";

        //String searchTextBoxTextOSInt = "", searchTextBoxTextOSInt1 = "";

        string consumerKey = "pNpMDTAW5Q0MACi912iQ8BhVy";
        string consumerSecret = "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO";
        string accessToken = "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua";
        string accessTokenSecret = "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI";
        TwitterSearch ts = new TwitterSearch(
            "pNpMDTAW5Q0MACi912iQ8BhVy",
            "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
            "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
            "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

        TwitterSearch gs = new TwitterSearch(
            "pNpMDTAW5Q0MACi912iQ8BhVy",
            "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
            "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
            "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");
        
        /// <summary>
        /// //////////////////////////////////////////////////////////////
        /// </summary>

        public class Person123
        {
            public String Name { get; set; }
        }//class person...


        public class tweetToReportItem
        {
            public String userName { get; set; }
            public String screenName { get; set; }
            public String dateTime { get; set; }
            public double v { get; set; }
            public double a { get; set; }
            public String tweet { get; set; }
        }//class person...


        public class osintItem : INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            private String _Header;
            public String Header
            {
                get { return _Header; }
                set
                {
                    _Header = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Header"));
                }
            }
            
            /////////////////////////////////////////////////////////////////////////////////
            // paging variables...
            // for more option visibility...
           
            private bool _googleActivitiesMoreOptionVisibility;
            public bool googleActivitiesMoreOptionVisibility
            {
                get { return _googleActivitiesMoreOptionVisibility; }
                set
                {
                    _googleActivitiesMoreOptionVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleActivitiesMoreOptionVisibility"));
                }
            }

            /////////////////////////////////////////////////////////////////////////////////


            /////////////////////////////////////////////////////////////////////////////////

            private bool _headerCloseIconVisibility;
            public bool headerCloseIconVisibility
            {
                get { return _headerCloseIconVisibility; }
                set
                {
                    _headerCloseIconVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("headerCloseIconVisibility"));
                }
            }

            private String _mySearch1;
            public String mySearch1
            {
                get { return _mySearch1; }
                set
                {
                    _mySearch1 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("mySearch1"));
                }
            }

            private String _lastSearch1;
            public String lastSearch1
            {
                get { return _lastSearch1; }
                set
                {
                    _lastSearch1 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("lastSearch1"));
                }
            }


            public String type { get; set; }
            public String tab_number { get; set; }
            
            public bool sADivVisbility { get; set; }

            public static readonly DependencyProperty BindableSourceProperty = DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(osintItem), new UIPropertyMetadata(null, OnBindableSourceChanged));

            public static string GetBindableSource(DependencyObject obj)
            {
                return (string)obj.GetValue(BindableSourceProperty);
            }

            public static void SetBindableSource(DependencyObject obj, string value)
            {
                obj.SetValue(BindableSourceProperty, value);
            }

            private static void OnBindableSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                var browser = o as WebBrowser;
                if (browser == null)
                    return;

                var uri = (string)e.NewValue;

                try
                {
                    browser.Source = !string.IsNullOrEmpty(uri) ? new Uri(uri) : null;
                }
                catch (ObjectDisposedException) { }
            }

            private string _sourceCodeSenti;
            public string sourceCodeSenti
            {
                get { return _sourceCodeSenti; }
                set
                {
                    if (_sourceCodeSenti != value)
                    {
                        _sourceCodeSenti = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("sourceCodeSenti"));

                    }
                }
            }


            public osintItem()
            {
                //People = new ObservableCollection<Person>();
            }
        }//class osintItem...

        public class careSentimentItem : INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            public String Header { get; set; }

            private bool _headerCloseIconVisibility;
            public bool headerCloseIconVisibility
            {
                get { return _headerCloseIconVisibility; }
                set
                {
                    _headerCloseIconVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("headerCloseIconVisibility"));
                }
            }

            private String _mySearch;
            public String mySearch
            {
                get { return _mySearch; }
                set
                {
                    _mySearch = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("mySearch"));
                }
            }

            public String type { get; set; }
            public String tab_number { get; set; }

            public bool sADivVisbility { get; set; }

            public static readonly DependencyProperty BindableSourceProperty = DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(osintItem), new UIPropertyMetadata(null, OnBindableSourceChanged));

            public static string GetBindableSource(DependencyObject obj)
            {
                return (string)obj.GetValue(BindableSourceProperty);
            }

            public static void SetBindableSource(DependencyObject obj, string value)
            {
                obj.SetValue(BindableSourceProperty, value);
            }

            private static void OnBindableSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                var browser = o as WebBrowser;
                if (browser == null)
                    return;

                var uri = (string)e.NewValue;

                try
                {
                    browser.Source = !string.IsNullOrEmpty(uri) ? new Uri(uri) : null;
                }
                catch (ObjectDisposedException) { }
            }

            private string _sourceCodeSenti;

            public string sourceCodeSenti
            {
                get { return _sourceCodeSenti; }
                set
                {
                    if (_sourceCodeSenti != value)
                    {
                        _sourceCodeSenti = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("sourceCodeSenti"));

                    }
                }
            }

            public careSentimentItem()
            {
                //People = new ObservableCollection<Person>();
            }
        }//class careSentimentItem...

        public class aTabItem:INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            //public String Header { get; set; }

            public ObservableCollection<CanvasShape> Shapes
            {
                get { return _Shapes; }
                set
                {
                    _Shapes = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Shapes"));
                }
            }
            ObservableCollection<CanvasShape> _Shapes = new ObservableCollection<CanvasShape>();

            private bool _fbUserListVisibility;
            public bool fbUserListVisibility
            {
                get { return _fbUserListVisibility; }
                set
                {
                    if (_fbUserListVisibility != value)
                    {
                        _fbUserListVisibility = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("fbUserListVisibility"));

                    }
                }
            }

            private bool _fbGroupListVisibility;
            public bool fbGroupListVisibility
            {
                get { return _fbGroupListVisibility; }
                set
                {
                    if (_fbGroupListVisibility != value)
                    {
                        _fbGroupListVisibility = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("fbGroupListVisibility"));

                    }
                }
            }

            private bool _fbPageListVisibility;
            public bool fbPageListVisibility
            {
                get { return _fbPageListVisibility; }
                set
                {
                    if (_fbPageListVisibility != value)
                    {
                        _fbPageListVisibility = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("fbPageListVisibility"));

                    }
                }
            }

            private bool _twUserListVisibility;
            public bool twUserListVisibility
            {
                get { return _twUserListVisibility; }
                set
                {
                    if (_twUserListVisibility != value)
                    {
                        _twUserListVisibility = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("twUserListVisibility"));

                    }
                }
            }

            private bool _twTweetListVisibility;
            public bool twTweetListVisibility
            {
                get { return _twTweetListVisibility; }
                set
                {
                    if (_twTweetListVisibility != value)
                    {
                        _twTweetListVisibility = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("twTweetListVisibility"));

                    }
                }
            }

            private bool _gUserListVisibility;
            public bool gUserListVisibility
            {
                get { return _gUserListVisibility; }
                set
                {
                    if (_gUserListVisibility != value)
                    {
                        _gUserListVisibility = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("gUserListVisibility"));

                    }
                }
            }

            private bool _gActivityListVisibility;
            public bool gActivityListVisibility
            {
                get { return _gActivityListVisibility; }
                set
                {
                    if (_gActivityListVisibility != value)
                    {
                        _gActivityListVisibility = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("gActivityListVisibility"));

                    }
                }
            }

            private String _Header;
            public String Header
            {
                get { return _Header; }
                set
                {
                    _Header = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Header"));
                }
            }

            private String _tab_number;
            public String tab_number
            {
                get { return _tab_number; }
                set
                {
                    _tab_number = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("tab_number"));
                }
            }

            private string _fbUserProfileLink;
            public string fbUserProfileLink
            {
                get { return _fbUserProfileLink; }
                set
                {
                    if (_fbUserProfileLink != value)
                    {
                        _fbUserProfileLink = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("fbUserProfileLink"));

                    }
                }
            }

            private bool _fbUserProfileLinkVisibility;
            public bool fbUserProfileLinkVisibility
            {
                get { return _fbUserProfileLinkVisibility; }
                set
                {
                    if (_fbUserProfileLinkVisibility != value)
                    {
                        _fbUserProfileLinkVisibility = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("fbUserProfileLinkVisibility"));

                    }
                }
            }

            private String _HeaderImgSrc;
            public String HeaderImgSrc
            {
                get { return _HeaderImgSrc; }
                set
                {
                    _HeaderImgSrc = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("HeaderImgSrc"));
                }
            }

            private bool _headerCloseIconVisibility;
            public bool headerCloseIconVisibility
            {
                get { return _headerCloseIconVisibility; }
                set
                {
                    _headerCloseIconVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("headerCloseIconVisibility"));
                }
            }

            private String _mySearch;
            public String mySearch
            {
                get { return _mySearch; }
                set
                {
                    _mySearch = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("mySearch"));
                }
            }

            private String _lastSearch;
            public String lastSearch
            {
                get { return _lastSearch; }
                set
                {
                    _lastSearch= value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("lastSearch"));
                }
            }

            public String type { get; set; }
            //public String tab_number { get; set; }
            public bool twitterUserDivVisbility { get; set; }
            public bool searchUserDivVisbility { get; set; }

            //////////////////////////////////////////////////////////////////
            /// web browser variables...
            /// 
            
            //////////////////////////////////////////////////////////////////////////////////////
            // google variables...
            private bool _googleUserLoadingImageVisbility;
            public bool googleUserLoadingImageVisbility
            {
                get { return _googleUserLoadingImageVisbility; }
                set
                {
                    _googleUserLoadingImageVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleUserLoadingImageVisbility"));
                }
            }

            private bool _googleUserProfileActivitiesListVisbility;
            public bool googleUserProfileActivitiesListVisbility
            {
                get { return _googleUserProfileActivitiesListVisbility; }
                set
                {
                    _googleUserProfileActivitiesListVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleUserProfileActivitiesListVisbility"));
                }
            }

            public bool googleUserDivVisbility { get; set; }

            private bool _googleActivitiesLoadingImageVisbility;
            public bool googleActivitiesLoadingImageVisbility
            {
                get { return _googleActivitiesLoadingImageVisbility; }
                set
                {
                    _googleActivitiesLoadingImageVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleActivitiesLoadingImageVisbility"));
                }
            }

            public ObservableCollection<googleUserListItems> googleUserListCollections
            {
                get { return _googleUserListCollections; }
                set
                {
                    _googleUserListCollections = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleUserListCollections"));
                }
            }
            ObservableCollection<googleUserListItems> _googleUserListCollections = new ObservableCollection<googleUserListItems>();

            public ObservableCollection<twitterTweetListItems> googleActivitiesListCollections
            {
                get { return _googleActivitiesListCollections; }
                set
                {
                    _googleActivitiesListCollections = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleActivitiesListCollections"));
                }
            }
            ObservableCollection<twitterTweetListItems> _googleActivitiesListCollections = new ObservableCollection<twitterTweetListItems>();

            private String _aboutMe;
            public String aboutMe
            {
                get { return _aboutMe; }
                set
                {
                    _aboutMe = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("aboutMe"));
                }
            }

            private String _Birthday;
            public String Birthday
            {
                get { return _Birthday; }
                set
                {
                    _Birthday = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Birthday"));
                }
            }

            private String _BraggingRights;
            public String BraggingRights
            {
                get { return _BraggingRights; }
                set
                {
                    _BraggingRights = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("BraggingRights"));
                }
            }

            private String _CircledByCount;
            public String CircledByCount
            {
                get { return _CircledByCount; }
                set
                {
                    _CircledByCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("CircledByCount"));
                }
            }

            private String _CurrentLocation;
            public String CurrentLocation
            {
                get { return _CurrentLocation; }
                set
                {
                    _CurrentLocation = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("CurrentLocation"));
                }
            }

            private String _GoogleUserDisplayName;
            public String GoogleUserDisplayName
            {
                get { return _GoogleUserDisplayName; }
                set
                {
                    _GoogleUserDisplayName = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("GoogleUserDisplayName"));
                }
            }

            private String _Domain;
            public String Domain
            {
                get { return _Domain; }
                set
                {
                    _Domain = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Domain"));
                }
            }

            private String _ETag;
            public String ETag
            {
                get { return _ETag; }
                set
                {
                    _ETag = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ETag"));
                }
            }

            private String _Gender;
            public String Gender
            {
                get { return _Gender; }
                set
                {
                    _Gender = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Gender"));
                }
            }

            private String _GoogleUserId;
            public String GoogleUserId
            {
                get { return _GoogleUserId; }
                set
                {
                    _GoogleUserId = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("GoogleUserId"));
                }
            }

            private String _GoogleUserImageUrl;
            public String GoogleUserImageUrl
            {
                get { return _GoogleUserImageUrl; }
                set
                {
                    _GoogleUserImageUrl = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("GoogleUserImageUrl"));
                }
            }

            private String _IsPlusUser;
            public String IsPlusUser
            {
                get { return _IsPlusUser; }
                set
                {
                    _IsPlusUser = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("IsPlusUser"));
                }
            }

            private String _Kind;
            public String Kind
            {
                get { return _Kind; }
                set
                {
                    _Kind = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Kind"));
                }
            }

            private String _Language;
            public String Language
            {
                get { return _Language; }
                set
                {
                    _Language = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Language"));
                }
            }

            private String _NickName;
            public String NickName
            {
                get { return _NickName; }
                set
                {
                    _NickName = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("NickName"));
                }
            }

            private String _ObjectType;
            public String ObjectType
            {
                get { return _ObjectType; }
                set
                {
                    _ObjectType = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ObjectType"));
                }
            }

            private String _Occupation;
            public String Occupation
            {
                get { return _Occupation; }
                set
                {
                    _Occupation = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Occupation"));
                }
            }

            private String _PlusOneCount;
            public String PlusOneCount
            {
                get { return _PlusOneCount; }
                set
                {
                    _PlusOneCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("PlusOneCount"));
                }
            }

            private String _RelationshipStatus;
            public String RelationshipStatus
            {
                get { return _RelationshipStatus; }
                set
                {
                    _RelationshipStatus = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("RelationshipStatus"));
                }
            }

            private String _Skills;
            public String Skills
            {
                get { return _Skills; }
                set
                {
                    _Skills = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Skills"));
                }
            }

            private String _Tagline;
            public String Tagline
            {
                get { return _Tagline; }
                set
                {
                    _Tagline = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Tagline"));
                }
            }

            private String _Url;
            public String Url
            {
                get { return _Url; }
                set
                {
                    _Url = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Url"));
                }
            }

            private String _Verified;
            public String Verified
            {
                get { return _Verified; }
                set
                {
                    _Verified = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Verified"));
                }
            }

            public ObservableCollection<googleUserPostsListItems> googleUserTabListCollections
            {
                get { return _googleUserTabListCollections; }
                set
                {
                    _googleUserTabListCollections = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleUserTabListCollections"));
                }
            }
            ObservableCollection<googleUserPostsListItems> _googleUserTabListCollections = new ObservableCollection<googleUserPostsListItems>();

            //////////////////////////////////////////////////////////////////////////////////////

            public bool sADivVisbility { get; set; }
            
            public static readonly DependencyProperty BindableSourceProperty = DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(aTabItem), new UIPropertyMetadata(null, OnBindableSourceChanged));

            public static string GetBindableSource(DependencyObject obj)
            {
                return (string)obj.GetValue(BindableSourceProperty);
            }

            public static void SetBindableSource(DependencyObject obj, string value)
            {
                obj.SetValue(BindableSourceProperty, value);
            }

            private static void OnBindableSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                var browser = o as WebBrowser;
                if (browser == null)
                    return;

                var uri = (string)e.NewValue;

                try
                {
                    browser.Source = !string.IsNullOrEmpty(uri) ? new Uri(uri) : null;
                }
                catch (ObjectDisposedException) { }
            }

            private string _sourceCodeSenti;

            public string sourceCodeSenti
            {
                get { return _sourceCodeSenti; }
                set
                {
                    if (_sourceCodeSenti != value)
                    {
                        _sourceCodeSenti = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("sourceCodeSenti"));
                
                    }
                }
            }

            /////////////////////////////////////////////////////////////////

            //public String MyStrigUrlProperty { get; set; }

            public bool fbUserDivVisbility { get; set; }
            public bool fbGroupDivVisbility { get; set; }
            public bool fbPageDivVisbility { get; set; }

            public bool twitterTweetListDivVisbility { get; set; }

            /////////////////////////////////////////////////////////////////////
            //************* twitter profile info header variables...
            private bool _twitterUserProfileTweetListVisbility;
            public bool twitterUserProfileTweetListVisbility
            {
                get { return _twitterUserProfileTweetListVisbility; }
                set
                {
                    _twitterUserProfileTweetListVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserProfileTweetListVisbility"));
                }
            }

            private String _twitterUserProfileUrl;
            public String twitterUserProfileUrl
            {
                get { return _twitterUserProfileUrl; }
                set
                {
                    _twitterUserProfileUrl = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserProfileUrl"));
                }
            }


            private bool _twitterUserProfileFollowingListVisbility;
            public bool twitterUserProfileFollowingListVisbility
            {
                get { return _twitterUserProfileFollowingListVisbility; }
                set
                {
                    _twitterUserProfileFollowingListVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserProfileFollowingListVisbility"));
                }
            }

            private bool _twitterUserProfileFollowersListVisbility;
            public bool twitterUserProfileFollowersListVisbility
            {
                get { return _twitterUserProfileFollowersListVisbility; }
                set
                {
                    _twitterUserProfileFollowersListVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserProfileFollowersListVisbility"));
                }
            }

            private bool _fbPageProfileLikersListVisbility;
            public bool fbPageProfileLikersListVisbility
            {
                get { return _fbPageProfileLikersListVisbility; }
                set
                {
                    _fbPageProfileLikersListVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageProfileLikersListVisbility"));
                }
            }

            private bool _fbPageProfileStatusesListVisbility;
            public bool fbPageProfileStatusesListVisbility
            {
                get { return _fbPageProfileStatusesListVisbility; }
                set
                {
                    _fbPageProfileStatusesListVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageProfileStatusesListVisbility"));
                }
            }

            private String _twitterUserProfileTweetCountEllipseBg;
            public String twitterUserProfileTweetCountEllipseBg
            {
                get { return _twitterUserProfileTweetCountEllipseBg; }
                set
                {
                    _twitterUserProfileTweetCountEllipseBg = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserProfileTweetCountEllipseBg"));
                }
            }

            private String _twitterUserProfileFollowersCountEllipseBg;
            public String twitterUserProfileFollowersCountEllipseBg
            {
                get { return _twitterUserProfileFollowersCountEllipseBg; }
                set
                {
                    _twitterUserProfileFollowersCountEllipseBg = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserProfileFollowersCountEllipseBg"));
                }
            }

            private String _twitterUserProfileFollowingCountEllipseBg;
            public String twitterUserProfileFollowingCountEllipseBg
            {
                get { return _twitterUserProfileFollowingCountEllipseBg; }
                set
                {
                    _twitterUserProfileFollowingCountEllipseBg = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserProfileFollowingCountEllipseBg"));
                }
            }

            public ObservableCollection<fbUserListItems> fbPageProfileLikersListCollections { get { return _fbPageProfileLikersListCollections; } set { _fbPageProfileLikersListCollections = fbPageProfileLikersListCollections; } }
            ObservableCollection<fbUserListItems> _fbPageProfileLikersListCollections = new ObservableCollection<fbUserListItems>();

            public ObservableCollection<twitterUserListItems> twitterUserProfileFollowersListCollections { get { return _twitterUserProfileFollowersListCollections; } set { _twitterUserProfileFollowersListCollections = twitterUserProfileFollowersListCollections; } }
            ObservableCollection<twitterUserListItems> _twitterUserProfileFollowersListCollections = new ObservableCollection<twitterUserListItems>();

            public ObservableCollection<twitterUserListItems> twitterUserProfileFollowingListCollections { get { return _twitterUserProfileFollowingListCollections; } set { _twitterUserProfileFollowingListCollections = twitterUserProfileFollowingListCollections; } }
            ObservableCollection<twitterUserListItems> _twitterUserProfileFollowingListCollections = new ObservableCollection<twitterUserListItems>();

            /////////////////////////////////////////////////////////////////////

            private bool _twitterUserLoadingImageVisbility;
            public bool twitterUserLoadingImageVisbility
            {
                get { return _twitterUserLoadingImageVisbility; } 
                set 
                {
                    _twitterUserLoadingImageVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserLoadingImageVisbility"));  
                } 
            }

            public bool _twitterTweetLoadingImageVisbility;
            public bool twitterTweetLoadingImageVisbility
            {
                get { return _twitterTweetLoadingImageVisbility; }
                set
                {
                    _twitterTweetLoadingImageVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterTweetLoadingImageVisbility"));
                }
            }

            public bool _fbUserLoadingImageVisbility;
            public bool fbUserLoadingImageVisbility
            {
                get { return _fbUserLoadingImageVisbility; }
                set
                {
                    _fbUserLoadingImageVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbUserLoadingImageVisbility"));
                }
            }

            public bool _fbPageLoadingImageVisbility;
            public bool fbPageLoadingImageVisbility {
                get { return _fbPageLoadingImageVisbility; }
                set
                {
                    _fbPageLoadingImageVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageLoadingImageVisbility"));
                }
            }

            public bool _fbGroupLoadingImageVisbility;
            public bool fbGroupLoadingImageVisbility {
                get { return _fbGroupLoadingImageVisbility; }
                set
                {
                    _fbGroupLoadingImageVisbility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbGroupLoadingImageVisbility"));
                }
            }
            /**
            public ObservableCollection<twitterProfileUserHeaderInfoListItems> twitterProfileUserHeaderInfoCollections
            {
                get { return _twitterProfileUserHeaderInfoCollections; }
                set
                {
                    _twitterProfileUserHeaderInfoCollections = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterProfileUserHeaderInfoCollections"));
                }
            }
            ObservableCollection<twitterProfileUserHeaderInfoListItems> _twitterProfileUserHeaderInfoCollections = new ObservableCollection<twitterProfileUserHeaderInfoListItems>();
            */
            /// <summary>
            /// ///////////////////////////
            /// //twitter user header info...
            /// </summary>

            public String _twitterProfileUserName;
            public String twitterProfileUserName
            {
                get { return _twitterProfileUserName; }
                set
                {
                    _twitterProfileUserName = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterProfileUserName"));
                }
            }

            public String _twitterProfileUserAge;
            public String twitterProfileUserAge
            {
                get { return _twitterProfileUserAge; }
                set
                {
                    _twitterProfileUserAge = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterProfileUserAge"));
                }
            }

            public String _twitterProfileUserCityCountry;
            public String twitterProfileUserCityCountry
            {
                get { return _twitterProfileUserCityCountry; }
                set
                {
                    _twitterProfileUserCityCountry = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterProfileUserCityCountry"));
                }
            }

            public String _twitterProfileUserAboutMe;
            public String twitterProfileUserAboutMe
            {
                get { return _twitterProfileUserAboutMe; }
                set
                {
                    _twitterProfileUserAboutMe = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterProfileUserAboutMe"));
                }
            }

            public String _twitterProfileUserId;
            public String twitterProfileUserId
            {
                get { return _twitterProfileUserId; }
                set
                {
                    _twitterProfileUserId = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterProfileUserId"));
                }
            }

            public String _twitterProfileUserDp;
            public String twitterProfileUserDp
            {
                get { return _twitterProfileUserDp; }
                set
                {
                    _twitterProfileUserDp = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterProfileUserDp"));
                }
            }

            public String _twitterProfileUserFollowerCount;
            public String twitterProfileUserFollowerCount
            {
                get { return _twitterProfileUserFollowerCount; }
                set
                {
                    _twitterProfileUserFollowerCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterProfileUserFollowerCount"));
                }
            }

            public String _twitterProfileUserFollowingCount;
            public String twitterProfileUserFollowingCount
            {
                get { return _twitterProfileUserFollowingCount; }
                set
                {
                    _twitterProfileUserFollowingCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterProfileUserFollowingCount"));
                }
            }

            public String _twitterProfileUserTweetCount;
            public String twitterProfileUserTweetCount
            {
                get { return _twitterProfileUserTweetCount; }
                set
                {
                    _twitterProfileUserTweetCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterProfileUserTweetCount"));
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////
            // Twiter user tweet data...
            public bool _twitterUserTweetListLoadingVisibility;
            public bool twitterUserTweetListLoadingVisibility
            {
                get { return _twitterUserTweetListLoadingVisibility; }
                set
                {
                    _twitterUserTweetListLoadingVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserTweetListLoadingVisibility"));
                }
            }

            // Twiter user tweet data...
            public bool _nothingToShowTextForSpecificUserOrPageListLoadingVisibility;
            public bool nothingToShowTextForSpecificUserOrPageListLoadingVisibility
            {
                get { return _nothingToShowTextForSpecificUserOrPageListLoadingVisibility; }
                set
                {
                    _nothingToShowTextForSpecificUserOrPageListLoadingVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("nothingToShowTextForSpecificUserOrPageListLoadingVisibility"));
                }
            }

            public bool _nothingToShowTextForTwitterUserSearchVisibility;
            public bool nothingToShowTextForTwitterUserSearchVisibility
            {
                get { return _nothingToShowTextForTwitterUserSearchVisibility; }
                set
                {
                    _nothingToShowTextForTwitterUserSearchVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("nothingToShowTextForTwitterUserSearchVisibility"));
                }
            }

            public bool _nothingToShowTextForTwitterTweetSearchVisibility;
            public bool nothingToShowTextForTwitterTweetSearchVisibility
            {
                get { return _nothingToShowTextForTwitterTweetSearchVisibility; }
                set
                {
                    _nothingToShowTextForTwitterTweetSearchVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("nothingToShowTextForTwitterTweetSearchVisibility"));
                }
            }

            public bool _nothingToShowTextForFbUserSearchVisibility;
            public bool nothingToShowTextForFbUserSearchVisibility
            {
                get { return _nothingToShowTextForFbUserSearchVisibility; }
                set
                {
                    _nothingToShowTextForFbUserSearchVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("nothingToShowTextForFbUserSearchVisibility"));
                }
            }

            public bool _nothingToShowTextForFbPageSearchVisibility;
            public bool nothingToShowTextForFbPageSearchVisibility
            {
                get { return _nothingToShowTextForFbPageSearchVisibility; }
                set
                {
                    _nothingToShowTextForFbPageSearchVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("nothingToShowTextForFbPageSearchVisibility"));
                }
            }

            public bool _nothingToShowTextForFbGroupSearchVisibility;
            public bool nothingToShowTextForFbGroupSearchVisibility
            {
                get { return _nothingToShowTextForFbGroupSearchVisibility; }
                set
                {
                    _nothingToShowTextForFbGroupSearchVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("nothingToShowTextForFbGroupSearchVisibility"));
                }
            }

            public bool _nothingToShowTextForGoogleUserSearchVisibility;
            public bool nothingToShowTextForGoogleUserSearchVisibility
            {
                get { return _nothingToShowTextForGoogleUserSearchVisibility; }
                set
                {
                    _nothingToShowTextForGoogleUserSearchVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("nothingToShowTextForGoogleUserSearchVisibility"));
                }
            }

            public bool _nothingToShowTextForGoogleActivitiesSearchVisibility;
            public bool nothingToShowTextForGoogleActivitiesSearchVisibility
            {
                get { return _nothingToShowTextForGoogleActivitiesSearchVisibility; }
                set
                {
                    _nothingToShowTextForGoogleActivitiesSearchVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("nothingToShowTextForGoogleActivitiesSearchVisibility"));
                }
            }

            public ObservableCollection<twitterUserTweetListItems> twitterUserTweetListCollections { get { return _twitterUserTweetListCollections; } set { _twitterUserTweetListCollections = twitterUserTweetListCollections; } }
            ObservableCollection<twitterUserTweetListItems> _twitterUserTweetListCollections = new ObservableCollection<twitterUserTweetListItems>();

            ///////////////////////////////////////////////////////////////////////////////////
            //twitter tweet page data...

            public bool _twitterTweetTweetListLoadingVisibility;
            public bool twitterTweetTweetListLoadingVisibility
            {
                get { return _twitterTweetTweetListLoadingVisibility; }
                set
                {
                    _twitterTweetTweetListLoadingVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterTweetTweetListLoadingVisibility"));
                }
            }

            public ObservableCollection<twitterTweetPageExpanderListItems> twitterTweetPageExpanderListCollections { get { return _twitterTweetPageExpanderListCollections; } set { _twitterTweetPageExpanderListCollections = twitterTweetPageExpanderListCollections; } }
            ObservableCollection<twitterTweetPageExpanderListItems> _twitterTweetPageExpanderListCollections = new ObservableCollection<twitterTweetPageExpanderListItems>();

            ///////////////////////////////////////////////////////////////////////////////////

            public String _fbUserListCollectionsCount;
            public String fbUserListCollectionsCount
            {
                get { return _fbUserListCollectionsCount; }
                set
                {
                    _fbUserListCollectionsCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbUserListCollectionsCount"));
                }
            }

            public String _fbPageListCollectionsCount;
            public String fbPageListCollectionsCount
            {
                get { return _fbPageListCollectionsCount; }
                set
                {
                    _fbPageListCollectionsCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageListCollectionsCount"));
                }
            }

            public String _fbGroupListCollectionsCount;
            public String fbGroupListCollectionsCount
            {
                get { return _fbGroupListCollectionsCount; }
                set
                {
                    _fbGroupListCollectionsCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbGroupListCollectionsCount"));
                }
            }

            public String _twitterUserListCollectionsCount;
            public String twitterUserListCollectionsCount
            {
                get { return _twitterUserListCollectionsCount; }
                set
                {
                    _twitterUserListCollectionsCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserListCollectionsCount"));
                }
            }

            public String _twitterTweetListCollectionsCount;
            public String twitterTweetListCollectionsCount
            {
                get { return _twitterTweetListCollectionsCount; }
                set
                {
                    _twitterTweetListCollectionsCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterTweetListCollectionsCount"));
                }
            }

            public String _googleUserListCollectionsCount;
            public String googleUserListCollectionsCount
            {
                get { return _googleUserListCollectionsCount; }
                set
                {
                    _googleUserListCollectionsCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleUserListCollectionsCount"));
                }
            }

            public String _googleActivitiesListCollectionsCount;
            public String googleActivitiesListCollectionsCount
            {
                get { return _googleActivitiesListCollectionsCount; }
                set
                {
                    _googleActivitiesListCollectionsCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleActivitiesListCollectionsCount"));
                }
            }


            public ObservableCollection<fbUserListItems> fbUserListCollections 
            {
                get { return _fbUserListCollections; }
                set {
                    _fbUserListCollections = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbUserListCollections"));  
                }
            }
            ObservableCollection<fbUserListItems> _fbUserListCollections = new ObservableCollection<fbUserListItems>();

            public ObservableCollection<fbGroupListItems> fbGroupListCollections { get { return _fbGroupListCollections; } }
            ObservableCollection<fbGroupListItems> _fbGroupListCollections = new ObservableCollection<fbGroupListItems>();

            public ObservableCollection<fbPageListItems> fbPageListCollections { get { return _fbPageListCollections; } }
            ObservableCollection<fbPageListItems> _fbPageListCollections = new ObservableCollection<fbPageListItems>();

            public ObservableCollection<twitterUserListItems> twitterUserListCollections { get { return _twitterUserListCollections; } set { _twitterUserListCollections = twitterUserListCollections; } }
            ObservableCollection<twitterUserListItems> _twitterUserListCollections = new ObservableCollection<twitterUserListItems>();

            public ObservableCollection<twitterTweetListItems> twitterTweetListCollections { get { return _twitterTweetListCollections; } set { _twitterTweetListCollections = twitterTweetListCollections; } }
            ObservableCollection<twitterTweetListItems> _twitterTweetListCollections = new ObservableCollection<twitterTweetListItems>();

            public ObservableCollection<twitterTweetPageListItems> twitterTweetPageListCollections { get { return _twitterTweetPageListCollections; } set { _twitterTweetPageListCollections = twitterTweetPageListCollections; } }
            ObservableCollection<twitterTweetPageListItems> _twitterTweetPageListCollections = new ObservableCollection<twitterTweetPageListItems>();
        
            ///////////////////////////////////////////////////////////////////////////////
            //*** new tab fb page variables...
            public ObservableCollection<fbPageTabListItems> fbPageTabListCollections { get { return _fbPageTabListCollections; } }
            ObservableCollection<fbPageTabListItems> _fbPageTabListCollections = new ObservableCollection<fbPageTabListItems>();

            public String _fbPageTabInfo_ImageSource;
            public String fbPageTabInfo_ImageSource
            {
                get { return _fbPageTabInfo_ImageSource; }
                set
                {
                    _fbPageTabInfo_ImageSource = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_ImageSource"));
                }
            }

            public String _fbPageTabInfo_LabelContent;
            public String fbPageTabInfo_LabelContent
            {
                get { return _fbPageTabInfo_LabelContent; }
                set
                {
                    _fbPageTabInfo_LabelContent = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_LabelContent"));
                }
            }

            public String _fbPageTabInfo_ID;
            public String fbPageTabInfo_ID
            {
                get { return _fbPageTabInfo_ID; }
                set
                {
                    _fbPageTabInfo_ID = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_ID"));
                }
            }

            public String _fbPageTabInfo_desc;
            public String fbPageTabInfo_desc
            {
                get { return _fbPageTabInfo_desc; }
                set
                {
                    _fbPageTabInfo_desc = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_desc"));
                }
            }

            public String _fbPageTabInfo_about;
            public String fbPageTabInfo_about
            {
                get { return _fbPageTabInfo_about; }
                set
                {
                    _fbPageTabInfo_about = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_about"));
                }
            }


            public String _fbPageTabInfo_awards;
            public String fbPageTabInfo_awards
            {
                get { return _fbPageTabInfo_awards; }
                set
                {
                    _fbPageTabInfo_awards = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_awards"));
                }
            }

            public String _fbPageTabInfo_canPost;
            public String fbPageTabInfo_canPost
            {
                get { return _fbPageTabInfo_canPost; }
                set
                {
                    _fbPageTabInfo_canPost= value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_canPost"));
                }
            }

            public String _fbPageTabInfo_checkIns;
            public String fbPageTabInfo_checkIns
            {
                get { return _fbPageTabInfo_checkIns; }
                set
                {
                    _fbPageTabInfo_checkIns= value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_checkIns"));
                }
            }

            public String _fbPageTabInfo_coverSource;
            public String fbPageTabInfo_coverSource
            {
                get { return _fbPageTabInfo_coverSource; }
                set
                {
                    _fbPageTabInfo_coverSource = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_coverSource"));
                }
            }

            public String _fbPageTabInfo_description;
            public String fbPageTabInfo_description
            {
                get { return _fbPageTabInfo_description; }
                set
                {
                    _fbPageTabInfo_description = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_description"));
                }
            }

            public String _fbPageTabInfo_hasAddedApp;
            public String fbPageTabInfo_hasAddedApp
            {
                get { return _fbPageTabInfo_hasAddedApp; }
                set
                {
                    _fbPageTabInfo_hasAddedApp = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_hasAddedApp"));
                }
            }

            public String _fbPageTabInfo_isCommunityPage;
            public String fbPageTabInfo_isCommunityPage
            {
                get { return _fbPageTabInfo_isCommunityPage; }
                set
                {
                    _fbPageTabInfo_isCommunityPage = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_isCommunityPage"));
                }
            }

            public String _fbPageTabInfo_isPublished;
            public String fbPageTabInfo_isPublished
            {
                get { return _fbPageTabInfo_isPublished; }
                set
                {
                    _fbPageTabInfo_isPublished = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_isPublished"));
                }
            }

            public String _fbPageTabInfo_link;
            public String fbPageTabInfo_link
            {
                get { return _fbPageTabInfo_link; }
                set
                {
                    _fbPageTabInfo_link= value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_link"));
                }
            }

            public String _fbPageTabInfo_userName;
            public String fbPageTabInfo_userName
            {
                get { return _fbPageTabInfo_userName; }
                set
                {
                    _fbPageTabInfo_userName = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_userName"));
                }
            }

            public String _fbPageTabInfo_website;
            public String fbPageTabInfo_website
            {
                get { return _fbPageTabInfo_website; }
                set
                {
                    _fbPageTabInfo_website = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_website"));
                }
            }

            public String _fbPageTabInfo_wereHere;
            public String fbPageTabInfo_wereHere
            {
                get { return _fbPageTabInfo_wereHere; }
                set
                {
                    _fbPageTabInfo_wereHere = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_wereHere"));
                }
            }

            public String _fbPageTabInfo_location;
            public String fbPageTabInfo_location
            {
                get { return _fbPageTabInfo_location; }
                set
                {
                    _fbPageTabInfo_location = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_location"));
                }
            }

            public String _fbPageTabInfo_likesCount;
            public String fbPageTabInfo_likesCount
            {
                get { return _fbPageTabInfo_likesCount; }
                set
                {
                    _fbPageTabInfo_likesCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_likesCount"));
                }
            }

            public String _fbPageTabInfo_Category;
            public String fbPageTabInfo_Category
            {
                get { return _fbPageTabInfo_Category; }
                set
                {
                    _fbPageTabInfo_Category = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_Category"));
                }
            }
            
            public String _fbPageTabInfo_talkingAboutCount;
            public String fbPageTabInfo_talkingAboutCount
            {
                get { return _fbPageTabInfo_talkingAboutCount; }
                set
                {
                    _fbPageTabInfo_talkingAboutCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTabInfo_talkingAboutCount"));
                }
            }
            ///////////////////////////////////////////////////////////////////////////////

            //public ObservableCollection<Person> People { get; set; }

            public aTabItem()
            {
                //People = new ObservableCollection<Person>();
            }
            
            public RelayCommand urlOpener_MouseLeftButtonUpCommand
            {
                get;
                private set;
            }

            private ICommand _urlOpener_MouseLeftButtonUpsaveCommand;
            public ICommand urlOpener_MouseLeftButtonUpSaveCommand
            {
                get
                {
                    if (_urlOpener_MouseLeftButtonUpsaveCommand == null)
                    {
                        _urlOpener_MouseLeftButtonUpsaveCommand = new RelayCommand(
                            param => myStaticObject.urlOpener_MouseLeftButtonUpStatic(),
                            param => this.urlOpener_MouseLeftButtonUpCanSave()
                        );
                    }
                    return _urlOpener_MouseLeftButtonUpsaveCommand;
                }
            }

            private bool urlOpener_MouseLeftButtonUpCanSave()
            {
                // Verify command can be executed here

                return true;
            }
       
            ///////////////////////////////////////////////////////////////////////

            public RelayCommand twitterUserInfoToReport_ClickCommand
            {
                get;
                private set;
            }

            private ICommand _twitterUserInfoToReport_ClicksaveCommand;
            public ICommand twitterUserInfoToReport_ClickSaveCommand
            {
                get
                {
                    if (_twitterUserInfoToReport_ClicksaveCommand == null)
                    {
                        //MainWindow m = new MainWindow();

                        _twitterUserInfoToReport_ClicksaveCommand = new RelayCommand(
                            param => myStaticObject.twitterUserInfoToReport_ClickStatic(),
                            param => this.twitterUserInfoToReport_ClickCanSave()
                        );
                    }
                    return _twitterUserInfoToReport_ClicksaveCommand;
                }
            }

            private bool twitterUserInfoToReport_ClickCanSave()
            {
                // Verify command can be executed here

                return true;
            }

            ///////////////////////////////////////////////////////////////////////

            public RelayCommand removeBookmark_ClickCommand
            {
                get;
                private set;
            }

            private ICommand _removeBookmark_ClicksaveCommand;
            public ICommand removeBookmark_ClickSaveCommand
            {
                get
                {
                    if (_removeBookmark_ClicksaveCommand == null)
                    {
                        //MainWindow m = new MainWindow();

                        _removeBookmark_ClicksaveCommand = new RelayCommand(
                            param => myStaticObject.removeBookmark_ClickStatic(),
                            param => this.removeBookmark_ClickCanSave()
                        );
                    }
                    return _removeBookmark_ClicksaveCommand;
                }
            }

            private bool removeBookmark_ClickCanSave()
            {
                // Verify command can be executed here

                return true;
            }

            ///////////////////////////////////////////////////////////////////////

            public RelayCommand twitterBookmarkUser_ClickCommand
            {
                get;
                private set;
            }

            private ICommand _twitterBookmarkUser_ClicksaveCommand;
            public ICommand twitterBookmarkUser_ClickSaveCommand
            {
                get
                {
                    if (_twitterBookmarkUser_ClicksaveCommand == null)
                    {
                        _twitterBookmarkUser_ClicksaveCommand = new RelayCommand(
                            param => myStaticObject.twitterBookmarkUser_ClickStatic(),
                            param => this.twitterBookmarkUser_ClickCanSave()
                        );
                    }
                    return _twitterBookmarkUser_ClicksaveCommand;
                }
            }

            private bool twitterBookmarkUser_ClickCanSave()
            {
                // Verify command can be executed here

                return true;
            }

            ///////////////////////////////////////////////////////////////////////


            ///////////////////////////////////////////////////////////////////////

            public RelayCommand twitterAllTweetsToReport_ClickCommand
            {
                get;
                private set;
            }

            private ICommand _twitterAllTweetsToReport_ClicksaveCommand;
            public ICommand twitterAllTweetsToReport_ClickSaveCommand
            {
                get
                {
                    if (_twitterAllTweetsToReport_ClicksaveCommand == null)
                    {
                        _twitterAllTweetsToReport_ClicksaveCommand = new RelayCommand(
                            param => myStaticObject.twitterAllTweetsToReport_ClickStatic(),
                            param => this.twitterAllTweetsToReport_ClickCanSave()
                        );
                    }
                    return _twitterAllTweetsToReport_ClicksaveCommand;
                }
            }

            private bool twitterAllTweetsToReport_ClickCanSave()
            {
                // Verify command can be executed here

                return true;
            }

            ///////////////////////////////////////////////////////////////////////

            public RelayCommand twitterTweetToReport_ClickCommand
            {
                get;
                private set;
            }

            private ICommand _twitterTweetToReport_ClicksaveCommand;
            public ICommand twitterTweetToReport_ClickSaveCommand
            {
                get
                {
                    if (_twitterTweetToReport_ClicksaveCommand == null)
                    {
                        _twitterTweetToReport_ClicksaveCommand = new RelayCommand(
                            param => myStaticObject.twitterTweetToReport_ClickStatic(),
                            param => this.twitterTweetToReport_ClickCanSave()
                        );
                    }
                    return _twitterTweetToReport_ClicksaveCommand;
                }
            }

            private bool twitterTweetToReport_ClickCanSave()
            {
                // Verify command can be executed here

                return true;
            }

            ///////////////////////////////////////////////////////////////////////

            public RelayCommand likeIconClick_previewMouseButtonDownCommand
            {
                get;
                private set;
            }

            private ICommand _likeIconClick_previewMouseButtonDownsaveCommand;
            public ICommand likeIconClick_previewMouseButtonDownSaveCommand
            {
                get
                {
                    if (_likeIconClick_previewMouseButtonDownsaveCommand == null)
                    {
                        _likeIconClick_previewMouseButtonDownsaveCommand = new RelayCommand(
                            param => myStaticObject.likeIconClick_previewMouseButtonDownStatic(),
                            param => this.likeIconClick_previewMouseButtonDownCanSave()
                        );
                    }
                    return _likeIconClick_previewMouseButtonDownsaveCommand;
                }
            }

            private bool likeIconClick_previewMouseButtonDownCanSave()
            {
                // Verify command can be executed here

                return true;
            }

            ///////////////////////////////////////////////////////////////////////

            public RelayCommand commentIconClick_previewMouseButtonDownCommand
            {
                get;
                private set;
            }

            private ICommand _commentIconClick_previewMouseButtonDownsaveCommand;
            public ICommand commentIconClick_previewMouseButtonDownSaveCommand
            {
                get
                {
                    if (_commentIconClick_previewMouseButtonDownsaveCommand == null)
                    {
                        _commentIconClick_previewMouseButtonDownsaveCommand = new RelayCommand(
                            param => myStaticObject.commentIconClick_previewMouseButtonDownStatic(),
                            param => this.commentIconClick_previewMouseButtonDownCanSave()
                        );
                    }
                    return _commentIconClick_previewMouseButtonDownsaveCommand;
                }
            }

            private bool commentIconClick_previewMouseButtonDownCanSave()
            {
                // Verify command can be executed here

                return true;
            }

            ///////////////////////////////////////////////////////////////////////

        }//class country...

        public ObservableCollection<fbUserListItems> outerListCollections
        {
            get { return _outerListCollections; }
            set
            {
                _outerListCollections = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("outerListCollections"));
            }
        }
        ObservableCollection<fbUserListItems> _outerListCollections = new ObservableCollection<fbUserListItems>();

        public ObservableCollection<aTabItem> Countries { get; set; }

        public ObservableCollection<bookmarkListItems> bookmarkListCollections
        {
            get { return _bookmarkListCollections; }
            set
            {
                _bookmarkListCollections = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("bookmarkListCollections"));
            }
        }
        public ObservableCollection<bookmarkListItems> _bookmarkListCollections = new ObservableCollection<bookmarkListItems>();
        public ObservableCollection<tweetToReportItem> tweetsToReportList { get; set; }        
        public ObservableCollection<tweetToReportItem> tweetsToReportListSad { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListUpset { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListUnHappy { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListStressed { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListDepressed { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListNervous { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListSubdued { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListActive { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListCalm { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListAlert { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListRelaxed { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListExcited { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListElated { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListSerene { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListHappy { get; set; }
        public ObservableCollection<tweetToReportItem> tweetsToReportListContented { get; set; }
        public ObservableCollection<osintItem> osintBased { get; set; }
        public ObservableCollection<osintItem> osintGoogleBased { get; set; }
        public ObservableCollection<osintItem> osintfbBased { get; set; }
        public ObservableCollection<osintItem> careSentimentBased { get; set; }

        //is is all that the interface requires
        public event PropertyChangedEventHandler PropertyChanged;//=new PropertyChangedEventHandler();

        bool _outerDivVisibility;
        public bool outerDivVisibility
        {
            get { return _outerDivVisibility; }
            set
            {
                _outerDivVisibility = value;
               // PropertyChanged(this,
                    OnPropertyChanged("outerDivVisibility");
            }
        }

        private void OnPropertyChanged(string propertyName){
    var handler = PropertyChanged;
    if (handler != null)
        handler(this, new PropertyChangedEventArgs(propertyName));
        }

        bool _outerLoadingImageVisibility;
        public bool outerLoadingImageVisibility
        {
            get { return _outerLoadingImageVisibility; }
            set
            {
                _outerLoadingImageVisibility = value;
                //PropertyChanged(this, new PropertyChangedEventArgs
                    OnPropertyChanged("outerLoadingImageVisibility");
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //**** google plus variables...
        string[] scopes;
        String CLIENT_ID = "904980209954-1lcou02m0c5a3auodim9i0ue0dbg2lnt.apps.googleusercontent.com";
        String CLIENT_SECRET = "_p5GYoAWngP2a4PdfrpgYLqD";
        string redirect_url = "urn:ietf:wg:oauth:2.0:oob";

        GoogleSearch GS; //= new GoogleSearch("904980209954-1lcou02m0c5a3auodim9i0ue0dbg2lnt.apps.googleusercontent.com", "_p5GYoAWngP2a4PdfrpgYLqD");        
        public MainWindow()
        {
            /*SplashScreen splash = new SplashScreen("twitter123.png");
            splash.Show(false);
            splash.Close(TimeSpan.FromMilliseconds(16000));*/
            
            InitializeComponent();

            //connection = new MySqlConnection(MyConnectionString);

            //////////////////////////////////////////////////////////////////////
            // initialize encryption....

            EncryptConfigurationFile();

            FetchMachineIpAndMacAddress();
            FetchMachineSID();

            //Mutex myMutex ;

     //private void Application_Startup(object sender, StartupEventArgs e)
     //{
        /*bool aIsNewInstance = false;
        myMutex = new Mutex(true, "MyWPFApplication", out aIsNewInstance);  
           if (!aIsNewInstance)
            {
              MessageBox.Show("Already an instance is running...");
              App.Current.Shutdown();  
            }*/
      //}

            //////////////////////////////////////////////////////////////////////

            try
            {
                //**ProcessStart(p1,p2);

                using (StreamReader sr = new StreamReader("fbSessionId.txt"))
                {
                    String line = sr.ReadToEnd();
                    //Console.WriteLine(line);
                    fbSessionId = line;
                }

                using (StreamReader sr1 = new StreamReader("reportingFileLocation.txt"))
                {
                    String line1 = sr1.ReadToEnd();
                    //Console.WriteLine(line);
                    reportFileLocation = line1;
                }
            }
            catch (Exception e)
            {
                int yyy = 0;
                int dd = 0;
                ////Console.WriteLine("The file could not be read:");
                //Console.WriteLine(e.Message);
            }//catch...

            try
            {
                GS = new GoogleSearch("904980209954-1lcou02m0c5a3auodim9i0ue0dbg2lnt.apps.googleusercontent.com", "_p5GYoAWngP2a4PdfrpgYLqD");

                outerListCollections.Clear();
                bookmarkListCollections.Clear();

                bookmarkNothingToshowText.Visibility = Visibility.Visible;

                int counterrr = 0; String line = "";

                using (StreamReader sr = new StreamReader("bookmarks.txt"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        //System.Console.WriteLine(line);
                        if (line.Trim().Length > 10)
                            counterrr++;
                        else
                            continue;

                        String name = "", type = "", socialMedia = "", id = "", imag_url = "", tweetCount = "", followingCount = "0", followersCount = "0", cityCountry = "", age = "", aboutMe = "", profile_url = "";

                        if (line.IndexOf(",.,.,.,.,.") >= 0)
                        {
                            name = line.Substring(0,line.IndexOf(",.,.,.,.,."));
                            line = line.Substring(line.IndexOf(",.,.,.,.,.")+10);
                        }

                        if (line.IndexOf(",.,.,.,.,.") >= 0)
                        {
                            type = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                            line = line.Substring(line.IndexOf(",.,.,.,.,.")+ 10);
                        }
                        if (line.IndexOf(",.,.,.,.,.") >= 0)
                        {
                            socialMedia = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                            line = line.Substring(line.IndexOf(",.,.,.,.,.")+ 10);
                        }
                        if (line.IndexOf(",.,.,.,.,.") >= 0)
                        {
                            id = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                            line = line.Substring(line.IndexOf(",.,.,.,.,.")+ 10);
                        }
                        if (line.IndexOf(",.,.,.,.,.") >= 0)
                        {
                            imag_url = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                            //line = line.Substring(line.IndexOf(",.,.,.,.,."), 10);
                        }

                        //if social media==twitter and type==user...
                        if (socialMedia == "twitter" && type == "user")
                        {
                            //if we have sufficient and enough data for an individual...
                            if (name.Length > 0 && type.Length > 0 && socialMedia.Length > 0 && id.Length > 0 && imag_url.Length > 0)
                            {
                                line = line.Substring(line.IndexOf(",.,.,.,.,.")+ 10);

                                if (line.IndexOf(",.,.,.,.,.") >= 0)
                                {
                                    tweetCount = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                                    line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                                }

                                if (line.IndexOf(",.,.,.,.,.") >= 0)
                                {
                                    followingCount = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                                    line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                                }
                                if (line.IndexOf(",.,.,.,.,.") >= 0)
                                {
                                    followersCount = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                                    line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                                }
                                if (line.IndexOf(",.,.,.,.,.") >= 0)
                                {
                                    cityCountry = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                                    line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                                }
                                if (line.IndexOf(",.,.,.,.,.") >= 0)
                                {
                                    age = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                                    line = line.Substring(line.IndexOf(",.,.,.,.,.")+ 10);
                                }
                                if (line.IndexOf(",.,.,.,.,.") >= 0)
                                {
                                    aboutMe = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                                    line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                                }
                                if (line.IndexOf(",.,.,.,.,.") >= 0)
                                {
                                    profile_url = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                                    //line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                                }

                                string userIconLink = "/WpfApplication2;component/Resources/fb.png";

                                if (socialMedia == "twitter") userIconLink = "/WpfApplication2;component/Resources/twitter.png";
                                if (socialMedia == "googlePlus") userIconLink = "/WpfApplication2;component/Resources/google_plus_icon_small.png";

                                bookmarkListCollections.Add(new bookmarkListItems()
                                {
                                    bookmarkMoreOptionBehindVisibility = true,
                                    bookmarkMoreOptionVisibility = true,
                                    GridViewColumnName_ID = id,
                                    GridViewColumnName_ImageSource = imag_url,
                                    GridViewColumnName_LabelContent = name,
                                    GridViewColumnSocialMedia = socialMedia,
                                    GridViewColumnType = type,
                                    GridViewColumnIconFbOrTwitter=userIconLink,
                                    GridViewColumnName_tweetsCount = tweetCount,
                                    GridViewColumnName_FollowingCount = followingCount,
                                    GridViewColumnName_FollowersCount = followersCount,
                                    GridViewColumnName_CityCountry = cityCountry,
                                    GridViewColumnName_Age = age,
                                    GridViewColumnName_AboutMe = aboutMe,
                                    twitterUserProfileUrl = profile_url
                                });
                            }//if we have sifficient data...
                        }//if(socialMedia=="twitter"&&type=="user")...

                    }
                    //String line = sr.ReadToEnd();
                    //Console.WriteLine(line);
                    //fbSessionId = line;
                }

                if (counterrr > 0)
                    bookmarkNothingToshowText.Visibility = Visibility.Collapsed;

                bookmarkList.DataContext = bookmarkListCollections;
                bookmarkList.ItemsSource = bookmarkListCollections;

                Countries = new ObservableCollection<aTabItem>();
                osintBased = new ObservableCollection<osintItem>();
                osintGoogleBased = new ObservableCollection<osintItem>();
                osintfbBased = new ObservableCollection<osintItem>();
                careSentimentBased = new ObservableCollection<osintItem>();

                tweetsToReportList = new ObservableCollection<tweetToReportItem>();

                aTabItem England = new aTabItem() { Header = "Search123" };
                //England.People.Add(new Person() { Name = "Ian" });
                England.Header = "Result";
                England.HeaderImgSrc = "/WpfApplication2;component/Resources/search.png";
                England.mySearch = "";
                England.lastSearch = "";
                England.type = "Result";
                England.tab_number = "0";
                England.fbUserListCollections.Clear();
                England.fbUserListCollectionsCount = "0";
                England.fbPageListCollectionsCount = "0";
                England.fbGroupListCollectionsCount = "0";
                England.twitterUserListCollectionsCount = "0";
                England.twitterTweetListCollectionsCount = "0";
                England.googleUserListCollectionsCount = "0";
                England.googleActivitiesListCollectionsCount = "0";
                England.twitterUserListCollections.Clear();
                //false means will hide the div...
                England.twitterUserDivVisbility = false;
                England.twitterTweetListDivVisbility = false;
                England.searchUserDivVisbility = true;
                England.googleUserDivVisbility = false;

                England.sourceCodeSenti=@"";
                
                England.headerCloseIconVisibility = false;
                England.sADivVisbility = false;

                England.fbGroupLoadingImageVisbility = false;
                England.fbUserLoadingImageVisbility = false;
                England.fbPageLoadingImageVisbility = false;
                England.twitterTweetLoadingImageVisbility = false;
                England.twitterUserLoadingImageVisbility = false;
                England.googleActivitiesLoadingImageVisbility = false;
                England.googleUserLoadingImageVisbility = false;

                England.fbUserListVisibility = true;
                England.twUserListVisibility = true;
                England.gUserListVisibility = true;

                England.fbGroupDivVisbility = false;
                England.fbPageDivVisbility = false;
                England.twTweetListVisibility = false;
                England.gActivityListVisibility = false;

                screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;

                /////////////////////////////////////////////////////////////////////////////
                #region upper arrows....

                CanvasShape cS = new CanvasShape();
                cS.imageVisibility = false;
                cS.x1 = (screenWidth / 2.0f) - 40.0f+25.0f; cS.x2 = (screenWidth / 3.0f) / 2.0f; cS.y1 = 0.0f-10.0f; cS.y2 = 10.0f + 87.0f;
                cS.lineVisibility = true;
                cS.lineColor = "#44000000";
                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.imageVisibility = false;
                cS.x1 = (screenWidth / 2.0f) - 40.0f+25.0f; cS.x2 = (screenWidth / 2.0f) - 40.0f+25.0f; cS.y1 = 0.0f-10.0f; cS.y2 = 10.0f + 87.0f;
                cS.lineVisibility = true;
                cS.lineColor = "#44000000";
                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.imageVisibility = false;
                cS.x1 = (screenWidth / 2.0f) - 40.0f + 25.0f; cS.x2 = screenWidth - (screenWidth / 3.0f) / 2.0f - 60.0f+25.0f; cS.y1 = 0.0f-10.0f; cS.y2 = 10.0f + 87.0f;
                cS.lineVisibility = true;
                cS.lineColor = "#44000000";
                England.Shapes.Add(cS);

                #endregion

                ///////////////////////////////////////////////////////////////////////////////////

                #region fb canvas objects...

                #region arrows

                cS = new CanvasShape();
                cS.imageVisibility = false;
                cS.x1 = (screenWidth / 3.0f) / 2.0f - 0.0f; cS.x2 = (screenWidth / 3.0f) / 2.0f - 0.0f - 100.0f; cS.y1 = 55.0f + 85.0f; cS.y2 = 95.0f + 85.0f;
                cS.lineVisibility = true;
                cS.lineColor = "#663b5998";
                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.imageVisibility = false;
                cS.x1 = (screenWidth / 3.0f) / 2.0f - 0.0f; cS.x2 = (screenWidth / 3.0f) / 2.0f; cS.y1 = 55.0f + 85.0f; cS.y2 = 95.0f + 85.0f;
                cS.lineVisibility = true;
                cS.lineColor = "#663b5998";
                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.imageVisibility = false;
                cS.x1 = (screenWidth / 3.0f) / 2.0f - 0.0f; cS.x2 = (screenWidth / 3.0f) / 2.0f - 0.0f + 100.0f; cS.y1 = 55.0f + 85.0f; cS.y2 = 95.0f + 85.0f;
                cS.lineVisibility = true;
                cS.lineColor = "#663b5998";

                England.Shapes.Add(cS);

                #endregion

                cS = new CanvasShape();
                cS.X = (screenWidth / 3.0f) / 2.0f - 25.0f; //10.0f + 78.0f; 
                cS.Y = 10.0f + 85.0f; cS.Height = 50.0f; cS.Width = 50.0f;
                cS.textIfAny = "";
                cS.ImageSource = "/CAREsma;component/Resources/fb.png";
                cS.imageVisibility = true;

                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = (screenWidth / 3.0f) / 2.0f - 30.0f - 100.0f; cS.Y = 90.0f + 85.0f; cS.Height = 40.0f; cS.Width = 60.0f;
                cS.textColor = "#ffffff";
                cS.ImageSource = "/CAREsma;component/Resources/fb_color.png";
                cS.imageVisibility = true;
                cS.type = "fbUser";
                cS.textIfAny = "0";//Users Found: 
                
                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = (screenWidth / 3.0f) / 2.0f - 30.0f; cS.Y = 90.0f + 85.0f; cS.Height = 40.0f; cS.Width = 60.0f;
                cS.textIfAny = "0";//Groups Found: 
                cS.textColor = "#ffffff";
                cS.ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
                cS.imageVisibility = true;
                cS.type = "fbGroup";

                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = (screenWidth / 3.0f) / 2.0f - 30.0f + 100.0f; cS.Y = 90.0f + 85.0f; cS.Height = 40.0f; cS.Width = 60.0f;
                cS.textIfAny = "0";//Pages Found:
                cS.ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
                cS.imageVisibility = true;
                cS.textColor = "#ffffff";
                cS.type = "fbPage";

                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = (screenWidth / 3.0f) / 2.0f - 30.0f - 100.0f + 10.0f; cS.Y = 85.0f + 90.0f + 35.0f; cS.Height = 40.0f; cS.Width = 60.0f;
                cS.textIfAny = "Users";//Pages Found: 
                cS.imageVisibility = false;
                cS.textColor = "#3b5998";
                cS.type = "fbUserText";
                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = (screenWidth / 3.0f) / 2.0f - 30.0f + 00.0f + 8.0f; cS.Y = 85.0f + 90.0f + 35.0f; cS.Height = 40.0f; cS.Width = 60.0f;
                cS.textIfAny = "Groups";//Pages Found: 
                cS.imageVisibility = false;
                cS.textColor = "#3b5998";
                cS.type = "fbGroupText";
                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = (screenWidth / 3.0f) / 2.0f - 30.0f + 100.0f + 10.0f; cS.Y = 85.0f + 90.0f + 35.0f; cS.Height = 40.0f; cS.Width = 60.0f;
                cS.textIfAny = "Pages";//Pages Found: 
                cS.imageVisibility = false;
                cS.textColor = "#3b5998";
                cS.type = "fbPageText";
                England.Shapes.Add(cS);

                #endregion

                ////////////////////////////////////////////////////////////////////////////

                #region twitter canvas objects...

                #region twitter arrows...

                cS = new CanvasShape();
                cS.imageVisibility = false;
                cS.x1 = (screenWidth / 2.0f) - 40.0f + 25.0f; cS.x2 = (screenWidth / 2.0f) - 100.0f - 40.0f + 30.0f; cS.y1 = 55.0f + 85.0f; cS.y2 = 95.0f + 85.0f;
                cS.lineVisibility = true;
                cS.lineColor = "#664099ff";
                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.imageVisibility = false;
                cS.x1 = (screenWidth / 2.0f) - 40.0f + 25.0f; cS.x2 = (screenWidth / 2.0f) + 100.0f - 40.0f + 30.0f; cS.y1 = 55.0f + 85.0f; cS.y2 = 95.0f + 85.0f;
                cS.lineVisibility = true;
                cS.lineColor = "#664099ff";

                England.Shapes.Add(cS);

                #endregion

                cS = new CanvasShape();
                cS.X = (screenWidth / 2.0f) - 40.0f; cS.Y = 10.0f + 85.0f; cS.Height = 50.0f; cS.Width = 50.0f;//500.0f+20.0f+40.0f
                cS.textIfAny = "";
                cS.ImageSource = "/WpfApplication2;component/Resources/twitter.png";
                cS.imageVisibility = true;

                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = (screenWidth / 2.0f) - 100.0f - 40.0f; cS.Y = 90.0f + 85.0f; cS.Height = 40.0f; cS.Width = 60.0f;//500.0f +20.0f+ 0.0f
                cS.ImageSource = "/CAREsma;component/Resources/twitter_color.png";
                cS.textIfAny = "0";
                cS.textColor = "#ffffff";
                cS.type = "twUser";
                cS.imageVisibility = true;

                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = (screenWidth / 2.0f) + 100.0f - 40.0f; cS.Y = 90.0f + 85.0f; cS.Height = 40.0f; cS.Width = 60.0f;//500.0f +20.0f+ 70.0f
                cS.textIfAny = "0";
                cS.ImageSource = "/CAREsma;component/Resources/twitter_color_fade.png";
                cS.textColor = "#ffffff";
                cS.type = "twTweet";
                cS.imageVisibility = true;

                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = (screenWidth / 2.0f) - 100.0f - 40.0f + 10.0f; cS.Y = 85.0f + 90.0f + 35.0f; cS.Height = 40.0f; cS.Width = 60.0f;
                cS.textIfAny = "Users";//Pages Found: 
                cS.imageVisibility = false;
                cS.textColor = "#3b5998";
                cS.type = "twUserText";
                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = (screenWidth / 2.0f) + 100.0f - 40.0f + 10.0f; cS.Y = 85.0f + 90.0f + 35.0f; cS.Height = 40.0f; cS.Width = 60.0f;
                cS.textIfAny = "Tweets";//Pages Found: 
                cS.imageVisibility = false;
                cS.textColor = "#3b5998";
                cS.type = "twTweetText";
                England.Shapes.Add(cS);

                #endregion

                ///////////////////////////////////////////////////////////////////////////////////

                #region g+ canvas objects...

                #region g+ arrows...

                cS = new CanvasShape();
                cS.imageVisibility = false;
                cS.x1 = screenWidth - (screenWidth / 3.0f) / 2.0f - 40.0f; cS.x2 = screenWidth - (screenWidth / 3.0f) / 2.0f - 20.0f - 100.0f; cS.y1 = 55.0f + 85.0f; cS.y2 = 95.0f + 85.0f;
                cS.lineVisibility = true;
                cS.lineColor = "#66b93425";
                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.imageVisibility = false;
                cS.x1 = screenWidth - (screenWidth / 3.0f) / 2.0f - 40.0f; cS.x2 = screenWidth - (screenWidth / 3.0f) / 2.0f - 20.0f + 100.0f; cS.y1 = 55.0f + 85.0f; cS.y2 = 95.0f + 85.0f;
                cS.lineVisibility = true;
                cS.lineColor = "#66b93425";

                England.Shapes.Add(cS);

                #endregion

                cS = new CanvasShape();
                cS.X = screenWidth - (screenWidth / 3.0f) / 2.0f - 60.0f; cS.Y = 10.0f + 85.0f; cS.Height = 50.0f; cS.Width = 50.0f;//800.0f+200.0f + 50.0f
                cS.textIfAny = "";
                cS.ImageSource = "/WpfApplication2;component/Resources/google_plus_icon_small.png";
                cS.imageVisibility = true;

                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = screenWidth - (screenWidth / 3.0f) / 2.0f - 60.0f - 100.0f; cS.Y = 90.0f + 85.0f; cS.Height = 40.0f; cS.Width = 60.0f;//800.0f+200.0f + 20.0f+0.0f
                cS.textIfAny = "0";
                cS.ImageSource = "/CAREsma;component/Resources/google_color.png";
                cS.textColor = "#ffffff";
                cS.type = "gUser";
                cS.imageVisibility = true;

                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = screenWidth - (screenWidth / 3.0f) / 2.0f - 60.0f + 100.0f; cS.Y = 90.0f + 85.0f; cS.Height = 40.0f; cS.Width = 60.0f;//800.0f + 200.0f +20.0f+ 70.0f
                cS.textIfAny = "0";
                cS.ImageSource = "/CAREsma;component/Resources/google_color_fade.png";
                cS.textColor = "#ffffff";
                cS.type = "gActivity";
                cS.imageVisibility = true;

                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = screenWidth - (screenWidth / 3.0f) / 2.0f - 60.0f - 100.0f + 10.0f; cS.Y = 35.0f + 90.0f + 85.0f; cS.Height = 40.0f; cS.Width = 60.0f;
                cS.textIfAny = "Users";//Pages Found: 
                cS.imageVisibility = false;
                cS.textColor = "#3b5998";
                cS.type = "gUserText";
                England.Shapes.Add(cS);

                cS = new CanvasShape();
                cS.X = screenWidth - (screenWidth / 3.0f) / 2.0f - 60.0f + 100.0f + 2.0f; cS.Y = 35.0f + 90.0f + 85.0f; cS.Height = 40.0f; cS.Width = 60.0f;
                cS.textIfAny = "Activities";//Pages Found: 
                cS.imageVisibility = false;
                cS.textColor = "#3b5998";
                cS.type = "gActivityText";
                England.Shapes.Add(cS);

                #endregion

                //////////////////////////////////////////////////////////////////////////////////////////

                // add a tabItem with + in header 
                aTabItem plusOne = new aTabItem(){ Header = "+" };
                plusOne.mySearch = "";
                plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/search.png";
                plusOne.type = "add";
                plusOne.fbUserListCollections.Clear();
                plusOne.twitterUserListCollections.Clear();
                plusOne.tab_number = "10000";
                plusOne.twitterUserDivVisbility = true;
                plusOne.twitterTweetListDivVisbility = false;
                plusOne.searchUserDivVisbility = false;
                plusOne.headerCloseIconVisibility = false;
                plusOne.googleUserDivVisbility = false;
                Countries.Add(England);
                Countries.Add(plusOne);

                // bind tab control
                tabDynamic.DataContext = Countries;
                tabDynamic.SelectedIndex = 0;

                osintItem item1 = new osintItem() { Header = "Search123" };
                item1.Header = "Result";
                item1.mySearch1 = "";
                item1.type = "Result";
                item1.tab_number = "0";

                item1.sourceCodeSenti = @"http://localhost:80/TwitterWizLocal/";//**@"http://www.csc.ncsu.edu/faculty/healey/tweet_viz/tweet_app/";

                item1.headerCloseIconVisibility = false;
                item1.sADivVisbility = true;

                // add a tabItem with + in header 
                osintItem item2 = new osintItem() { Header = "+" };
                item2.mySearch1 = "";
                item2.type = "add";
                item2.tab_number = "1";
                item2.headerCloseIconVisibility = false;

                osintBased.Add(item1);
                //**osintBased.Add(item2);

                // bind tab control
                tabDynamic1.DataContext = osintBased;
                tabDynamic1.SelectedIndex = 0;

                osintItem item11 = new osintItem() { Header = "Search123" };
                item11.Header = "Result";
                item11.mySearch1 = "";
                item11.type = "Result";
                item11.tab_number = "0";

                item11.sourceCodeSenti = @"http://localhost:80/GoogleWiz/";//**@"http://www.csc.ncsu.edu/faculty/healey/tweet_viz/tweet_app/";

                item11.headerCloseIconVisibility = false;
                item11.sADivVisbility = true;

                // add a tabItem with + in header 
                osintItem item22 = new osintItem() { Header = "+" };
                item22.mySearch1 = "";
                item22.type = "add";
                item22.tab_number = "1";
                item22.headerCloseIconVisibility = false;

                osintGoogleBased.Add(item11);
                //**osintGoogleBased.Add(item22);

                // bind tab control
                tabDynamicGoogle.DataContext = osintGoogleBased;
                tabDynamicGoogle.SelectedIndex = 0;

                osintItem item112 = new osintItem() { Header = "Search123" };
                item112.Header = "Result";
                item112.mySearch1 = "";
                item112.type = "Result";
                item112.tab_number = "0";

                item112.sourceCodeSenti = @"http://localhost:80/fbWiz/";//**@"http://www.csc.ncsu.edu/faculty/healey/tweet_viz/tweet_app/";

                item112.headerCloseIconVisibility = false;
                item112.sADivVisbility = true;

                // add a tabItem with + in header 
                osintItem item222 = new osintItem() { Header = "+" };
                item222.mySearch1 = "";
                item222.type = "add";
                item222.tab_number = "1";
                item222.headerCloseIconVisibility = false;

                osintfbBased.Add(item112);

                // bind tab control
                tabDynamicfb.DataContext = osintfbBased;
                tabDynamicfb.SelectedIndex = 0;


                osintItem item3 = new osintItem() { Header = "Search123" };
                item3.Header = "Result";
                item3.mySearch1 = "";
                item3.type = "Result";
                item3.tab_number = "0";

                //string curDir = Directory.GetCurrentDirectory();
                //string iii = curDir + @"\work\index.html";

                item3.sourceCodeSenti = @"http://localhost:80/analysis/";//iii;//@".\work\index.html";

                item3.headerCloseIconVisibility = false;
                item3.sADivVisbility = true;

                // add a tabItem with + in header 
                osintItem item4 = new osintItem() { Header = "+" };
                item4.mySearch1 = "";
                item4.type = "add";
                item4.tab_number = "1";
                item4.headerCloseIconVisibility = false;
                item4.sourceCodeSenti = @"http://localhost:80/analysis/";//iii;//@".\work\index.html";

                careSentimentBased.Add(item3);
                careSentimentBased.Add(item4);

                // bind tab control
                tabDynamic2.DataContext = careSentimentBased;
                tabDynamic2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            /**
            ListViewItemsCollections.Add(new ListViewItemsData()
            {
                GridViewColumnName_ImageSource = @"C:\Users\hussein\Documents\Visual Studio 2010\Projects\WpfApplication2\WpfApplication2\Resources\fb.png",
                GridViewColumnName_LabelContent = "Usman Sajid"
            });

            ListViewItemsCollections.Add(new ListViewItemsData()
            {
                GridViewColumnName_ImageSource = @"https://graph.facebook.com/828529127195657/picture",
                GridViewColumnName_LabelContent = "Usman Sajid"
            });

            fbUserList.ItemsSource = ListViewItemsCollections;

            crossIcon.Visibility = Visibility.Hidden;
            */

            myStaticObject = this;

        }//end of mainWindow...

        //twitterProfileUserHeaderInfoListItems
        public class twitterProfileUserHeaderInfoListItems
        {
            public string twitterProfileUserHeaderUserId { get; set; }
            public string twitterProfileUserHeaderUserName { get; set; }
            public string twitterProfileUserHeaderAge { get; set; }
            public string twitterProfileUserHeaderCityCountry { get; set; }
            public string twitterProfileUserHeaderAboutMe { get; set; }
        }
        public class fbUserListItems:INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            public string GridViewColumnName_ImageSource { get; set; }
            public string GridViewColumnName_LabelContent { get; set; }
            public string GridViewColumnName_ID { get; set; }
            public string GridViewColumnTags { get; set; }
            public string GridViewColumnLocation { get; set; }
            public string GridViewColumn_createdTime { get; set; }
            public string GridViewColumn_commentorId { get; set; }

            private bool _fbUserMoreOptionVisibility;
            public bool fbUserMoreOptionVisibility
            {
                get { return _fbUserMoreOptionVisibility; }
                set
                {
                    _fbUserMoreOptionVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbUserMoreOptionVisibility"));
                }
            }

            private String _fbUserMoreOptionText;
            public String fbUserMoreOptionText
            {
                get { return _fbUserMoreOptionText; }
                set
                {
                    _fbUserMoreOptionText = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbUserMoreOptionText"));
                }
            }

            private bool _fbUserMoreOptionBehindVisibility;
            public bool fbUserMoreOptionBehindVisibility
            {
                get { return _fbUserMoreOptionBehindVisibility; }
                set
                {
                    _fbUserMoreOptionBehindVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbUserMoreOptionBehindVisibility"));
                }
            }

        }
        public class fbGroupListItems:INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            public string fbGroup_ImageSource { get; set; }
            public string fbGroup_LabelContent { get; set; }
            public string fbGroup_ID { get; set; }
            public string fbGroupTags { get; set; }
            public string fbGroupLocation { get; set; }

            private bool _fbGroupMoreOptionVisibility;
            public bool fbGroupMoreOptionVisibility
            {
                get { return _fbGroupMoreOptionVisibility; }
                set
                {
                    _fbGroupMoreOptionVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbGroupMoreOptionVisibility"));
                }
            }

            private String _fbGroupMoreOptionText;
            public String fbGroupMoreOptionText
            {
                get { return _fbGroupMoreOptionText; }
                set
                {
                    _fbGroupMoreOptionText = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbGroupMoreOptionText"));
                }
            }

            private bool _fbGroupMoreOptionBehindVisibility;
            public bool fbGroupMoreOptionBehindVisibility
            {
                get { return _fbGroupMoreOptionBehindVisibility; }
                set
                {
                    _fbGroupMoreOptionBehindVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbGroupMoreOptionBehindVisibility"));
                }
            }

        }
        public class fbPageListItems:INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            public string fbPage_ImageSource { get; set; }
            public string fbPage_LabelContent { get; set; }
            public string fbPage_ID { get; set; }
            public string fbPageTags { get; set; }
            public string fbPageLocation { get; set; }

            private String _fbPage_BgColor;
            public String fbPage_BgColor
            {
                get { return _fbPage_BgColor; }
                set
                {
                    _fbPage_BgColor = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPage_BgColor"));
                }
            }

            private int _fbPage_myIndex;
            public int fbPage_myIndex
            {
                get { return _fbPage_myIndex; }
                set
                {
                    _fbPage_myIndex = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPage_myIndex"));
                }
            }

            private int _fbPage_SelectedUser;
            public int fbPage_SelectedUser
            {
                get { return _fbPage_SelectedUser; }
                set
                {
                    _fbPage_SelectedUser = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPage_SelectedUser"));
                }
            }

            private bool _fbPageMoreOptionVisibility;
            public bool fbPageMoreOptionVisibility
            {
                get { return _fbPageMoreOptionVisibility; }
                set
                {
                    _fbPageMoreOptionVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageMoreOptionVisibility"));
                }
            }

            private String _fbPageMoreOptionText;
            public String fbPageMoreOptionText
            {
                get { return _fbPageMoreOptionText; }
                set
                {
                    _fbPageMoreOptionText = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageMoreOptionText"));
                }
            }


            private bool _fbPageMoreBehindOptionVisibility;
            public bool fbPageMoreBehindOptionVisibility
            {
                get { return _fbPageMoreBehindOptionVisibility; }
                set
                {
                    _fbPageMoreBehindOptionVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageMoreBehindOptionVisibility"));
                }
            }
        }
        public class bookmarkListItems : INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            ////////////////////////////////////////////////////////
            // generic variables...

            public string GridViewColumnIconFbOrTwitter { get; set; }

            public string GridViewColumnType { get; set; }
            public string GridViewColumnSocialMedia{ get; set; }

            private bool _bookmarkMoreOptionVisibility;
            public bool bookmarkMoreOptionVisibility
            {
                get { return _bookmarkMoreOptionVisibility; }
                set
                {
                    _bookmarkMoreOptionVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("bookmarkMoreOptionVisibility"));
                }
            }

            private bool _bookmarkMoreOptionBehindVisibility;
            public bool bookmarkMoreOptionBehindVisibility
            {
                get { return _bookmarkMoreOptionBehindVisibility; }
                set
                {
                    _bookmarkMoreOptionBehindVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("bookmarkMoreOptionBehindVisibility"));
                }
            }

            //////////////////////////////////////////////////////////////////
            //fb page variables...

            public string fbPage_ImageSource { get; set; }
            public string fbPage_LabelContent { get; set; }
            public string fbPage_ID { get; set; }
            public string fbPageTags { get; set; }
            public string fbPageLocation { get; set; }

            //////////////////////////////////////////////////////////////////
            //fb group...

            public string fbGroup_ImageSource { get; set; }
            public string fbGroup_LabelContent { get; set; }
            public string fbGroup_ID { get; set; }
            public string fbGroupTags { get; set; }
            public string fbGroupLocation { get; set; }

            ///////////////////////////////////////////////////////////////////
            //twitteruser...
            private String _twitterUserProfileUrl;
            public String twitterUserProfileUrl
            {
                get { return _twitterUserProfileUrl; }
                set
                {
                    _twitterUserProfileUrl = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserProfileUrl"));
                }
            }

            public string GridViewColumnName_ImageSource { get; set; }
            public string GridViewColumnName_LabelContent { get; set; }
            public string GridViewColumnName_LabelContentScreenName { get; set; }

            public string GridViewColumnName_Age { get; set; }
            public string GridViewColumnName_CityCountry { get; set; }
            public string GridViewColumnName_AboutMe { get; set; }

            public string GridViewColumnName_FollowersCount { get; set; }
            public string GridViewColumnName_FollowingCount { get; set; }
            public string GridViewColumnName_tweetsCount { get; set; }

            public string GridViewColumnLocation { get; set; }
            public string GridViewColumnName_ID { get; set; }

            //////////////////////////////////////////////////////////////////
            // google user...

            private String _aboutMe;
            public String aboutMe
            {
                get { return _aboutMe; }
                set
                {
                    _aboutMe = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("aboutMe"));
                }
            }

            private String _Birthday;
            public String Birthday
            {
                get { return _Birthday; }
                set
                {
                    _Birthday = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Birthday"));
                }
            }

            private String _BraggingRights;
            public String BraggingRights
            {
                get { return _BraggingRights; }
                set
                {
                    _BraggingRights = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("BraggingRights"));
                }
            }

            private int _CircledByCount;
            public int CircledByCount
            {
                get { return _CircledByCount; }
                set
                {
                    _CircledByCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("CircledByCount"));
                }
            }

            private String _CurrentLocation;
            public String CurrentLocation
            {
                get { return _CurrentLocation; }
                set
                {
                    _CurrentLocation = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("CurrentLocation"));
                }
            }

            private String _DisplayName;
            public String DisplayName
            {
                get { return _DisplayName; }
                set
                {
                    _DisplayName = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("DisplayName"));
                }
            }

            private String _Domain;
            public String Domain
            {
                get { return _Domain; }
                set
                {
                    _Domain = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Domain"));
                }
            }

            private String _ETag;
            public String ETag
            {
                get { return _ETag; }
                set
                {
                    _ETag = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ETag"));
                }
            }

            private String _Gender;
            public String Gender
            {
                get { return _Gender; }
                set
                {
                    _Gender = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Gender"));
                }
            }

            private String _Id;
            public String Id
            {
                get { return _Id; }
                set
                {
                    _Id = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                }
            }

            private String _ImageUrl;
            public String ImageUrl
            {
                get { return _ImageUrl; }
                set
                {
                    _ImageUrl = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ImageUrl"));
                }
            }

            private bool _IsPlusUser;
            public bool IsPlusUser
            {
                get { return _IsPlusUser; }
                set
                {
                    _IsPlusUser = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("IsPlusUser"));
                }
            }

            private String _Kind;
            public String Kind
            {
                get { return _Kind; }
                set
                {
                    _Kind = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Kind"));
                }
            }

            private String _Language;
            public String Language
            {
                get { return _Language; }
                set
                {
                    _Language = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Language"));
                }
            }

            private String _NickName;
            public String NickName
            {
                get { return _NickName; }
                set
                {
                    _NickName = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("NickName"));
                }
            }

            private String _ObjectType;
            public String ObjectType
            {
                get { return _ObjectType; }
                set
                {
                    _ObjectType = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ObjectType"));
                }
            }

            private String _Occupation;
            public String Occupation
            {
                get { return _Occupation; }
                set
                {
                    _Occupation = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Occupation"));
                }
            }

            private int _PlusOneCount;
            public int PlusOneCount
            {
                get { return _PlusOneCount; }
                set
                {
                    _PlusOneCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("PlusOneCount"));
                }
            }

            private String _RelationshipStatus;
            public String RelationshipStatus
            {
                get { return _RelationshipStatus; }
                set
                {
                    _RelationshipStatus = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("RelationshipStatus"));
                }
            }

            private String _Skills;
            public String Skills
            {
                get { return _Skills; }
                set
                {
                    _Skills = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Skills"));
                }
            }

            private String _Tagline;
            public String Tagline
            {
                get { return _Tagline; }
                set
                {
                    _Tagline = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Tagline"));
                }
            }

            private String _Url;
            public String Url
            {
                get { return _Url; }
                set
                {
                    _Url = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Url"));
                }
            }

            private bool _Verified;
            public bool Verified
            {
                get { return _Verified; }
                set
                {
                    _Verified = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Verified"));
                }
            }

            //////////////////////////////////////////////////////////////////
        }
        public class fbPageTabInfo
        {
            public string fbPageTabInfo_ImageSource { get; set; }
            public string fbPageTabInfo_LabelContent { get; set; }
            public string fbPageTabInfo_ID { get; set; }
            public string fbPageTabInfo_desc { get; set; }
            public string fbPageTabInfo_location { get; set; }
            public string fbPageTabInfo_likesCount { get; set; }
            public string fbPageTabInfo_talkingAboutCount { get; set; }
        }
        public class fbPageTabListItems:INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            public string fbPageTab_ImageSource { get; set; }
            public string fbPageTab_LabelContent { get; set; }
            public string fbPageTab_ID { get; set; }
            public string fbPageTab_PhotoUrl { get; set; }
            public string fbPageTab_desc { get; set; }
            public string fbPageTab_type { get; set; }

            public string fbPageTab_likesCount { get; set; }
            public string fbPageTab_commentCount { get; set; }
            public string fbPageTab_shareCount{ get; set; }

            public string fbPageTab_createdTime { get; set; }
            public string fbPageTab_updatedTime { get; set; }

            private bool _fbPageTab_dataImgVisibility;
            public bool fbPageTab_dataImgVisibility
            {
                get { return _fbPageTab_dataImgVisibility; }
                set
                {
                    _fbPageTab_dataImgVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("fbPageTab_dataImgVisibility"));
                }
            }

        }
        public class googleUserPostsListItems : INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            private String _Access;
            public String Access
            {
                get { return _Access; }
                set
                {
                    _Access = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Access"));
                }
            }

            private String _ActorDisplayName;
            public String ActorDisplayName
            {
                get { return _ActorDisplayName; }
                set
                {
                    _ActorDisplayName = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ActorDisplayName"));
                }
            }

            private String _ActorId;
            public String ActorId
            {
                get { return _ActorId; }
                set
                {
                    _ActorId = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ActorId"));
                }
            }

            private string _ActorDp;
            public string ActorDp
            {
                get { return _ActorDp; }
                set
                {
                    _ActorDp = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ActorDp"));
                }
            }

            private String _Address;
            public String Address
            {
                get { return _Address; }
                set
                {
                    _Address = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Address"));
                }
            }

            private String _Annotation;
            public String Annotation
            {
                get { return _Annotation; }
                set
                {
                    _Annotation = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Annotation"));
                }
            }

            private String _CrosspostSource;
            public String CrosspostSource
            {
                get { return _CrosspostSource; }
                set
                {
                    _CrosspostSource = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("CrosspostSource"));
                }
            }

            private String _ETag;
            public String ETag
            {
                get { return _ETag; }
                set
                {
                    _ETag = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ETag"));
                }
            }

            private String _Geocode;
            public String Geocode
            {
                get { return _Geocode; }
                set
                {
                    _Geocode = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Geocode"));
                }
            }

            private String _Id;
            public String Id
            {
                get { return _Id; }
                set
                {
                    _Id = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                }
            }

            private String _ImageUrl;
            public String ImageUrl
            {
                get { return _ImageUrl; }
                set
                {
                    _ImageUrl = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ImageUrl"));
                }
            }
            
            private String _Kind;
            public String Kind
            {
                get { return _Kind; }
                set
                {
                    _Kind = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Kind"));
                }
            }

            private String _Location;
            public String Location
            {
                get { return _Location; }
                set
                {
                    _Location = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Location"));
                }
            }

            private String _plusOneCount;
            public String plusOneCount
            {
                get { return _plusOneCount; }
                set
                {
                    _plusOneCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("plusOneCount"));
                }
            }

            private String _repliesCount;
            public String repliesCount
            {
                get { return _repliesCount; }
                set
                {
                    _repliesCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("repliesCount"));
                }
            }

            private String _ResharersCount;
            public String ResharersCount
            {
                get { return _ResharersCount; }
                set
                {
                    _ResharersCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ResharersCount"));
                }
            }

            private string _PlaceId;
            public string PlaceId
            {
                get { return _PlaceId; }
                set
                {
                    _PlaceId = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("PlaceId"));
                }
            }

            private String _PlaceName;
            public String PlaceName
            {
                get { return _PlaceName; }
                set
                {
                    _PlaceName = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("PlaceName"));
                }
            }

            private String _Provider;
            public String Provider
            {
                get { return _Provider; }
                set
                {
                    _Provider = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Provider"));
                }
            }

            private String _Published;
            public String Published
            {
                get { return _Published; }
                set
                {
                    _Published = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Published"));
                }
            }

            private string _PublishedRaw;
            public string PublishedRaw
            {
                get { return _PublishedRaw; }
                set
                {
                    _PublishedRaw = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("PublishedRaw"));
                }
            }

            private string _Radius;
            public string Radius
            {
                get { return _Radius; }
                set
                {
                    _Radius = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Radius"));
                }
            }

            private String _Title;
            public String Title
            {
                get { return _Title; }
                set
                {
                    _Title = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Title"));
                }
            }

            private String _updated;
            public String updated
            {
                get { return _updated; }
                set
                {
                    _updated = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("updated"));
                }
            }

            private String _updatedRaw;
            public String updatedRaw
            {
                get { return _updatedRaw; }
                set
                {
                    _updatedRaw = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("updatedRaw"));
                }
            }

            private String _Url;
            public String Url
            {
                get { return _Url; }
                set
                {
                    _Url = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Url"));
                }
            }

            private String _Verb;
            public String Verb
            {
                get { return _Verb; }
                set
                {
                    _Verb = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Verb"));
                }
            }

            private bool _googleUserTab_dataImgVisibility;
            public bool googleUserTab_dataImgVisibility
            {
                get { return _googleUserTab_dataImgVisibility; }
                set
                {
                    _googleUserTab_dataImgVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleUserTab_dataImgVisibility"));
                }
            }

        }//class...
        public class twitterUserListItems : INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            public string GridViewColumnName_ImageSource { get; set; }
            public string GridViewColumnName_LabelContent { get; set; }
            public string GridViewColumnName_LabelContentScreenName { get; set; }

            public string GridViewColumnName_Age { get; set; }
            public string GridViewColumnName_CityCountry { get; set; }
            public string GridViewColumnName_AboutMe { get; set; }

            public string GridViewColumnName_FollowersCount { get; set; }
            public string GridViewColumnName_FollowingCount { get; set; }
            public string GridViewColumnName_tweetsCount{ get; set; }

            public string GridViewColumnLocation { get; set; }
            public string GridViewColumnName_ID { get; set; }

            private String _GridViewColumn_Url;
            public String GridViewColumn_Url
            {
                get { return _GridViewColumn_Url; }
                set
                {
                    _GridViewColumn_Url = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("GridViewColumn_Url"));
                }
            }

            private String _GridViewColumn_BgColor;
            public String GridViewColumn_BgColor
            {
                get { return _GridViewColumn_BgColor; }
                set
                {
                    _GridViewColumn_BgColor = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("GridViewColumn_BgColor"));
                }
            }

            private int _GridViewColumnName_myIndex;
            public int GridViewColumnName_myIndex
            {
                get { return _GridViewColumnName_myIndex; }
                set
                {
                    _GridViewColumnName_myIndex = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("GridViewColumnName_myIndex"));
                }
            }

            private int _GridViewColumnName_SelectedUser;
            public int GridViewColumnName_SelectedUser
            {
                get { return _GridViewColumnName_SelectedUser; }
                set
                {
                    _GridViewColumnName_SelectedUser = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("GridViewColumnName_SelectedUser"));
                }
            }

            private bool _twitterUserMoreOptionVisibility;
            public bool twitterUserMoreOptionVisibility
            {
                get { return _twitterUserMoreOptionVisibility; }
                set
                {
                    _twitterUserMoreOptionVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserMoreOptionVisibility"));
                }
            }

            private String _twitterUserMoreOptionText;
            public String twitterUserMoreOptionText
            {
                get { return _twitterUserMoreOptionText; }
                set
                {
                    _twitterUserMoreOptionText = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserMoreOptionText"));
                }
            }

            private bool _twitterUserMoreOptionBehindVisibility;
            public bool twitterUserMoreOptionBehindVisibility
            {
                get { return _twitterUserMoreOptionBehindVisibility; }
                set
                {
                    _twitterUserMoreOptionBehindVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserMoreOptionBehindVisibility"));
                }
            }

        }//class...
        public class googleUserListItems : INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            private String _aboutMe;
            public String aboutMe
            {
                get { return _aboutMe; }
                set
                {
                    _aboutMe = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("aboutMe"));
                }
            }

            private String _Birthday;
            public String Birthday
            {
                get { return _Birthday; }
                set
                {
                    _Birthday = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Birthday"));
                }
            }

            private String _BraggingRights;
            public String BraggingRights
            {
                get { return _BraggingRights; }
                set
                {
                    _BraggingRights = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("BraggingRights"));
                }
            }

            private int _CircledByCount;
            public int CircledByCount
            {
                get { return _CircledByCount; }
                set
                {
                    _CircledByCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("CircledByCount"));
                }
            }

            private String _CurrentLocation;
            public String CurrentLocation
            {
                get { return _CurrentLocation; }
                set
                {
                    _CurrentLocation = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("CurrentLocation"));
                }
            }

            private String _DisplayName;
            public String DisplayName
            {
                get { return _DisplayName; }
                set
                {
                    _DisplayName = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("DisplayName"));
                }
            }

            private String _Domain;
            public String Domain
            {
                get { return _Domain; }
                set
                {
                    _Domain = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Domain"));
                }
            }

            private String _ETag;
            public String ETag
            {
                get { return _ETag; }
                set
                {
                    _ETag = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ETag"));
                }
            }

            private String _Gender;
            public String Gender
            {
                get { return _Gender; }
                set
                {
                    _Gender = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Gender"));
                }
            }

            private String _Id;
            public String Id
            {
                get { return _Id; }
                set
                {
                    _Id= value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                }
            }

            private String _ImageUrl;
            public String ImageUrl
            {
                get { return _ImageUrl; }
                set
                {
                    _ImageUrl = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ImageUrl"));
                }
            }

            private bool _IsPlusUser;
            public bool IsPlusUser
            {
                get { return _IsPlusUser; }
                set
                {
                    _IsPlusUser = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("IsPlusUser"));
                }
            }

            private String _Kind;
            public String Kind
            {
                get { return _Kind; }
                set
                {
                    _Kind = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Kind"));
                }
            }

            private String _Language;
            public String Language
            {
                get { return _Language; }
                set
                {
                    _Language = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Language"));
                }
            }

            private String _NickName;
            public String NickName
            {
                get { return _NickName; }
                set
                {
                    _NickName = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("NickName"));
                }
            }

            private String _ObjectType;
            public String ObjectType
            {
                get { return _ObjectType; }
                set
                {
                    _ObjectType = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ObjectType"));
                }
            }

            private String _Occupation;
            public String Occupation
            {
                get { return _Occupation; }
                set
                {
                    _Occupation = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Occupation"));
                }
            }

            private int _PlusOneCount;
            public int PlusOneCount
            {
                get { return _PlusOneCount; }
                set
                {
                    _PlusOneCount = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("PlusOneCount"));
                }
            }

            private String _RelationshipStatus;
            public String RelationshipStatus
            {
                get { return _RelationshipStatus; }
                set
                {
                    _RelationshipStatus = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("RelationshipStatus"));
                }
            }

            private String _Skills;
            public String Skills
            {
                get { return _Skills; }
                set
                {
                    _Skills = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Skills"));
                }
            }

            private String _Tagline;
            public String Tagline
            {
                get { return _Tagline; }
                set
                {
                    _Tagline = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Tagline"));
                }
            }

            private String _Url;
            public String Url
            {
                get { return _Url; }
                set
                {
                    _Url = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Url"));
                }
            }

            private bool _Verified;
            public bool Verified
            {
                get { return _Verified; }
                set
                {
                    _Verified = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Verified"));
                }
            }

            private String _GridViewColumn_BgColor;
            public String GridViewColumn_BgColor
            {
                get { return _GridViewColumn_BgColor; }
                set
                {
                    _GridViewColumn_BgColor = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("GridViewColumn_BgColor"));
                }
            }

            private int _GridViewColumnName_myIndex;
            public int GridViewColumnName_myIndex
            {
                get { return _GridViewColumnName_myIndex; }
                set
                {
                    _GridViewColumnName_myIndex = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("GridViewColumnName_myIndex"));
                }
            }

            private int _GridViewColumnName_SelectedUser;
            public int GridViewColumnName_SelectedUser
            {
                get { return _GridViewColumnName_SelectedUser; }
                set
                {
                    _GridViewColumnName_SelectedUser = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("GridViewColumnName_SelectedUser"));
                }
            }

            private bool _googleUserMoreOptionVisibility;
            public bool googleUserMoreOptionVisibility
            {
                get { return _googleUserMoreOptionVisibility; }
                set
                {
                    _googleUserMoreOptionVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleUserMoreOptionVisibility"));
                }
            }

            private String _googleUserMoreOptionText;
            public String googleUserMoreOptionText
            {
                get { return _googleUserMoreOptionText; }
                set
                {
                    _googleUserMoreOptionText = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleUserMoreOptionText"));
                }
            }

            private bool _googleUserMoreOptionBehindVisibility;
            public bool googleUserMoreOptionBehindVisibility
            {
                get { return _googleUserMoreOptionBehindVisibility; }
                set
                {
                    _googleUserMoreOptionBehindVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("googleUserMoreOptionBehindVisibility"));
                }
            }

        }//class...
        public class twitterUserTweetListItems:INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            public string twitterUserTweetListItems_Id { get; set; }
            public string twitterUserTweetListItems_desc { get; set; }
            public string twitterUserTweetListItems_name { get; set; }

            public string twitterUserTweetListItems_screenName { get; set; }
            public string twitterUserTweetListItems_dateTime { get; set; }
            public string twitterUserTweetListItems_userDp { get; set; }

            public string twitterUserTweetListItems_retweetCount { get; set; }
            public string twitterUserTweetListItems_Photo { get; set; }

            private bool _twitterUserTweetListItems_PhotoVisibility;
            public bool twitterUserTweetListItems_PhotoVisibility
            {
                get { return _twitterUserTweetListItems_PhotoVisibility; }
                set
                {
                    _twitterUserTweetListItems_PhotoVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserTweetListItems_PhotoVisibility"));
                }
            }

            private String _twitterUserTweetListItems_reportButtonText;
            public String twitterUserTweetListItems_reportButtonText
            {
                get { return _twitterUserTweetListItems_reportButtonText; }
                set
                {
                    _twitterUserTweetListItems_reportButtonText = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserTweetListItems_reportButtonText"));
                }
            }

            private String _twitterUserTweetListItems_number;
            public String twitterUserTweetListItems_number
            {
                get { return _twitterUserTweetListItems_number; }
                set
                {
                    _twitterUserTweetListItems_number = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterUserTweetListItems_number"));
                }
            }

            public string twitterUserTweetListItems_favoriteCount { get; set; }
        }
        public class twitterTweetPageExpanderListItems
        {
            public string twitterTweetPageExpanderListItems_ImageSource { get; set; }
            public string twitterTweetPageExpanderListItems_LabelContent { get; set; }
            public string twitterTweetPageExpanderListItems_LabelContentScreenName { get; set; }

            public string twitterTweetPageExpanderListItems_Age { get; set; }
            public string twitterTweetPageExpanderListItems_CityCountry { get; set; }
            public string twitterTweetPageExpanderListItems_AboutMe { get; set; }

            public string twitterTweetPageExpanderListItems_FollowersCount { get; set; }
            public string twitterTweetPageExpanderListItems_FollowingCount { get; set; }
            public string twitterTweetPageExpanderListItems_tweetsCount { get; set; }

            public string twitterTweetPageExpanderListItems_ID { get; set; }
            public string twitterTweetPageExpanderListItems_BackgroundColor { get; set; }
        }
        public class twitterTweetPageListItems
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            public string twitterTweetPageListItems_Id { get; set; }
            public string twitterTweetPageListItems_desc { get; set; }
            public string twitterTweetPageListItems_name { get; set; }

            public string twitterTweetPageListItems_screenName { get; set; }
            public string twitterTweetPageListItems_dateTime { get; set; }
            public string twitterTweetPageListItems_userDp { get; set; }

            public string twitterTweetPageListItems_retweetCount { get; set; }
            public string twitterTweetPageListItems_favoriteCount { get; set; }

            private bool _twitterTweetsPhotoVisibility;
            public bool twitterTweetsPhotoVisibility
            {
                get { return _twitterTweetsPhotoVisibility; }
                set
                {
                    _twitterTweetsPhotoVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterTweetsPhotoVisibility"));
                }
            }

            private String _twitterTweetsPhoto;
            public String twitterTweetsPhoto
            {
                get { return _twitterTweetsPhoto; }
                set
                {
                    _twitterTweetsPhoto = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterTweetsPhoto"));
                }
            }

        }
        public class twitterTweetListItems:INotifyPropertyChanged
        {
            //is is all that the interface requires
            public event PropertyChangedEventHandler PropertyChanged;

            public string userId { get; set; }
            public string desc { get; set; }

            public string dateTime { get; set; }
            public string userScreenName { get; set;} 

            public string tweetId { get; set; }
            public string userName { get; set; }

            public long tweetIdLong { get; set; }


            public string userProfileImageUrl { get; set; }
            public string tweetLocation { get; set; }

            //twitterTweetMoreOptionVisibility = true,
                               //twitterTweetsMoreOptionBehindVisibility = false,
                               //twitterTweetMoreOptionText = "more"

            private bool _twitterTweetsMoreOptionVisibility;
            public bool twitterTweetsMoreOptionVisibility
            {
                get { return _twitterTweetsMoreOptionVisibility; }
                set
                {
                    _twitterTweetsMoreOptionVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterTweetsMoreOptionVisibility"));
                }
            }

            private bool _twitterTweetsMoreOptionBehindVisibility;
            public bool twitterTweetsMoreOptionBehindVisibility
            {
                get { return _twitterTweetsMoreOptionBehindVisibility; }
                set
                {
                    _twitterTweetsMoreOptionBehindVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterTweetsMoreOptionBehindVisibility"));
                }
            }

            private String _twitterTweetsMoreOptionText;
            public String twitterTweetsMoreOptionText
            {
                get { return _twitterTweetsMoreOptionText; }
                set
                {
                    _twitterTweetsMoreOptionText= value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterTweetsMoreOptionText"));
                }
            }

            private bool _twitterTweetsPhotoVisibility;
            public bool twitterTweetsPhotoVisibility
            {
                get { return _twitterTweetsPhotoVisibility; }
                set
                {
                    _twitterTweetsPhotoVisibility = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterTweetsPhotoVisibility"));
                }
            }

            private String _twitterTweetsPhoto;
            public String twitterTweetsPhoto
            {
                get { return _twitterTweetsPhoto; }
                set
                {
                    _twitterTweetsPhoto = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("twitterTweetsPhoto"));
                }
            }

        }

        private TabItem AddTabItem(int which_one)
        {
            //which_one==0 -> search window
            //which_one==1 -> facebook
            //which_one -> twitter

            int count = _tabItems.Count;

            // create new tab item
            TabItem tab = new TabItem();

            tab.Header = string.Format("Tab {0}", count);
            tab.Name = string.Format("tab{0}", count);
            tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;

            tab.MouseDoubleClick += new MouseButtonEventHandler(tab_MouseDoubleClick);

            // add controls to tab item, this case I added just a textbox
            //TextBox txt = new TextBox();
            //txt.Name = "txt";
            
            //tab.Content = txt;
            
            // insert tab item right before the last (+) tab item
            _tabItems.Insert(count - 1, tab);

            return tab;
        }

        private void tabAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            /**
            // clear tab control binding
            tabDynamic.DataContext = null;

            TabItem tab = this.AddTabItem(0);

            // bind tab control
            tabDynamic.DataContext = _tabItems;

            // select newly added tab item
            tabDynamic.SelectedItem = tab;*/

            MessageBox.Show("aaaaaaaaaaaa");
            e.Handled = true;
        }

        private void tab_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /**TabItem tab = sender as TabItem;

            TabProperty dlg = new TabProperty();

            // get existing header text
            dlg.txtTitle.Text = tab.Header.ToString();

            if (dlg.ShowDialog() == true)
            {
                // change header text
                tab.Header = dlg.txtTitle.Text.Trim();
            }*/
        }

        private void tabDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*TabControl ddd = sender as TabControl;
            var ddsds=ddd.SelectedItem as TabItem;
            var tab = tabDynamic.Items[tabDynamic.SelectedIndex] as Grid;
            DockPanel twitterDiv = tab.FindName("TwitterProfileTab") as DockPanel;
            DockPanel searchDiv = tab.FindName("searchTab") as DockPanel;

            String tab_number = tab.Tag.ToString();

            if (Countries[Convert.ToInt32(tab_number)].type.Equals("search"))
            {
                searchDiv.Visibility = Visibility.Visible;
                twitterDiv.Visibility = Visibility.Hidden;
            }
            else if (Countries[Convert.ToInt32(tab_number)].type.Equals("twitterUserProfile"))
            {
                twitterDiv.Visibility = Visibility.Visible;
                searchDiv.Visibility = Visibility.Hidden;
            }
            else
            {
                searchDiv.Visibility = Visibility.Visible;
                twitterDiv.Visibility = Visibility.Hidden;
            }//else...*/
            //e.Handled = false;
            /*
            TabItem tab = tabDynamic.SelectedItem as TabItem;
            if (tab == null) return;
            
            if (tab.Equals(_tabAdd))
            {
                // clear tab control binding
                tabDynamic.DataContext = null;

                TabItem newTab = this.AddTabItem(0);

                // bind tab control
                tabDynamic.DataContext = _tabItems;

                // select newly added tab item
                tabDynamic.SelectedItem = newTab;
            }
            else
            {
                // your code here...
            }*/

            // add a tabItem with + in header 
            /**aTabItem plusOne = new aTabItem() { Header = "Search" };
            plusOne.mySearch = "";
            plusOne.type = "search";
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = true;

            // tabAdd.MouseLeftButtonUp += new MouseButtonEventHandler(tabAdd_MouseLeftButtonUp);

            // add first tab
            //this.AddTabItem(0);

            Countries.Insert(tabDynamic.Items.Count-1,plusOne);
            
            if(tabDynamic.Items.Count>=2)
            tabDynamic.SelectedIndex = tabDynamic.Items.Count - 2;
            else
                tabDynamic.SelectedIndex = tabDynamic.Items.Count - 1;*/
            //tabDynamic.SelectedIndex = 0;
            //MessageBox.Show("selection changed");
        }
     
        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                //Do your stuff
                MessageBox.Show("hello");
            }
        }//end of func...

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;

            if (but != null && but.Tag != null)
            {
                int indexToDelete = Convert.ToInt32(but.Tag.ToString());
                String indexToDeleteStr = indexToDelete.ToString();

                int sI = -1; //tabDynamic.SelectedIndex;

                int i = 0;

                foreach (aTabItem item in Countries)
                {
                    if (item.tab_number == indexToDeleteStr) { sI = i; break; }
                    i++;
                }//foreach...

                if (sI >= 0)
                {
                    Countries.RemoveAt(sI);
                    tabDynamic.SelectedIndex = sI - 1;
                }//if(sI>=0)...

            }//if(but!=null&&but.Tag!=null)...
        }//func...

        private void btnDelete1_Click(object sender, RoutedEventArgs e)
        {
            int sI = tabDynamic1.SelectedIndex;
            osintBased.RemoveAt(sI);

            tabDynamic1.SelectedIndex = sI - 1;
        }

        private void btnDelete2_Click(object sender, RoutedEventArgs e)
        {
            int sI = tabDynamic2.SelectedIndex;
            careSentimentBased.RemoveAt(sI);

            tabDynamic2.SelectedIndex = sI - 1;
        }

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key== Key.Enter)
            {
                TextBox sTB = sender as TextBox;
                DockPanel the_parent = (((sTB.Parent as Grid).Parent as StackPanel).Parent as Border).Parent as DockPanel;

                ListView fbUserList = the_parent.FindName("fbUserList") as ListView;
                ListView twitterUserList = the_parent.FindName("twitterUserList") as ListView;

                //lll.Visibility = Visibility.Hidden;
                UniformGrid uG=the_parent.Children[2] as UniformGrid;
                UniformGrid uG1 = uG.Children[0] as UniformGrid;
                DockPanel dP = uG1.Children[0] as DockPanel;
                
                if (sTB.Text.Trim().Length > 0)
                {
                    Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].fbUserListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].googleUserListCollections.Clear();

                    Countries[tabDynamic.SelectedIndex].fbGroupLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].fbPageLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].fbUserLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].twitterTweetLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].twitterUserLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].googleActivitiesLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].googleUserLoadingImageVisbility = true;

                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForGoogleActivitiesSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForGoogleUserSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForFbUserSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForFbPageSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForFbGroupSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForTwitterTweetSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForTwitterUserSearchVisibility = false;

                    searchTextBoxText = sTB.Text.Trim();

                    Countries[tabDynamic.SelectedIndex].mySearch = sTB.Text;
                    Countries[tabDynamic.SelectedIndex].lastSearch = sTB.Text;
                    Countries[tabDynamic.SelectedIndex].Header = searchTextBoxText;

                    searchInParallelThread = new Thread(searchInParallel);

                    tabIndexiiii = tabDynamic.SelectedIndex;

                    //fbDateTimeConverter("2015-08-31T12:25:55+0000");

                    searchInParallelThread.Start();

                }//searchTBox.Text.Trim().Length > 0)...
                else
                {
                    MessageBox.Show("Please enter keyword to search...");
                }//else...

                e.Handled = true;

            }//if enter pressed...
        }

        public String fbDateTimeConverter(String inputDateTime)
        {
            String outputDateTime = inputDateTime;

            try
            {
                int year = Convert.ToInt32(inputDateTime.Substring(0, 4));
                int month = Convert.ToInt32(inputDateTime.Substring(5, 2));
                int day = Convert.ToInt32(inputDateTime.Substring(8, 2));

                int hours = Convert.ToInt32(inputDateTime.Substring(11, 2));
                int minutes = Convert.ToInt32(inputDateTime.Substring(14, 2));
                int seconds = Convert.ToInt32(inputDateTime.Substring(17, 2));

                DateTime dT = new DateTime(year, month, day, hours, minutes, seconds);

                dT = dT.AddHours(5);

                outputDateTime = dT.ToShortDateString() + "T" + dT.ToString("HH:mm:ss") + "+0000";

            }//try...
            catch 
            {

            }//catch...

            return outputDateTime;

        }//func...


        public String twitterDateTimeConverter(String inputDateTime)
        {
            String outputDateTime = inputDateTime;

            try
            {
                int year = Convert.ToInt32(inputDateTime.Substring(0, 4));
                int month = Convert.ToInt32(inputDateTime.Substring(5, 2));
                int day = Convert.ToInt32(inputDateTime.Substring(8, 2));

                int hours = Convert.ToInt32(inputDateTime.Substring(11, 2));
                int minutes = Convert.ToInt32(inputDateTime.Substring(14, 2));
                int seconds = Convert.ToInt32(inputDateTime.Substring(17, 2));

                DateTime dT = new DateTime(year, month, day, hours, minutes, seconds);

                dT = dT.AddHours(5);

                outputDateTime = dT.ToShortDateString() + "T" + dT.ToString("HH:mm:ss") + "+0000";

            }//try...
            catch
            {

            }//catch...

            return outputDateTime;

        }//func...

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            /**
            Grid tab = sender as Grid;
            DockPanel twitterDiv = tab.FindName("TwitterProfileTab") as DockPanel;
            DockPanel searchDiv = tab.FindName("searchTab") as DockPanel;

            String tab_number = tab.Tag.ToString();

            if (Countries[Convert.ToInt32(tab_number)].type.Equals("search"))
            {
                searchDiv.Visibility = Visibility.Visible;
                twitterDiv.Visibility = Visibility.Hidden;
            }
            else if (Countries[Convert.ToInt32(tab_number)].type.Equals("twitterUserProfile"))
            {
                twitterDiv.Visibility = Visibility.Visible;
                searchDiv.Visibility = Visibility.Hidden;
            }
            else
            {
                searchDiv.Visibility = Visibility.Visible;
                twitterDiv.Visibility = Visibility.Hidden;
            }//else...
            */
        }

        private void MainGrid_Initialized(object sender, EventArgs e)
        {
            /*
            Grid tab = sender as Grid;
            DockPanel twitterDiv=tab.FindName("TwitterProfileTab") as DockPanel;
            DockPanel searchDiv = tab.FindName("searchTab") as DockPanel;
            
            String tab_number = tab.Tag.ToString();

            if (Countries[Convert.ToInt32(tab_number)].type.Equals("search"))
            {
                searchDiv.Visibility = Visibility.Visible; 
                twitterDiv.Visibility = Visibility.Hidden; 
            }
            else if (Countries[Convert.ToInt32(tab_number)].type.Equals("twitterUserProfile"))
            {
                twitterDiv.Visibility = Visibility.Visible; 
                searchDiv.Visibility = Visibility.Hidden; 
            }
            else
            {
                searchDiv.Visibility = Visibility.Visible;
                twitterDiv.Visibility = Visibility.Hidden;
            }//else...
            */
        }

        private void tabDynamic_Loaded(object sender, RoutedEventArgs e)
        {
           // TabControl tc=sender as TabControl;
            //bool sdsads = tabDynamic.;
           // int fff = tabDynamic.Items.Count;
           //// aTabItem ti = tc.Items[0] as aTabItem;
            
           // Grid tab = sender as Grid;
        }//end of func...

        #region SocialNetworkAPIs
        public class GoogleSearch
        {
            //** screen_name , id , display_name **//     
            //############## Data Store Variables ####################
            public IList<Person> Search_Person;
            public IList<Person> Get_AllPeople;
            public PeopleResource.GetRequest personRequest;
            public IList<Person> Get_AllPeopleActivity;
            public IList<Activity> Get_AllActivities;
            public Activity Act;
            public IList<Activity> Get_SearchedActivities;
            public IList<Comment> Get_AllComments;
            public ICollection<KeyValuePair<String, String>> ResultSearch = new Dictionary<String, String>();
            //############# service related variables #################
            public Comment Comments;
            public string[] scopes;
            public UserCredential credential;
            public PlusService Service;
            //#########################################################

            public GoogleSearch(String ClientID, String ClientSecret)
            {
                // string filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Daimto.Auth.Store\Google.Apis.Auth.OAuth2.Responses.TokenResponse-" + Environment.UserName;
                // var localfileDatastore = new LocalFileDataStore(filename);
                // Service = Diamto.Authentication.Authenticaton.AuthenticateOauth(ClientID, ClientSecret, "CARE OSINT", localfileDatastore);
                scopes = new string[] {
                PlusService.Scope.PlusLogin,
                PlusService.Scope.UserinfoEmail,
                PlusService.Scope.UserinfoProfile ,"profile" };
                try
                {

                    // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets { ClientId = ClientID, ClientSecret = ClientSecret },
                                                                                         scopes,
                                                                                         Environment.UserName,
                                                                                         CancellationToken.None,
                                                                                         new FileDataStore("Daimto.GooglePlusm.Auth.Store")).Result;
                }
                catch (Exception ex)
                {
                }

                Service = new PlusService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Google Plus Sample",

                });
            }

            public IList<Person> GP_UserSearchByName(string query,int count,string nextPageToken)
            {
               try
                {
                    Search_Person = DaimtoGooglePlusHelper.SearchPeopleLimitedPaging(Service, query, 1, 50, nextPageToken);

                    return Search_Person;
                }
                catch { return null; }

            }

            public IList<Person> GP_GetAllPeople(string _userId)
            {
                try
                {
                    Get_AllPeople = DaimtoGooglePlusHelper.GetAllPeople(Service, _userId);
                    return Get_AllPeople;
                }
                catch
                {
                    return null;
                }
            }

            public Person GP_GetPerson(string _userId)
            {
                personRequest = Service.People.Get(_userId);
                return personRequest.Execute();
            }

            public IList<Person> GP_PeopleListByActivity(string _activityId, PeopleResource.ListByActivityRequest.CollectionEnum _type)
            {
                try
                {
                    Get_AllPeopleActivity = DaimtoGooglePlusHelper.PeopleListByActivity(Service, _activityId, _type);
                    return Get_AllPeopleActivity;
                }
                catch
                {
                    return null;
                }
            }

            public IList<Activity> GP_GetAllActivities(PlusService service, string _userId,int count,String nextPageToken)
            {

                try
                {
                    Get_AllActivities = DaimtoGooglePlusHelper.GetAllActivitiesPaging(service, _userId,1,20,nextPageToken);
                    return Get_AllActivities;
                }
                catch
                {
                    return null;
                }
            }

            public Activity GP_GetActivity(PlusService service, string _activityId)
            {

                try
                {
                    Act = DaimtoGooglePlusHelper.GetActivity(Service, _activityId);
                    return Act;
                }
                catch
                {
                    return null;
                }
            }

            public IList<Activity> GP_SearchActivities(PlusService service, string _query,int count,string nextPageToken)
            {
                try
                {
                    Get_SearchedActivities = DaimtoGooglePlusHelper.SearchActivitiesPaging(service, _query,1,count,nextPageToken);
                    return Get_SearchedActivities;
                }
                catch
                {
                    return null;
                }
            }

            public IList<Comment> GP_GetAllComments(PlusService service, string _activityId)
            {

                try
                {
                    Get_AllComments = DaimtoGooglePlusHelper.GetAllComments(service, _activityId);
                    return Get_AllComments;
                }
                catch
                {
                    return null;
                }
            }

            public Comment GP_Getcomment(PlusService service, string _commentId)
            {
                try
                {
                    Comments = DaimtoGooglePlusHelper.Getcomment(service, _commentId);
                    return Comments;
                }
                catch
                {
                    return null;
                }
            }

        }//class...

        public class TwitterSearch123445
        {
            public dynamic TwitterData;
            public dynamic TwitterClient;
            public dynamic TweetsListUser;
            public dynamic TweetsFollowers;
            public dynamic UserFollowers;
            public dynamic ListFollowerIds;

            public string ScreenName { get; set; }
            public string ID { get; set; }
            public dynamic Type { get; set; }
            public string AccessToken { get; set; }
            public string Link { get; set; }
            public string ImageLink { get; set; }
            public string CreatedTime { get; set; }
            public string UpdatedTime { get; set; }
            public String consumerKey { get; set; }
            public String consumerSecret { get; set; }
            public String accessToken { get; set; }
            public String accessTokenSecret { get; set; }
            public ICollection<KeyValuePair<String, Dictionary<string, string>>> ResultSearch = new Dictionary<String, Dictionary<string, string>>();
            public List<Tuple<String, String, String, String, String, String, String>> ResultSearchTweet = new List<Tuple<String, String, String, String, String, String, String>>();


            public TwitterSearch123445(String consumerKey, String consumerSecret, String accessToken, String accessTokenSecret)  // constructor
            {
                //************* new instantiations ************

                Link = "";
                UpdatedTime = "";
                CreatedTime = "";
                ScreenName = "";
                ID = "";
                ImageLink = "";
                ResultSearch = new Dictionary<string, Dictionary<string, string>>();
                Type = "";
                this.accessToken = accessToken;
                this.accessTokenSecret = accessTokenSecret;
                this.consumerKey = consumerKey;
                this.consumerSecret = consumerSecret;

            }
            public TwitterSearch123445()  // constructor
            {
                //************* new instantiations ************
                Link = "";
                CreatedTime = "";
                UpdatedTime = "";
                ScreenName = "";
                ID = "";
                ImageLink = "";
                Type = "";

            }
            public void TW_UserSearchByName(string name, string count)
            {
                TwitterService twitterService = new TwitterService();
                twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);

                var tweets_search = twitterService.SearchForUser(new SearchForUserOptions { Q = name.ToString(), Count = Convert.ToInt32(count) });
                try
                {
                    foreach (var tweet in tweets_search)
                    {
                        try
                        {
                            Dictionary<string, string> lt = new Dictionary<string, string>();
                            lt.Add("id", tweet.Id.ToString()); lt.Add("name", tweet.Name); lt.Add("image_url", tweet.ProfileImageUrl);
                            lt.Add("description", tweet.Description); lt.Add("location", tweet.Location); lt.Add("screenName", tweet.ScreenName);
                            lt.Add("tweetsCount", tweet.StatusesCount.ToString()); lt.Add("followersCount", tweet.FollowersCount.ToString());
                            lt.Add("followingCount", tweet.FriendsCount.ToString());

                            ResultSearch.Add(new KeyValuePair<String, Dictionary<string, string>>(tweet.Id.ToString(), lt));
                        }
                        catch { }
                    }//foreach...
                }//outer try...
                catch
                {
                    MessageBox.Show("Internet Issue... Please try again!!!");
                }//catch...

                int ggg = 0;

            }
            public List<Tuple<string, string, string, string, String, String, String>> TW_TweetSearchByKeyword(string keyword, string count)
            {
                TwitterService twitterService = new TwitterService();
                twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
                string maxid = "1000000000000"; // dummy value
                int tweetcount = 0;

                List<TwitterStatus> resultList;
                
                if (maxid != null)
                {
                    try
                    {
                        Int32 cou = Convert.ToInt32(count);
                         var tweets_search = twitterService.Search(new SearchOptions { Q = keyword, Count = cou });
                        resultList = new List<TwitterStatus>(tweets_search.Statuses);

                        if (resultList.Count > 0)
                            maxid = resultList.Last().IdStr;
                        else
                            return ResultSearchTweet;
                   
                    //if (tweets_search == null) return;

                    foreach (var tweet in tweets_search.Statuses)
                    {
                        try
                        {
                            /**Dictionary<string, string> lt = new Dictionary<string, string>();
                            lt.Add("id", ); lt.Add("name", ); lt.Add("image_url", );
                            lt.Add("location", ); 
                            lt.Add("tweetsCount", ); lt.Add("followersCount", );
                            lt.Add("followingCount", );
                            */
                            ResultSearchTweet.Add(new Tuple<string, string, string, string,string, string, string>(
                                tweet.User.ScreenName, tweet.Text, tweet.CreatedDate.ToLongTimeString()+" "+tweet.CreatedDate.ToLongDateString(), tweet.User.Id.ToString(),
                                tweet.Id.ToString(),tweet.User.Name,tweet.User.ProfileImageUrl));
                            tweetcount++;
                        }
                        catch { }
                    }//foreach...
                    
                    while (maxid != null && tweetcount < Convert.ToInt32(count))
                    {
                        maxid = resultList.Last().IdStr;
                        tweets_search = twitterService.Search(new SearchOptions { Q = keyword, Count = Convert.ToInt32(count), MaxId = Convert.ToInt64(maxid) });
                        resultList = new List<TwitterStatus>(tweets_search.Statuses);
                        foreach (var tweet in tweets_search.Statuses)
                        {
                            try
                            {
                                ResultSearchTweet.Add(new Tuple<string, string, string, string, string, string, string>(
                                tweet.User.ScreenName, tweet.Text, tweet.CreatedDate.ToShortDateString(), tweet.User.Id.ToString(),
                                tweet.Id.ToString(), tweet.User.Name, tweet.User.ProfileImageUrl));
                                tweetcount++;
                            }
                            catch { }
                        }
                    }//while....

                    }
                    catch { }


                }

                return ResultSearchTweet;
            }
            public void TW_UserDataByID(string ID)
            {

                TwitterService twitterService = new TwitterService();
                twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
                var tweets_search = twitterService.GetUserProfileFor(new GetUserProfileForOptions { UserId = Convert.ToInt64(ID) });
                TwitterData = tweets_search;


            }
            public void TW_UserDataByScreenName(string name)
            {

                TwitterService twitterService = new TwitterService();
                twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
                var tweets_search = twitterService.GetUserProfileFor(new GetUserProfileForOptions { ScreenName = name });
                TwitterData = tweets_search;
            }
            public void TW_UserTweetsByID(string ID)
            {

                TwitterService twitterService = new TwitterService();
                twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
                //var tweets_search = twitterService.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions { }); 
                var tweets_search = twitterService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { UserId = Convert.ToInt64(ID) });

                TweetsListUser = tweets_search;


            }
            public void TW_UserFollowersByID(string ID, string ScrName)
            {

                TwitterService twitterService = new TwitterService();
                twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
                var tweets_search = twitterService.ListFollowers(new ListFollowersOptions { UserId = Convert.ToInt64(ID), ScreenName = ScrName });
                UserFollowers = tweets_search;

            }
            public void TW_UserFriendsByID(string ID, string ScrName)
            {

                TwitterService twitterService = new TwitterService();
                twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
                var tweets_search = twitterService.ListFriends(new ListFriendsOptions{ UserId = Convert.ToInt64(ID), ScreenName = ScrName });
                UserFollowers = tweets_search;

            }
            public void TW_UserFollow(string ID, string ScrName)
            {

                TwitterService twitterService = new TwitterService();
                twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
                var tweets_search = twitterService.ListFollowerIdsOf(new ListFollowerIdsOfOptions { UserId = Convert.ToInt64(ID), ScreenName = ScrName });
                ListFollowerIds = tweets_search;

            }
        }
        
        public class TwitterSearch
    {
        public dynamic TwitterData;
        public dynamic TwitterClient;
        public dynamic TweetsListUser;
        public dynamic TweetsFollowers;
        public dynamic UserFollowers;
        public dynamic UserFriends;
        public dynamic ListFollowerIds;
        public dynamic ListMemberships;
        public dynamic ListSubscriptions;
        public dynamic ListFavourites;
        public dynamic ListFollowings;

        public string ScreenName { get; set; }
        public string ID { get; set; }
        public dynamic Type { get; set; }
        public string AccessToken { get; set; }
        public string Link { get; set; }
        public string ImageLink { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
        public String consumerKey { get; set; }
        public String consumerSecret { get; set; }
        public String accessToken { get; set; }
        public String accessTokenSecret { get; set; }
        //public ICollection<KeyValuePair<String, String>> ResultSearch = new Dictionary<String, String>();
        public ICollection<KeyValuePair<String, Dictionary<string, string>>> ResultSearch = new Dictionary<String, Dictionary<string, string>>();
        //public List<Tuple<String, String, String, String>> ResultSearchTweet = new List<Tuple<String, String, String, String>>();
        public ICollection<KeyValuePair<String, Dictionary<string, string>>> ResultSearchTweet = new Dictionary<String, Dictionary<string, string>>();
        //** screen_name , id , display_name **//
        public List<Tuple<String, String, String>> FollowersList = new List<Tuple<String, String, String>>();
        public List<Tuple<String, String, String>> FriendsList = new List<Tuple<String, String, String>>();
        public List<Tuple<String, String>> MembershipList = new List<Tuple<String, String>>();
        public List<Tuple<String, String>> SubscriptionsList = new List<Tuple<String, String>>();
        public List<Tuple<String, String>> FavouritesList = new List<Tuple<String, String>>();

        public TwitterSearch(String consumerKey, String consumerSecret, String accessToken, String accessTokenSecret)  // constructor
        {
            //************* new instantiations ************

            Link = "";
            UpdatedTime = "";
            CreatedTime = "";
            ScreenName = "";
            ID = "";
            ImageLink = "";
            //ResultSearch = new Dictionary<string, string>();

            ResultSearch = new Dictionary<String, Dictionary<string, string>>();
            Type = "";
            this.accessToken = accessToken;
            this.accessTokenSecret = accessTokenSecret;
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;

        }
        public TwitterSearch()  // constructor
        {
            //************* new instantiations ************
            Link = "";
            CreatedTime = "";
            UpdatedTime = "";
            ScreenName = "";
            ID = "";
            ImageLink = "";
            Type = "";


        }
        public void TW_UserSearchByName(string name, string count)
        {
            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            //** 20 items per page
            int page_no = Convert.ToInt32(count) / 20 + 1;
          

            for (int c = 0; c < page_no; c++)
            {
                var tweets_search = twitterService.SearchForUser(new SearchForUserOptions { Q = name.ToString(), Count = Convert.ToInt32(count), Page = c });

                foreach (var tweet in tweets_search)
                {
                    try
                    {
                        Dictionary<string, string> lt = new Dictionary<string, string>();
                        lt.Add("id", tweet.Id.ToString()); lt.Add("name", tweet.Name); lt.Add("image_url", tweet.ProfileImageUrl);
                        lt.Add("description", tweet.Description); lt.Add("location", tweet.Location); lt.Add("screenName", tweet.ScreenName);
                        lt.Add("tweetsCount", tweet.StatusesCount.ToString()); lt.Add("followersCount", tweet.FollowersCount.ToString());
                        lt.Add("followingCount", tweet.FriendsCount.ToString());

                        //ResultSearch.Add(new KeyValuePair<String, String>(tweet.Id.ToString(), tweet.ScreenName));
                        ResultSearch.Add(new KeyValuePair<String, Dictionary<string, string>>(tweet.Id.ToString(), lt));
                    }
                    catch { }
                }
            }
           

        }
        public void TW_UserSearchByNamePaging(string name, int NoofResultsPerPage , int PageNo)
        {
            //NOTE : max results range is 1-20
            //if you need to fetch five pages using nexxt button just increment 'PageNo' in parameter

            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            //** max 20 items per page
                int page_no = PageNo;
                var tweets_search = twitterService.SearchForUser(new SearchForUserOptions { Q = name.ToString(), Count = NoofResultsPerPage, Page = PageNo });

                try
                {
                    foreach (var tweet in tweets_search)
                    {
                        try
                        {
                            Dictionary<string, string> lt = new Dictionary<string, string>();
                            lt.Add("id", tweet.Id.ToString()); lt.Add("name", tweet.Name); lt.Add("image_url", tweet.ProfileImageUrl);
                            lt.Add("description", tweet.Description); lt.Add("location", tweet.Location); lt.Add("screenName", tweet.ScreenName);
                            lt.Add("tweetsCount", tweet.StatusesCount.ToString()); lt.Add("followersCount", tweet.FollowersCount.ToString());
                            lt.Add("followingCount", tweet.FriendsCount.ToString());
                            lt.Add("Url", tweet.Url);

                            //ResultSearch.Add(new KeyValuePair<String, String>(tweet.Id.ToString(), tweet.ScreenName));
                            ResultSearch.Add(new KeyValuePair<String, Dictionary<string, string>>(tweet.Id.ToString(), lt));
                        }
                        catch { }
                    }
                }
                catch { }
        }
        public void/**Dictionary<String, Dictionary<string, string>>*/ TW_TweetSearchByKeyword(string keyword, string count)
        {
            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            string maxid = "1000000000000"; // dummy value
            int tweetcount = 0;
            
            List<TwitterStatus> resultList=null;
            if (maxid != null)
            {
                var tweets_search = twitterService.Search(new SearchOptions { Q = keyword, Count = Convert.ToInt32(count) , Lang = "en"});

                try
                {
                    resultList = new List<TwitterStatus>(tweets_search.Statuses);

                    //     if (resultList.Count <= 0)
                    //       return

                    if (resultList != null && resultList.Count > 0)
                        maxid = resultList.Last().IdStr;

                    foreach (var tweet in tweets_search.Statuses)
                    {
                        try
                        {
                            String mediaPhoto = "";

                            if (tweet.Entities.Media != null && tweet.Entities.Media.Count > 0)
                            {
                                mediaPhoto = tweet.Entities.Media[0].MediaUrl;
                            }//if (tweet.Entities.Media != null && tweet.Entities.Media.Count > 0)...

                            Dictionary<string, string> lt = new Dictionary<string, string>();
                            lt.Add("screenName", tweet.User.ScreenName);lt.Add("tweetId", tweet.Id.ToString()); 
                            lt.Add("userName", tweet.User.Name);lt.Add("description", tweet.Text); 
                            lt.Add("dateTime", tweet.CreatedDate.ToLongTimeString() + " " + tweet.CreatedDate.ToLongDateString());lt.Add("userprofileImgUrl", tweet.User.ProfileImageUrl);
                            lt.Add("photo", mediaPhoto); lt.Add("tweetUserId", tweet.User.Id.ToString());

                            //ResultSearch.Add(new KeyValuePair<String, String>(tweet.Id.ToString(), tweet.ScreenName));
                            ResultSearch.Add(new KeyValuePair<String, Dictionary<string, string>>(tweet.Id.ToString(), lt));

                            //tweet.Entities.Media[0].MediaUrl
                            ResultSearchTweet.Add(new KeyValuePair<String, Dictionary<string, string>>(tweet.Id.ToString(), lt));
                              
                                /**
                                new Tuple<string, string, string, string, string, string, string>(
                                    tweet.User.ScreenName, tweet.Text, tweet.CreatedDate.ToLongTimeString() + " " + tweet.CreatedDate.ToLongDateString(), tweet.User.Id.ToString(),
                                    tweet.Id.ToString(), tweet.User.Name, tweet.User.ProfileImageUrl));
                                */

                            tweetcount++;
                        }
                        catch { }
                    }//foreach...

                }
                catch { }

                if (resultList != null && resultList.Count > 0)
                while (maxid != null && tweetcount < Convert.ToInt32(count))
                {
                    maxid = resultList.Last().IdStr;
                    tweets_search = twitterService.Search(new SearchOptions { Q = keyword, Count = Convert.ToInt32(count), MaxId = Convert.ToInt64(maxid) });
                    
                    if(tweets_search!=null&&tweets_search.Statuses!=null)
                    {
                    resultList = new List<TwitterStatus>(tweets_search.Statuses);

                    if (resultList != null)
                    {
                        foreach (var tweet in tweets_search.Statuses)
                        {
                            try
                            {
                                String mediaPhoto = "";

                                if (tweet.Entities.Media != null && tweet.Entities.Media.Count > 0)
                                {
                                    mediaPhoto = tweet.Entities.Media[0].MediaUrl;
                                }//if (tweet.Entities.Media != null && tweet.Entities.Media.Count > 0)...

                                Dictionary<string, string> lt = new Dictionary<string, string>();
                                lt.Add("screenName", tweet.User.ScreenName); lt.Add("tweetId", tweet.Id.ToString());
                                lt.Add("userName", tweet.User.Name); lt.Add("description", tweet.Text);
                                lt.Add("dateTime", tweet.CreatedDate.ToLongTimeString() + " " + tweet.CreatedDate.ToLongDateString()); lt.Add("userprofileImgUrl", tweet.User.ProfileImageUrl);
                                lt.Add("photo", mediaPhoto); lt.Add("tweetUserId", tweet.User.Id.ToString());

                                //ResultSearch.Add(new KeyValuePair<String, String>(tweet.Id.ToString(), tweet.ScreenName));
                                ResultSearch.Add(new KeyValuePair<String, Dictionary<string, string>>(tweet.Id.ToString(), lt));

                                //tweet.Entities.Media[0].MediaUrl
                                ResultSearchTweet.Add(new KeyValuePair<String, Dictionary<string, string>>(tweet.Id.ToString(), lt));


                                /*ResultSearchTweet.Add(new Tuple<string, string, string, string, string, string, string>(
                                    tweet.User.ScreenName, tweet.Text, tweet.CreatedDate.ToLongTimeString() + " " + tweet.CreatedDate.ToLongDateString(), tweet.User.Id.ToString(),
                                    tweet.Id.ToString(), tweet.User.Name, tweet.User.ProfileImageUrl));*/
                                tweetcount++;
                            }
                            catch { }
                        }//foreach...

                    }//if(resultList!=null)
                }//if(tweets_search!=null&&tweets_search.Statuses!=null)...


                }//while...

            }

            //return ResultSearchTweet;
        }
        public void/*Dictionary<String, Dictionary<string, string>>*/ /**List<Tuple<string, string, string, string,string,string,string>>*/ TW_TweetSearchByKeywordPaging(string keyword, int ItemsPerPage, int NumberOfPages, long MaxID)
        {
            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            long? maxid = null; // dummy value
            int count = 0;
            int iterate = NumberOfPages;
            List<TwitterStatus> resultList = null; ;
            dynamic tweets_search = null;

            //if (MaxID.Equals(null) || !MaxID.Equals(-1))
            //{
            //    tweets_search = twitterService.Search(new SearchOptions { Q = keyword, Count = ItemsPerPage });
            //    resultList = new List<TwitterStatus>(tweets_search.Statuses);
            //    maxid = resultList.Last().Id;
            //    foreach (var tweet in tweets_search.Statuses)
            //    {
            //        try
            //        {
            //            ResultSearchTweet.Add(new Tuple<string, string, string, string, string, string, string>(
            //                       tweet.User.ScreenName, tweet.Text, tweet.CreatedDate.ToLongTimeString() + " " + tweet.CreatedDate.ToLongDateString(), tweet.User.Id.ToString(),
            //                       tweet.Id.ToString(), tweet.User.Name, tweet.User.ProfileImageUrl));
            //        }
            //        catch { }
            //    }
            //    count++;
            //}  

             if(MaxID != null)
                {
                    //maxid = resultList.Last().Id;
                    tweets_search = twitterService.Search(new SearchOptions { Q = keyword, Count = ItemsPerPage, MaxId = MaxID });
                    resultList = new List<TwitterStatus>(tweets_search.Statuses);
                    foreach (var tweet in tweets_search.Statuses)
                    {
                        try
                        {
                            String mediaPhoto = "";

                            if (tweet.Entities.Media != null && tweet.Entities.Media.Count > 0)
                            {
                                mediaPhoto = tweet.Entities.Media[0].MediaUrl;
                            }//if (tweet.Entities.Media != null && tweet.Entities.Media.Count > 0)...

                            Dictionary<string, string> lt = new Dictionary<string, string>();
                            lt.Add("screenName", tweet.User.ScreenName); lt.Add("tweetId", tweet.Id.ToString());
                            lt.Add("userName", tweet.User.Name); lt.Add("description", tweet.Text);
                            lt.Add("dateTime", tweet.CreatedDate.ToLongTimeString() + " " + tweet.CreatedDate.ToLongDateString()); lt.Add("userprofileImgUrl", tweet.User.ProfileImageUrl);
                            lt.Add("photo", mediaPhoto); lt.Add("tweetUserId", tweet.User.Id.ToString());

                            //ResultSearch.Add(new KeyValuePair<String, String>(tweet.Id.ToString(), tweet.ScreenName));
                            ResultSearch.Add(new KeyValuePair<String, Dictionary<string, string>>(tweet.Id.ToString(), lt));

                            //tweet.Entities.Media[0].MediaUrl
                            ResultSearchTweet.Add(new KeyValuePair<String, Dictionary<string, string>>(tweet.Id.ToString(), lt));
                            
                        }
                        catch { }
                    }
                count++;
                }

                //**ResultSearchTweet.Add(new Tuple<string,string,string,string,string,string,string>(maxid.ToString(),"","","","","",""));

            //return ResultSearchTweet;
        }   
        public void TW_UserDataByID(string ID)
        {

            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            var tweets_search = twitterService.GetUserProfileFor(new GetUserProfileForOptions { UserId = Convert.ToInt64(ID) });
            TwitterData = tweets_search;
            

        }
        public void TW_UserDataByScreenName(string name)
        {

            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            var tweets_search = twitterService.GetUserProfileFor(new GetUserProfileForOptions { ScreenName = name });
            TwitterData = tweets_search;
        }
        public void TW_UserTweetsByID(string ID )
        {

            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);            
            var tweets_search = twitterService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { UserId = Convert.ToInt64(ID) });
            TweetsListUser = tweets_search;


        }
        public void TW_UserTweetsByIDPaging(string ID , int NumberofTweetsPerPage, int MaxId)
        {
            // NumberofTweetsPerPage Range --> 1 - 20
            // MaxID or null means execute with MaxID Parameter
            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            dynamic tweets_search;
            if(MaxId != -1 || MaxId != null) 
            tweets_search = twitterService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { UserId = Convert.ToInt64(ID) , Count = NumberofTweetsPerPage , MaxId = MaxId });
            else
                tweets_search = twitterService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { UserId = Convert.ToInt64(ID), Count = NumberofTweetsPerPage });
            
            TweetsListUser = tweets_search;


        }
       
        public void TW_UserFollowersByID(string ID, string ScrName)
        {

            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            var tweets_search = twitterService.ListFollowers(new ListFollowersOptions { UserId = Convert.ToInt64(ID), ScreenName = ScrName});
            UserFollowers = tweets_search;

        }
        public void TW_UserFollowingsByID(string ID, string ScrName)
        {

         //   TwitterService twitterService = new TwitterService();
           // twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
           // var tweets_search = twitterService. { UserId = Convert.ToInt64(ID) });
           // ListFollowings = tweets_search;

        }
        public void TW_UserFriendsByID(string ID, string ScrName)
        {

            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            var tweets_search = twitterService.ListFriends(new ListFriendsOptions { UserId = Convert.ToInt64(ID), ScreenName = ScrName });
            UserFriends = tweets_search;

        }
        public void TW_UserFollow(string ID, string ScrName)
        {

            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            var tweets_search = twitterService.ListFollowerIdsOf(new ListFollowerIdsOfOptions { UserId = Convert.ToInt64(ID), ScreenName = ScrName });
            ListFollowerIds = tweets_search;

        }
        public void TW_UserMembershipsById(string ID, string ScrName)
        {

            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            //var tweets_search = twitterService.ListFollowerIdsOf(new ListFollowerIdsOfOptions { UserId = Convert.ToInt64(ID), ScreenName = ScrName });
            var tweets_search = twitterService.ListListMembershipsFor(new ListListMembershipsForOptions { UserId = Convert.ToInt64(ID), ScreenName = ScrName });
            ListMemberships = tweets_search;

        }
        public void TW_UserSubscribersById(string ID, string ScrName)
        {

            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            //var tweets_search = twitterService.ListFollowerIdsOf(new ListFollowerIdsOfOptions { UserId = Convert.ToInt64(ID), ScreenName = ScrName });
            var tweets_search = twitterService.ListSubscriptions(new ListSubscriptionsOptions { UserId = Convert.ToInt64(ID), ScreenName = ScrName });
            ListSubscriptions = tweets_search;

        }
        public void TW_UserFavouritesById(string ID, string ScrName)
        {

            TwitterService twitterService = new TwitterService();
            twitterService.AuthenticateWith(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            var tweets_search = twitterService.ListFavoriteTweets(new ListFavoriteTweetsOptions { UserId = Convert.ToInt64(ID), ScreenName = ScrName, Count = 200 });
            ListFavourites = tweets_search;
          
        }
    }//class...

        public class FacebookSearch
         {
             public dynamic FacebookData;
             public dynamic FacebookClient;
             public string Name { get; set; }
             public long ID { get; set; }
             public dynamic Type { get; set; }
             public string AccessToken { get; set; }
             public string Link { get; set; }
             public string ImageLink { get; set; }
             public string CreatedTime { get; set; }
             public string UpdatedTime { get; set; }
             public List<Tuple<string, string>> PageLikes = new List<Tuple<string, string>>();
             public List<Tuple<string, string>> Feed = new List<Tuple<string, string>>();
             public List<Tuple<string, string, string, string>> Feed_List = new List<Tuple<string, string, string, string>>();
             public List<Tuple<string, string>> MoviesLikes = new List<Tuple<string, string>>();
             public List<Tuple<string, string>> GroupLikes = new List<Tuple<string, string>>();
             public List<Tuple<string, string>> BookLikes = new List<Tuple<string, string>>();
             public List<Tuple<string, string>> PostLikes = new List<Tuple<string, string>>();
             //public List<Tuple<String, String>> ResultSearch = new List<Tuple<string, string>>();
             //public List<Dictionary<string, string>> ResultSearch = new List<Dictionary<string, string>>();
             public ICollection<KeyValuePair<String, String>> ResultSearch = new Dictionary<String, String>();

             public FacebookSearch(String AccessToken)  // constructor
             {
                 //************* new instantiations ************
                 Link = "";
                 UpdatedTime = "";
                 CreatedTime = "";
                 Name = "";
                 ID = 0;
                 ImageLink = "";
                 Feed_List = new List<Tuple<string, string, string, string>>();
                 PageLikes = new List<Tuple<string, string>>();
                 Feed = new List<Tuple<string, string>>();
                 MoviesLikes = new List<Tuple<string, string>>();
                 GroupLikes = new List<Tuple<string, string>>();
                 BookLikes = new List<Tuple<string, string>>();
                 PostLikes = new List<Tuple<string, string>>();
                 //ResultSearch = new List<Tuple<string, string>>();
                 //ResultSearch = new List<Dictionary<string, string>>();
                 ResultSearch = new Dictionary<string, string>();
                 Type = "";
                 FacebookClient = new FacebookClient(AccessToken);
             }
             public FacebookSearch()  // constructor
             {
                 //************* new instantiations ************
                 Link = "";
                 CreatedTime = "";
                 UpdatedTime = "";
                 Name = "";
                 ID = 0;
                 ImageLink = "";
                 PageLikes = null;
                 Feed = null;
                 MoviesLikes = null;
                 GroupLikes = null;
                 BookLikes = null;
                 PostLikes = null;
                 Type = "";
                 //FacebookClient = new FacebookClient(AccessToken);

             }
             public void Query(string name, string type, string place, string limit, string offset)
             {
                 this.Type = type;
                 //this.FacebookData = this.FacebookClient.Get("/search?q=" + name + "&type="+type);
                 if (type == "user")
                 {
                     try
                     {
                         this.FacebookData = FacebookClient.Get("/search?q=" + name + " in " + place + "&type=" + type + "&limit=" + limit + "&offset=" + offset);
                     }
                     catch (Exception e)
                     {
                         MessageBox.Show(e.Message);
                         return;
                     }
                 }

                 if (type == "page" || type == "group")
                 {
                     try
                     {
                         this.FacebookData = FacebookClient.Get("/search?q=" + name + "&type=" + type + "&limit=" + limit + "&offset=" + offset);
                     }
                     catch (Exception e)
                     {
                         MessageBox.Show(e.Message);
                         return;
                     }
                 }

                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;

                 foreach (dynamic L1 in (JsonArray)this.FacebookData["data"])
                 {
                     try
                     {

                         ResultSearch.Add(new KeyValuePair<String, String>((string)(((JsonObject)L1)["id"]), (string)(((JsonObject)L1)["name"])));
                         //ResultSearch.Add(new Tuple<string, string>(((string)(((JsonObject)L1)["id"])), ((string)(((JsonObject)L1)["name"]))));

                     }
                     catch { }
                 }
             }
             public void GetUserDataByID(string id, string limit, string offset)
             {
                 //******************** my id *****************
                 this.FacebookData = FacebookClient.Get("/" + id + "/feed?fields=id,from,picture,type,full_picture,likes.summary(true),comments.summary(true),link,shares,updated_time,created_time,message,message_tags,properties,story,description&show_expired=true&include_hidden=true&limit=" + limit + "&offset=" + offset);
                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;
             }
             public void GetUserGeneralDataByID(string id, string limit, string offset)
             {
                 //******************** my id *****************
                 this.FacebookData = FacebookClient.Get("/" + id + "/feed?limit=" + limit + "&offset=" + offset);
                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;

             }
             public void GetPageLikedPages(string id, string limit, string offset)
             {
                 //************* Getting Likes *************
                 this.FacebookData = FacebookClient.Get("/" + id + "/likes?limit=" + limit + "&offset=" + offset);
                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;

                 //foreach (dynamic L1 in (JsonArray)this.FacebookData["data"])
                 //{
                 //    this.PageLikes.Add(new Tuple<string, string>(((string)(((JsonObject)L1)["name"])), ((string)(((JsonObject)L1)["id"]))));
                 //}

             }
             public void GetPageComments(string id, string limit, string offset)
             {
                 //************* Getting Likes *************
                 this.FacebookData = FacebookClient.Get("/" + id + "/comments?limit=" + limit + "&offset=" + offset);
                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;

                 //foreach (dynamic L1 in (JsonArray)this.FacebookData["data"])
                 //{
                 //    this.PageLikes.Add(new Tuple<string, string>(((string)(((JsonObject)L1)["name"])), ((string)(((JsonObject)L1)["id"]))));
                 //}

             }
             public void GetPagePosts(string id, string limit, string offset)
             {
                 //************* Getting page post *************
                 Feed.Clear();
                 Feed_List.Clear();
                 this.FacebookData = FacebookClient.Get("/" + id + "/posts?limit=" + limit + "&offset=" + offset);
                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;


                 foreach (dynamic L1 in (JsonArray)this.FacebookData["data"])
                 {
                     try
                     {
                         this.Feed.Add(new Tuple<string, string>(((string)(((JsonObject)L1)["object_id"])), ((string)(((JsonObject)L1)["message"]))));
                         this.Feed_List.Add(new Tuple<string, string, string, string>(((string)(((JsonObject)L1)["object_id"])), ((string)(((JsonObject)L1)["message"])), ((string)(((JsonObject)L1)["created_time"])), ((string)(((JsonObject)L1)["updated_time"]))));
                     }
                     catch { }
                 }


             }
             public void GetPageDataByID(string id, string limit, string offset)
             {
                 //******************** my id *****************
                 this.FacebookData = FacebookClient.Get("/" + id + "/feed?fields=id,type,from,picture,full_picture,likes.summary(true),comments.summary(true),link,shares,updated_time,created_time,message,message_tags,properties,story,description&show_expired=true&include_hidden=true&limit=" + limit + "&offset=" + offset);
                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;
             }
             public void GetPageLikersByID(string id, string limit, string offset)
             {
                 //******************** my id *****************
                 this.FacebookData = FacebookClient.Get("/" + id + "/likes?limit=" + limit + "&offset=" + offset);
                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;
             }
             public void GetPageGeneralData(string id)
             {
                 //******************** my id *****************
                 this.FacebookData = FacebookClient.Get("/" + id);
                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;
                 //this.Link = ((string)((this.FacebookData["link"])));
             }
             public void GetPostDataByID(string id, string limit, string offset)
             {
                 this.PostLikes.Clear();
                 //************* Getting Likes *************
                 this.FacebookData = FacebookClient.Get("/" + id + "/likes?limit=" + limit + " & offset = " + offset);
                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;

                 foreach (dynamic L1 in (JsonArray)this.FacebookData["data"])
                 {
                     this.PostLikes.Add(new Tuple<string, string>(((string)(((JsonObject)L1)["name"])), ((string)(((JsonObject)L1)["id"]))));
                 }//foreach...

                 //************* Getting General Data about post *************
                 this.FacebookData = FacebookClient.Get("/" + id);
                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;

                 try
                 {
                     this.Name = ((string)((this.FacebookData["name"])));
                 }
                 catch { }
                 this.Link = ((string)((this.FacebookData["link"])));
                 this.ImageLink = ((string)((this.FacebookData["source"])));
                 this.CreatedTime = ((string)((this.FacebookData["created_time"])));



             }
             public void GetPictureByID(string id)
             {

                 this.FacebookData = FacebookClient.Get("/" + id + "/picture?width=500&height=500&redirect=false");
                 this.FacebookData = (IDictionary<string, object>)this.FacebookData;
                 this.ImageLink = ((string)(((JsonObject)this.FacebookData["data"])["url"]));

             }
         }//end of class...
        #endregion
/*
    public void acv()
    {
        String searchTBox = searchTextBoxText;

        if (searchTBox.Trim().Length > 0)
        {
            ts.TW_UserSearchByName(searchTBox, "50");
            foreach (KeyValuePair<string, Dictionary<string, string>> item in ts.ResultSearch)
            {
                Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                string name = "", profile_image_url = "";
                bool tem = cc.TryGetValue("name", out name);
                bool tem1 = cc.TryGetValue("image_url", out profile_image_url);

                Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Add(new twitterUserListItems()
                {
                    GridViewColumnName_ImageSource = profile_image_url,
                    GridViewColumnName_LabelContent = name
                });

                //TW_lbSearchResult.Items.Add(item.Value.ToString());
            }//foreach...
        }//searchTBox.Text.Trim().Length > 0)...
        else
        {
            MessageBox.Show("Please enter keyword to search...");
        }//else...

    }*/
        private void searchIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
                Image sTB = sender as Image;
                DockPanel the_parent = (((sTB.Parent as Grid).Parent as StackPanel).Parent as Border).Parent as DockPanel;

                TextBox searchTBox = the_parent.FindName("searchTextBox") as TextBox;
                ListView fbUserList = the_parent.FindName("fbUserList") as ListView;
                ListView twitterUserList = the_parent.FindName("twitterUserList") as ListView;
                Countries[tabDynamic.SelectedIndex].mySearch = searchTBox.Text;
                Countries[tabDynamic.SelectedIndex].lastSearch = searchTBox.Text;
            
                
            /*
                tabDynamic.DataContext = Countries;
                tabDynamic.SelectedIndex = 0;
            */
                if (searchTBox.Text.Trim().Length > 0)
                {
                    searchTextBoxText = searchTBox.Text.Trim();

                    Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].fbUserListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Clear();
                    Countries[tabDynamic.SelectedIndex].googleUserListCollections.Clear();

                    Countries[tabDynamic.SelectedIndex].fbGroupLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].fbPageLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].fbUserLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].twitterTweetLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].twitterUserLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].googleUserLoadingImageVisbility = true;
                    Countries[tabDynamic.SelectedIndex].googleActivitiesLoadingImageVisbility = true;

                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForGoogleActivitiesSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForGoogleUserSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForFbUserSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForFbPageSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForFbGroupSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForTwitterTweetSearchVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForTwitterUserSearchVisibility = false;

                    Countries[tabDynamic.SelectedIndex].Header = searchTextBoxText;
                    Countries[tabDynamic.SelectedIndex].lastSearch = searchTextBoxText;

                    searchInParallelThread = new Thread(searchInParallel);

                    tabIndexiiii = tabDynamic.SelectedIndex;

                    searchInParallelThread.Start();

                    /*ts.TW_UserSearchByName(searchTBox.Text, "4");

                    foreach (KeyValuePair<string, Dictionary<string, string>> item in ts.ResultSearch)
                    {
                        Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                        string name = "", profile_image_url = "";
                        bool tem = cc.TryGetValue("name", out name);
                        bool tem1 = cc.TryGetValue("image_url", out profile_image_url);

                        Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Add(new twitterUserListItems()
                        {
                            GridViewColumnName_ImageSource = profile_image_url,
                            GridViewColumnName_LabelContent = name
                        });

                        //TW_lbSearchResult.Items.Add(item.Value.ToString());
                    }//foreach...

                    List<Tuple<String, String, String, String>> temp = new List<Tuple<string, string, string, string>>();
                    temp = ts.TW_TweetSearchByKeyword(searchTBox.Text, "4");
                    Tuple<String, String, String, String> aaaa = temp[0];

                    foreach (Tuple<String, String, String, String> item in temp) 
                    {
                        Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Add(new twitterTweetListItems()
                        {
                            userName = item.Item1,
                            desc = item.Item2,
                            dateTime=item.Item3,
                            userId=item.Item4
                        });

                    }//foreach...


                    //*** using query function of custom class****/
                    //fs.Query(searchTBox.Text, "user","", "4");

                    // foreach((KeyValuePair<string, string>)listProperties.SelectedItem in fs.ResultSearch)
                    //{}

                    //*** Adding search result into some LIST listBoxSearch ****/
                    /**foreach (KeyValuePair<string, string> item in fs.ResultSearch)
                    {
                        //lbSearchResult.Items.Add(item.Value.ToString());
                        Countries[tabDynamic.SelectedIndex].fbUserListCollections.Add(new fbUserListItems()
                        {
                            GridViewColumnName_ID = item.Key,
                            GridViewColumnName_LabelContent = item.Value,
                            GridViewColumnName_ImageSource = @"https://graph.facebook.com/"+item.Key+@"/picture?type=large"
                        });

                    }//foreach...
                    */
                    //*** using query function of custom class****/
                    //fs1.Query(searchTBox.Text, "group", "", "4");
                    
                    //*** Adding search result into some LIST listBoxSearch ****/
                    /**foreach (KeyValuePair<string, string> item in fs1.ResultSearch)
                    {
                        //lbSearchResult.Items.Add(item.Value.ToString());
                        Countries[tabDynamic.SelectedIndex].fbPageListCollections.Add(new fbPageListItems()
                        {
                            fbPage_ID = item.Key,
                            fbPage_LabelContent = item.Value,
                            fbPage_ImageSource = @"https://graph.facebook.com/" + item.Key + @"/picture?type=large"
                        });

                    }//foreach...
                    */
                    //*** using query function of custom class****/
                    //fs2.Query(searchTBox.Text, "page", "", "4");
                    
                    //*** Adding search result into some LIST listBoxSearch ****/
                   /* foreach (KeyValuePair<string, string> item in fs2.ResultSearch)
                    {
                        //lbSearchResult.Items.Add(item.Value.ToString());
                        Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Add(new fbGroupListItems()
                        {
                            fbGroup_ID = item.Key,
                            fbGroup_LabelContent = item.Value,
                            fbGroup_ImageSource = @"https://graph.facebook.com/" + item.Key + @"/picture?type=large"
                        });

                    }//foreach...
                    */
                    /**Countries[tabDynamic.SelectedIndex].fbGroupLoadingImageVisbility = false;
                    Countries[tabDynamic.SelectedIndex].fbPageLoadingImageVisbility = false;
                    Countries[tabDynamic.SelectedIndex].fbUserLoadingImageVisbility = false;
                    Countries[tabDynamic.SelectedIndex].twitterTweetLoadingImageVisbility = false;
                    Countries[tabDynamic.SelectedIndex].twitterUserLoadingImageVisbility = false;
                    */
                    //dgvLoadSearchedTweets(temp);
                    //int ggg = 0;
                }//searchTBox.Text.Trim().Length > 0)...
                else
                {
                    MessageBox.Show("Please enter keyword to search...");
                }//else...
            
            
            /**
                if (searchTBox.Text.Trim().Length > 0)
                {
                    searchTextBoxText = searchTBox.Text;
                    Thread th = new Thread(acv);
                    th.Start();
                }//searchTBox.Text.Trim().Length > 0)...
                else
                {
                    MessageBox.Show("Please enter keyword to search...");
                }//else...
                */
        }

        private void TwitterUserProfileTweetList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetCountEllipseBg.Equals("#aaaaaa")) return;

            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetCountEllipseBg = "#aaaaaa";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingCountEllipseBg = "#e1e1e1";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersCountEllipseBg = "#e1e1e1";

            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetListVisbility = true;

            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;

            /**
            searchTextBoxText = Countries[tabDynamic.SelectedIndex].twitterProfileUserId;
            searchTextBoxText1 = Countries[tabDynamic.SelectedIndex].twitterProfileUserAge;

            if (searchTextBoxText1.Contains("@")) searchTextBoxText1 = searchTextBoxText1.Replace("@", "");

            Thread twitterUserTweetsInParallelThread = new Thread(getTwitterUserFollowersInParallel);
            twitterUserTweetsInParallelThread.Start();*/

        }//func...

        private void TwitterUserProfileFollowersList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersCountEllipseBg.Equals("#aaaaaa")) return;

            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetCountEllipseBg = "#e1e1e1";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingCountEllipseBg = "#e1e1e1";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersCountEllipseBg = "#aaaaaa";

            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListVisbility = true;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetListVisbility = false;

            searchTextBoxText=Countries[tabDynamic.SelectedIndex].twitterProfileUserId;
            searchTextBoxText1 = Countries[tabDynamic.SelectedIndex].twitterProfileUserAge;

            if (searchTextBoxText1.Contains("@")) searchTextBoxText1= searchTextBoxText1.Replace("@", "");

            if (Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListCollections.Count > 0) return;
            
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListCollections.Clear();

            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = true;

            Thread twitterUserTweetsInParallelThread = new Thread(getTwitterUserFollowersInParallel);
            twitterUserTweetsInParallelThread.Start();

        }//func...

        private void TwitterUserProfileFollowingList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingCountEllipseBg.Equals("#aaaaaa")) return;

            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetCountEllipseBg = "#e1e1e1";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingCountEllipseBg = "#aaaaaa";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersCountEllipseBg = "#e1e1e1";

            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListVisbility = true;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetListVisbility = false;

            searchTextBoxText = Countries[tabDynamic.SelectedIndex].twitterProfileUserId;
            searchTextBoxText1 = Countries[tabDynamic.SelectedIndex].twitterProfileUserAge;

            if (searchTextBoxText1.Contains("@")) searchTextBoxText1 = searchTextBoxText1.Replace("@", "");

            if (Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListCollections.Count > 0) return;
           
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListCollections.Clear();

            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = true;

            Thread twitterUserTweetsInParallelThread = new Thread(getTwitterUserFollowingInParallel);
            twitterUserTweetsInParallelThread.Start();
        }

        private void fbPageLikersList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /**
            if (Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersCountEllipseBg.Equals("#aaaaaa")) return;

            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetCountEllipseBg = "#e1e1e1";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingCountEllipseBg = "#e1e1e1";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersCountEllipseBg = "#aaaaaa";

            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListVisbility = true;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetListVisbility = false;

            searchTextBoxText = Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID;
            
            if (Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListCollections.Count > 0) return;

            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListCollections.Clear();

            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = true;
            */
            //Thread fbPageLikersInParallelThread = new Thread(getFbPageLikersInParallel);
            //fbPageLikersInParallelThread.Start();

        }//func...

        private void fbPageStatusesList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetCountEllipseBg.Equals("#aaaaaa")) return;

            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetCountEllipseBg = "#aaaaaa";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingCountEllipseBg = "#e1e1e1";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersCountEllipseBg = "#e1e1e1";

            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetListVisbility = true;

            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;

            /**
            searchTextBoxText = Countries[tabDynamic.SelectedIndex].twitterProfileUserId;
            searchTextBoxText1 = Countries[tabDynamic.SelectedIndex].twitterProfileUserAge;

            if (searchTextBoxText1.Contains("@")) searchTextBoxText1 = searchTextBoxText1.Replace("@", "");

            Thread twitterUserTweetsInParallelThread = new Thread(getTwitterUserFollowersInParallel);
            twitterUserTweetsInParallelThread.Start();*/

        }//func...

        public void getTwitterUserTweetsInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                ts = new TwitterSearch(
            "pNpMDTAW5Q0MACi912iQ8BhVy",
            "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
            "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
            "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

                ts.TW_UserTweetsByID(searchTextBoxText);

                if (ts.TweetsListUser != null)
                {
                    int incIndex=0;
                    foreach (dynamic item in ts.TweetsListUser)
                    {

                        App.Current.Dispatcher.Invoke((Action)(() =>
                           {
                               String mediaUrl = "";

                               String text = item.GetType().GetProperty("Text").GetValue(item, null);
                               Object userObj = item.GetType().GetProperty("User").GetValue(item, null);
                               String screenName = userObj.GetType().GetProperty("ScreenName").GetValue(userObj, null).ToString();
                               String name = userObj.GetType().GetProperty("Name").GetValue(userObj, null).ToString();
                               String retweetCount = Convert.ToString(item.GetType().GetProperty("RetweetCount").GetValue(item, null));
                               String favoriteCount = "0";//item.GetType().GetProperty("Text").GetValue(item, null);
                               String tweetId = item.GetType().GetProperty("IdStr").GetValue(item, null);

                               //bool tem1 = item.TryGetValue("createdDate", out dateTime);

                               //tem1 = cc.TryGetValue("dateTime", out dateTime);

                               DateTime ob = item.GetType().GetProperty("CreatedDate").GetValue(item, null);

                               ob = ob.AddHours(5);

                               //DateTime ob1 = ob;
                               
                               String dateTime = "Created DateTime: "+ob.ToShortDateString()+" "+ob.ToShortTimeString();
                               String userDp = userObj.GetType().GetProperty("ProfileImageUrl").GetValue(userObj, null).ToString();
                               bool twitterUserTweetListItems_PhotoVisibility=false;
                               userObj = item.GetType().GetProperty("Entities").GetValue(item, null);
                               userObj = userObj.GetType().GetProperty("Media").GetValue(userObj, null);

                               IList<TweetSharp.TwitterMedia> tMList = userObj as IList<TweetSharp.TwitterMedia>;

                               if (tMList != null && tMList.Count > 0)
                               {
                                   TwitterMedia tM = tMList[0] as TwitterMedia;
                                   mediaUrl = tM.MediaUrl;

                                   twitterUserTweetListItems_PhotoVisibility = true;
                               }//if (tMList != null && tMList.Count > 0)...

                               //String mediaUrl=tM.MediaUrl;
                               
                               //userObj.GetType().GetProperty("MediaUrl").GetValue(userObj, null).ToString();
                               String fdfd = incIndex.ToString();
                               //Do something here...
                               Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections.Add(new twitterUserTweetListItems()
                               {
                                   twitterUserTweetListItems_dateTime = dateTime,
                                   twitterUserTweetListItems_desc = text,
                                   twitterUserTweetListItems_favoriteCount = favoriteCount,
                                   twitterUserTweetListItems_Id = tweetId,
                                   twitterUserTweetListItems_name = name,
                                   twitterUserTweetListItems_retweetCount = retweetCount,
                                   twitterUserTweetListItems_screenName = "( @" + screenName + " )",
                                   twitterUserTweetListItems_userDp = userDp,
                                   twitterUserTweetListItems_PhotoVisibility = twitterUserTweetListItems_PhotoVisibility,
                                   twitterUserTweetListItems_Photo=mediaUrl,
                                   twitterUserTweetListItems_number=incIndex.ToString(),
                                   twitterUserTweetListItems_reportButtonText="Add to Report"
                                   
                                   
                               });

                               Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;
                           }));//dispatcher...
                        
                        incIndex++;

                    }//foreach...
                }//if(ts.TweetsListUser!=null)...
            }//else...

        }//end of func...

        public void getTwitterUserFollowersInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                ts = new TwitterSearch(
            "pNpMDTAW5Q0MACi912iQ8BhVy",
            "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
            "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
            "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

                ts.TW_UserFollowersByID(searchTextBoxText, searchTextBoxText1);

                foreach (dynamic item in ts.UserFollowers)
                {

                    //int i=0;
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        
                        String id = Convert.ToString(item.GetType().GetProperty("Id").GetValue(item, null));
                        //Object userObj = item.GetType().GetProperty("User").GetValue(item, null);
                        String screenName = item.GetType().GetProperty("ScreenName").GetValue(item, null);
                        String name = item.GetType().GetProperty("Name").GetValue(item, null);
                        String location = item.GetType().GetProperty("Location").GetValue(item, null);
                        String tweetCount = Convert.ToString(item.GetType().GetProperty("StatusesCount").GetValue(item, null));
                        String followersCount = Convert.ToString(item.GetType().GetProperty("FollowersCount").GetValue(item, null));
                        String followingCount = Convert.ToString(item.GetType().GetProperty("FriendsCount").GetValue(item, null));
                        String userDp = Convert.ToString(item.GetType().GetProperty("ProfileImageUrl").GetValue(item, null));
                        
                        //Do something here...
                        Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListCollections.Add(new twitterUserListItems()
                        {
                          GridViewColumnLocation=location,
                          GridViewColumnName_ID=id,
                          GridViewColumnName_ImageSource=userDp,
                          GridViewColumnName_LabelContent=name,
                          GridViewColumnName_FollowersCount=followersCount,
                          GridViewColumnName_FollowingCount=followingCount,
                          GridViewColumnName_LabelContentScreenName=screenName,
                          GridViewColumnName_tweetsCount=tweetCount
                        });

                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;
                    }));//dispatcher...

                }//foreach...

            }//else...

        }//end of func...

        public void getFbPageLikersInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
            }//else...
        }//end of func...

        public void getTwitterUserFollowingInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                ts = new TwitterSearch(
            "pNpMDTAW5Q0MACi912iQ8BhVy",
            "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
            "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
            "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

                ts.TW_UserFriendsByID(searchTextBoxText, searchTextBoxText1);

                foreach (dynamic item in ts.UserFriends)
                {

                    //int i=0;
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {

                        String id = Convert.ToString(item.GetType().GetProperty("Id").GetValue(item, null));
                        //Object userObj = item.GetType().GetProperty("User").GetValue(item, null);
                        String screenName = item.GetType().GetProperty("ScreenName").GetValue(item, null);
                        String name = item.GetType().GetProperty("Name").GetValue(item, null);
                        String location = item.GetType().GetProperty("Location").GetValue(item, null);
                        String tweetCount = Convert.ToString(item.GetType().GetProperty("StatusesCount").GetValue(item, null));
                        String followersCount = Convert.ToString(item.GetType().GetProperty("FollowersCount").GetValue(item, null));
                        String followingCount = Convert.ToString(item.GetType().GetProperty("FriendsCount").GetValue(item, null));
                        String userDp = Convert.ToString(item.GetType().GetProperty("ProfileImageUrl").GetValue(item, null));

                        //Do something here...
                        Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListCollections.Add(new twitterUserListItems()
                        {
                            GridViewColumnLocation = location,
                            GridViewColumnName_ID = id,
                            GridViewColumnName_ImageSource = userDp,
                            GridViewColumnName_LabelContent = name,
                            GridViewColumnName_FollowersCount = followersCount,
                            GridViewColumnName_FollowingCount = followingCount,
                            GridViewColumnName_LabelContentScreenName = screenName,
                            GridViewColumnName_tweetsCount = tweetCount
                        });

                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;
                    }));//dispatcher...

                }//foreach...

            }//else...

        }//end of func...

        public void searchInParallel() 
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                ts = new TwitterSearch(
            "pNpMDTAW5Q0MACi912iQ8BhVy",
            "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
            "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
            "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

                fs = new FacebookSearch(fbSessionId);
                fs1 = new FacebookSearch(fbSessionId);
                fs2 = new FacebookSearch(fbSessionId);

                //GS = new GoogleSearch("904980209954-1lcou02m0c5a3auodim9i0ue0dbg2lnt.apps.googleusercontent.com", "_p5GYoAWngP2a4PdfrpgYLqD");

                int tabIndexi = tabIndexiiii;

                ts.TW_UserSearchByNamePaging(searchTextBoxText, 50,1);

                   App.Current.Dispatcher.Invoke((Action)(() =>
                   {
                       foreach (KeyValuePair<string, Dictionary<string, string>> item in ts.ResultSearch)
                       {
                           Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                           string Url="",name = "", id = "", screenName = "", profile_image_url = "", description = "", age = "", location = "", followersCount = "", tweetsCount = "", FollowingCount = "";
                           bool tem = cc.TryGetValue("name", out name);
                           bool tem1 = cc.TryGetValue("image_url", out profile_image_url);
                           bool tem2 = cc.TryGetValue("description", out description);
                           bool tem3 = cc.TryGetValue("screenName", out age);
                           bool tem4 = cc.TryGetValue("location", out location);
                           tem4 = cc.TryGetValue("screenName", out screenName);
                           tem4 = cc.TryGetValue("id", out id);

                           tem1 = cc.TryGetValue("followersCount", out followersCount);
                           tem1 = cc.TryGetValue("tweetsCount", out tweetsCount);
                           tem1 = cc.TryGetValue("followingCount", out FollowingCount);

                           tem1 = cc.TryGetValue("Url", out Url);

                       //Do something here...
                           Countries[tabIndexi].twitterUserListCollections.Add(new twitterUserListItems()
                       {
                           GridViewColumnName_ID=id,
                           GridViewColumnName_ImageSource = profile_image_url,
                           GridViewColumnName_LabelContent = name,
                           GridViewColumnLocation=location,
                           GridViewColumnName_AboutMe=description,
                           GridViewColumnName_Age=screenName,
                           GridViewColumnName_CityCountry=location,
                           GridViewColumnName_LabelContentScreenName="@"+screenName,
                           GridViewColumnName_FollowersCount=followersCount,
                           GridViewColumnName_FollowingCount=FollowingCount,
                           GridViewColumnName_tweetsCount=tweetsCount,
                           twitterUserMoreOptionBehindVisibility=true,
                           twitterUserMoreOptionVisibility=false,
                           GridViewColumn_Url=Url
                       });

                }//foreach...

                       Countries[tabIndexi].twitterUserLoadingImageVisbility = false;

                       if (Countries[tabIndexi].twitterUserListCollections.Count == 0)
                           Countries[tabIndexi].nothingToShowTextForTwitterUserSearchVisibility = true;
                       else
                           Countries[tabIndexi].nothingToShowTextForTwitterUserSearchVisibility = false;

                       Countries[tabIndexi].twitterUserListCollectionsCount = Countries[tabIndexi].twitterUserListCollections.Count.ToString();

                       Countries[tabIndexi].Shapes[16].textIfAny = Countries[tabIndexi].twitterUserListCollections.Count.ToString();

                       if (Countries[tabIndexi].twitterUserListCollections.Count > 0 && ((float)Countries[tabIndexi].twitterUserListCollections.Count) % 50.0f == 0.0f)
                           Countries[tabIndexi].twitterUserListCollections.Add(new twitterUserListItems()
                           {
                               twitterUserMoreOptionVisibility = true,
                               twitterUserMoreOptionBehindVisibility=false,
                               twitterUserMoreOptionText="more",
                               GridViewColumnName_ID = Countries[tabIndexi].twitterUserListCollections[Countries[tabIndexi].twitterUserListCollections.Count - 1].GridViewColumnName_ID
                           });

                   }));//dispatcher...

                   ICollection<KeyValuePair<String, Dictionary<string, string>>> temp = new Dictionary<String, Dictionary<string, string>>();
                /*temp =*/ ts.TW_TweetSearchByKeyword(searchTextBoxText, "50");
                temp=ts.ResultSearchTweet;
                //Tuple<String, String, String, String, String, String, String> aaaa = temp[0];

                 App.Current.Dispatcher.Invoke((Action)(() =>
                   {
                       foreach (KeyValuePair<string, Dictionary<string, string>> item in temp)
                {
                    /*
                     Dictionary<string, string> lt = new Dictionary<string, string>();
                     lt.Add("screenName", tweet.User.ScreenName);lt.Add("tweetId", tweet.Id.ToString()); 
                     lt.Add("userName", tweet.User.Name);lt.Add("description", tweet.Text); 
                     lt.Add("dateTime", tweet.CreatedDate.ToLongTimeString() + " " + tweet.CreatedDate.ToLongDateString());lt.Add("userprofileImgUrl", tweet.User.ProfileImageUrl);
                     lt.Add("photo", mediaPhoto); lt.Add("tweetUserId", tweet.User.Id.ToString());

                    */

                    Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                    DateTime dateTime=new DateTime();
                        String userName = "", tweetId = "", screenName = "", profile_image_url = "", description = "", photo = "", tweetUserId = "";
                    bool tem = cc.TryGetValue("userName", out userName);
                    bool tem1 = cc.TryGetValue("userprofileImgUrl", out profile_image_url);
                    bool tem2 = cc.TryGetValue("description", out description);
                    //bool tem3 = cc.TryGetValue("screenName", out age);
                    bool tem4 = cc.TryGetValue("photo", out photo);
                    tem4 = cc.TryGetValue("screenName", out screenName);
                    tem4 = cc.TryGetValue("tweetId", out tweetId);

                    tem1 = cc.TryGetValue("tweetUserId", out tweetUserId);
                    //tem1 = cc.TryGetValue("tweetsCount", out tweetsCount);
                    //tem1 = cc.TryGetValue("followingCount", out FollowingCount);

                    //item.GetType().GetProperty("dateTime").GetValue(item, null);

                    String actualDateTime;

                    tem1 = cc.TryGetValue("dateTime", out actualDateTime);

                    dateTime = DateTime.Parse(actualDateTime);

                    dateTime = dateTime.AddHours(5);


                           //dateTime=item.GetType().GetProperty("CreatedDate").GetValue(item, null);//cc.TryGetValue("dateTime", out dateTime);

                       Countries[tabIndexi].twitterTweetListCollections.Add(new twitterTweetListItems()
                       {
                           userScreenName = screenName,
                           desc = description,
                           dateTime = dateTime.ToLongTimeString()+" "+dateTime.ToLongDateString()/*dateTime*/,
                           userId = tweetUserId,
                           tweetId=tweetId,
                           userName=userName,
                           userProfileImageUrl=profile_image_url,
                           twitterTweetsMoreOptionBehindVisibility=true,
                           twitterTweetsMoreOptionVisibility=false,
                           twitterTweetsPhotoVisibility=false,
                           twitterTweetsPhoto=photo
                       });

                       Countries[tabIndexi].twitterTweetListCollectionsCount = Countries[tabIndexi].twitterTweetListCollections.Count.ToString();

                       Countries[tabIndexi].Shapes[17].textIfAny = Countries[tabIndexi].twitterTweetListCollections.Count.ToString();

                       if (((float)Countries[tabIndexi].twitterTweetListCollections.Count) % 50.0f == 0.0f)
                           Countries[tabIndexi].twitterTweetListCollections.Add(new twitterTweetListItems()
                           {
                               twitterTweetsMoreOptionVisibility = true,
                               twitterTweetsMoreOptionBehindVisibility = false,
                               twitterTweetsMoreOptionText = "more",
                               tweetId = Countries[tabIndexi].twitterTweetListCollections[Countries[tabIndexi].twitterTweetListCollections.Count - 1].tweetId
                           });

                }//foreach...

                       if (Countries[tabIndexi].twitterTweetListCollections.Count == 0)
                           Countries[tabIndexi].nothingToShowTextForTwitterTweetSearchVisibility = true;
                else
                           Countries[tabIndexi].nothingToShowTextForTwitterTweetSearchVisibility = false;

                       Countries[tabIndexi].twitterTweetLoadingImageVisbility = false;
                   }));

                    fs.Query(searchTextBoxText, "user","", "50","0");

                  App.Current.Dispatcher.Invoke((Action)(() =>
                   {
                    //*** Adding search result into some LIST listBoxSearch ****/
                    foreach (KeyValuePair<string, string> item in fs.ResultSearch)
                    {
                       //lbSearchResult.Items.Add(item.Value.ToString());
                        Countries[tabIndexi].fbUserListCollections.Add(new fbUserListItems()
                       {
                           GridViewColumnName_ID = item.Key,
                           GridViewColumnName_LabelContent = item.Value,
                           GridViewColumnName_ImageSource = @"https://graph.facebook.com/" + item.Key + @"/picture?type=large",
                           fbUserMoreOptionBehindVisibility=true,
                           fbUserMoreOptionVisibility=false
                       });

                        Countries[tabIndexi].fbUserListCollectionsCount = Countries[tabIndexi].fbUserListCollections.Count.ToString();

                        Countries[tabIndexi].Shapes[7].textIfAny = Countries[tabIndexi].fbUserListCollections.Count.ToString();
                   
                    }//foreach...

                    Countries[tabIndexi].fbUserLoadingImageVisbility = false;
                    if (Countries[tabIndexi].fbUserListCollections.Count == 0)
                        Countries[tabIndexi].nothingToShowTextForFbUserSearchVisibility = true;
                    else
                        Countries[tabIndexi].nothingToShowTextForFbUserSearchVisibility = false;


                    if (Countries[tabIndexi].fbUserListCollections.Count > 0 && ((float)Countries[tabIndexi].fbUserListCollections.Count) % 50.0f == 0.0f)
                        Countries[tabIndexi].fbUserListCollections.Add(new fbUserListItems()
                        {
                            fbUserMoreOptionVisibility = true,
                            fbUserMoreOptionBehindVisibility = false,
                            fbUserMoreOptionText = "more",
                            GridViewColumnName_ID = Countries[tabIndexi].fbUserListCollections[Countries[tabIndexi].fbUserListCollections.Count - 1].GridViewColumnName_ID
                        });
                   }));

                fs1.Query(searchTextBoxText, "page", "", "50","0");
                    
                App.Current.Dispatcher.Invoke((Action)(() =>
                   {
                    //*** Adding search result into some LIST listBoxSearch ****/
                    foreach (KeyValuePair<string, string> item in fs1.ResultSearch)
                    {
                       //lbSearchResult.Items.Add(item.Value.ToString());
                        Countries[tabIndexi].fbPageListCollections.Add(new fbPageListItems()
                       {
                           fbPage_ID = item.Key,
                           fbPage_LabelContent = item.Value,
                           fbPage_ImageSource = @"https://graph.facebook.com/" + item.Key + @"/picture?type=large",
                           fbPageMoreBehindOptionVisibility=true,
                           fbPageMoreOptionVisibility=false
                       });

                        Countries[tabIndexi].fbPageListCollectionsCount = Countries[tabIndexi].fbPageListCollections.Count.ToString();

                        Countries[tabIndexi].Shapes[9].textIfAny = Countries[tabIndexi].fbPageListCollections.Count.ToString();
                   
                    }//foreach...

                    Countries[tabIndexi].fbPageLoadingImageVisbility = false;
                    if (Countries[tabIndexi].fbPageListCollections.Count == 0)
                        Countries[tabIndexi].nothingToShowTextForFbPageSearchVisibility = true;
                    else
                        Countries[tabIndexi].nothingToShowTextForFbPageSearchVisibility = false;


                    if (Countries[tabIndexi].fbPageListCollections.Count > 0 && ((float)Countries[tabIndexi].fbPageListCollections.Count) % 50.0f == 0.0f)
                        Countries[tabIndexi].fbPageListCollections.Add(new fbPageListItems()
                        {
                            fbPageMoreOptionVisibility = true,
                            fbPageMoreBehindOptionVisibility = false,
                            fbPageMoreOptionText = "more",
                            fbPage_ID = Countries[tabIndexi].fbPageListCollections[Countries[tabIndexi].fbPageListCollections.Count - 1].fbPage_ID
                        });
                   }));
 
                    //*** using query function of custom class****/
                    fs2.Query(searchTextBoxText, "group", "", "50","0");
                    
                 App.Current.Dispatcher.Invoke((Action)(() =>
                   {
                    //*** Adding search result into some LIST listBoxSearch ****/
                    foreach (KeyValuePair<string, string> item in fs2.ResultSearch)
                    {  
                       //lbSearchResult.Items.Add(item.Value.ToString());
                        Countries[tabIndexi].fbGroupListCollections.Add(new fbGroupListItems()
                       {
                           fbGroup_ID = item.Key,
                           fbGroup_LabelContent = item.Value,
                           fbGroup_ImageSource = @"https://graph.facebook.com/" + item.Key + @"/picture?type=large",
                           fbGroupMoreOptionBehindVisibility=true,
                           fbGroupMoreOptionVisibility=false
                       });

                        Countries[tabIndexi].fbGroupListCollectionsCount = Countries[tabIndexi].fbGroupListCollections.Count.ToString();

                        Countries[tabIndexi].Shapes[8].textIfAny = Countries[tabIndexi].fbGroupListCollections.Count.ToString();

                    }//foreach...

                    Countries[tabIndexi].fbGroupLoadingImageVisbility = false;
                    if (Countries[tabIndexi].fbGroupListCollections.Count == 0)
                        Countries[tabIndexi].nothingToShowTextForFbGroupSearchVisibility = true;
                    else
                        Countries[tabIndexi].nothingToShowTextForFbGroupSearchVisibility = false;

                    if (Countries[tabIndexi].fbGroupListCollections.Count > 0 && ((float)Countries[tabIndexi].fbGroupListCollections.Count) % 50.0f == 0.0f)
                        Countries[tabIndexi].fbGroupListCollections.Add(new fbGroupListItems()
                        {
                            fbGroupMoreOptionVisibility = true,
                            fbGroupMoreOptionBehindVisibility = false,
                            fbGroupMoreOptionText = "more",
                            fbGroup_ID = Countries[tabIndexi].fbGroupListCollections[Countries[tabIndexi].fbGroupListCollections.Count - 1].fbGroup_ID
                        });
                   
                   }));
                /////////////////////////////////////////////////////////////////////////////////////////////////////
                // ** google plus...

                if(GS!=null&&GS.ResultSearch!=null)
                    GS.ResultSearch.Clear();
                else
                    GS = new GoogleSearch("904980209954-1lcou02m0c5a3auodim9i0ue0dbg2lnt.apps.googleusercontent.com", "_p5GYoAWngP2a4PdfrpgYLqD");

                    //Cursor.Current = Cursors.WaitCursor;
                    IList<Person> SP = GS.GP_UserSearchByName(searchTextBoxText,50,"");

                    App.Current.Dispatcher.Invoke((Action)(() =>
                      {
                          if (SP != null && SP.Count > 0)
                          {
                              foreach (Person P in SP)
                              {
                                  if (P.Url == null && SP.Count == 50 + 1)
                                  {
                                      Countries[tabIndexi].googleUserListCollectionsCount = Countries[tabIndexi].googleUserListCollections.Count.ToString();

                                      Countries[tabIndexi].Shapes[23].textIfAny = Countries[tabIndexi].googleUserListCollections.Count.ToString();

                                      if (Countries[tabIndexi].googleUserListCollections.Count > 0 && ((float)Countries[tabIndexi].googleUserListCollections.Count) % 50.0f == 0.0f)
                                          Countries[tabIndexi].googleUserListCollections.Add(new googleUserListItems()
                                          {
                                              googleUserMoreOptionVisibility = true,
                                              googleUserMoreOptionBehindVisibility = false,
                                              googleUserMoreOptionText = "more",
                                              Id = SP[SP.Count - 1].DisplayName
                                          });

                                      continue;
                                  }//end of if...
                                  else
                                      Countries[tabIndexi].googleUserListCollectionsCount = Countries[tabIndexi].googleUserListCollections.Count.ToString();

                                  try
                                  {
                                      int a = 0, b = 0;
                                      bool c = false, d = false;
                                      if (P.CircledByCount != null) { a = Convert.ToInt32(P.CircledByCount); }
                                      if (P.PlusOneCount != null) { b = Convert.ToInt32(P.PlusOneCount); }
                                      if (P.IsPlusUser != null) { c = Convert.ToBoolean(P.IsPlusUser); }
                                      if (P.Verified != null) { d = Convert.ToBoolean(P.Verified); }

                                      Countries[tabIndexi].googleUserListCollections.Add(new googleUserListItems()
                                      {
                                          Id = P.Id,
                                          DisplayName = P.DisplayName,
                                          ImageUrl = P.Image.Url,
                                          aboutMe = P.AboutMe,
                                          Birthday = P.Birthday,
                                          BraggingRights = P.BraggingRights,
                                          CircledByCount = a,
                                          CurrentLocation = P.CurrentLocation,
                                          Domain = P.Domain,
                                          ETag = P.ETag,
                                          Gender = P.Gender,
                                          IsPlusUser = c,
                                          Kind = P.Kind,
                                          Language = P.Language,
                                          NickName = P.Nickname,
                                          ObjectType = P.ObjectType,
                                          Occupation = P.Occupation,
                                          PlusOneCount = b,
                                          RelationshipStatus = P.RelationshipStatus,
                                          Skills = P.Skills,
                                          Tagline = P.Tagline,
                                          Url = P.Url,
                                          Verified = d,
                                          googleUserMoreOptionBehindVisibility = true,
                                          googleUserMoreOptionVisibility = false
                                      });

                                      //GS.ResultSearch.Add(new KeyValuePair<String, String>(P.Id.ToString(), P.DisplayName.ToString()));
                                      //GP_lbSearchResults.Items.Add(P.DisplayName);
                                  }
                                  catch { }
                              }//foreach...
                          }//if sp!=null && sp.count>0...


                          Countries[tabIndexi].googleUserLoadingImageVisbility = false;

                          if (Countries[tabIndexi].googleUserListCollections.Count == 0)
                              Countries[tabIndexi].nothingToShowTextForGoogleUserSearchVisibility = true;
                          else
                              Countries[tabIndexi].nothingToShowTextForGoogleUserSearchVisibility = false;
                         
                      }));

                    IList<Activity> ActList = GS.GP_SearchActivities(GS.Service, searchTextBoxText,20,"");

                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        if (ActList != null && ActList.Count>0)
                        {
                            foreach (Activity P in ActList)
                            {
                                try
                                {
                                    if (P.Url == null && ActList.Count == 20 + 1)
                                    {
                                        Countries[tabIndexi].googleActivitiesListCollectionsCount = Countries[tabIndexi].googleActivitiesListCollections.Count.ToString();

                                        if (Countries[tabIndexi].googleActivitiesListCollections.Count > 0 && ((float)Countries[tabIndexi].googleActivitiesListCollections.Count) % 20.0f == 0.0f)
                                            Countries[tabIndexi].googleActivitiesListCollections.Add(new twitterTweetListItems()
                                            {
                                                twitterTweetsMoreOptionVisibility = true,
                                                twitterTweetsMoreOptionBehindVisibility = false,
                                                twitterTweetsMoreOptionText = "more",
                                                tweetId = ActList[ActList.Count - 1].Title
                                            });

                                        continue;
                                    }//end of if...
                                    else
                                        Countries[tabIndexi].googleActivitiesListCollectionsCount = Countries[tabIndexi].googleActivitiesListCollections.Count.ToString();

                                    Countries[tabIndexi].Shapes[24].textIfAny = Countries[tabIndexi].googleActivitiesListCollections.Count.ToString();

                                    Countries[tabIndexi].googleActivitiesListCollections.Add(new twitterTweetListItems()
                                    {
                                        tweetId = P.Id,
                                        desc = P.Title,
                                        dateTime = P.Published.ToString(),
                                        userId = P.Actor.Id,
                                        twitterTweetsMoreOptionVisibility = false,
                                        twitterTweetsMoreOptionBehindVisibility = true
                                    });
                                }
                                catch { }
                            }//foreach...
                        }//if(ActList!=null)...

                        Countries[tabIndexi].googleActivitiesLoadingImageVisbility = false;

                        if (Countries[tabIndexi].googleActivitiesListCollections.Count == 0)
                            Countries[tabIndexi].nothingToShowTextForGoogleActivitiesSearchVisibility = true;
                        else
                            Countries[tabIndexi].nothingToShowTextForGoogleActivitiesSearchVisibility = false;

                        //Countries[tabDynamic.SelectedIndex].googleActivitiesListCollectionsCount = Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Count.ToString();
                    }));

                /////////////////////////////////////////////////////////////////////////////////////////////////////

            }//else...

        }//end of func... 

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox sTB = sender as TextBox;
            Countries[tabDynamic.SelectedIndex].mySearch = sTB.Text;
        }

        private void searchOsintTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox sTB = sender as TextBox;
            osintBased[tabDynamic1.SelectedIndex].mySearch1 = sTB.Text;
        }

        private void searchOsintGoogleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox sTB = sender as TextBox;
            osintGoogleBased[tabDynamicGoogle.SelectedIndex].mySearch1 = sTB.Text;
        }

        private void searchOsintfbTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox sTB = sender as TextBox;
            osintfbBased[tabDynamicfb.SelectedIndex].mySearch1 = sTB.Text;
        }

        private void fbPageList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;

            fbPageListItems gV = lVItem.Content as fbPageListItems;

            if (gV.fbPageMoreOptionVisibility == true)
            {
                if (gV.fbPageMoreOptionText.Equals("more") || gV.fbPageMoreOptionText.Equals("More"))
                {
                    int offset = 0;

                    offset = Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count;

                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].lastSearch;
                    searchTextBoxText1 = offset.ToString();

                    Countries[tabDynamic.SelectedIndex].fbPageListCollections[Countries[tabDynamic.SelectedIndex].
                        fbPageListCollections.Count - 1].fbPageMoreOptionText = "loading...";

                    Thread th = new Thread(MoreFbPagesForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }

            // add a tabItem with + in header 
            aTabItem plusOne = new aTabItem() { Header = gV.fbPage_LabelContent };
            plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/fb.png";
            plusOne.mySearch = "";
            plusOne.lastSearch = Countries[tabDynamic.SelectedIndex].lastSearch;
            plusOne.type = "fbPage";
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = false;
            plusOne.fbPageDivVisbility=true;
            plusOne.fbGroupDivVisbility = false;
            plusOne.fbUserDivVisbility = false;
            plusOne.twitterTweetListDivVisbility = false;
            plusOne.headerCloseIconVisibility = true;
            
            plusOne.fbPageTabInfo_ImageSource = gV.fbPage_ImageSource;
            plusOne.fbPageTabInfo_LabelContent= gV.fbPage_LabelContent;
            plusOne.fbPageTabInfo_ID = gV.fbPage_ID;

            plusOne.twitterUserTweetListLoadingVisibility = true;

            plusOne.twitterUserProfileTweetCountEllipseBg = "#aaaaaa";
            plusOne.twitterUserProfileFollowingCountEllipseBg = "#e1e1e1";
            plusOne.twitterUserProfileFollowersCountEllipseBg = "#e1e1e1";

            plusOne.twitterUserProfileFollowersListVisbility = false;
            plusOne.twitterUserProfileFollowingListVisbility = false;
            plusOne.twitterUserProfileTweetListVisbility = true;

            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...
            int len = Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count;

            for (int i = 0; i < len; i++)
            {
                plusOne.fbPageListCollections.Add(Countries[tabDynamic.SelectedIndex].fbPageListCollections[i]);
                if (gV.fbPage_ID.Equals(plusOne.fbPageListCollections[i].fbPage_ID))
                {
                    plusOne.fbPageListCollections[i].fbPage_BgColor = "#dddddd";
                    plusOne.fbPageListCollections[i].fbPage_SelectedUser = i;
                }
                else
                    plusOne.fbPageListCollections[i].fbPage_BgColor = "#ffffff";

                plusOne.fbPageListCollections[i].fbPage_myIndex = i;

            }//for loop...
            ///////////////////////////////////////////////////////////
            
            searchTextBoxText = gV.fbPage_ID;

            //String bbddd = plusOne.twitterUserListCollections[0].GridViewColumn_BgColor;

            Thread fbPageInfoInParallelThread = new Thread(getfBPageInfoInParallel);
            fbPageInfoInParallelThread.Start();
            
            //ts.TW_UserTweetsByID(gV.GridViewColumnName_ID);

            Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

            tabDynamic.SelectedItem = plusOne;
        
        }//func...

        private void fbGroupList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;

            fbGroupListItems gV = lVItem.Content as fbGroupListItems;

            if (gV.fbGroupMoreOptionVisibility == true)
            {
                if (gV.fbGroupMoreOptionText.Equals("more") || gV.fbGroupMoreOptionText.Equals("More"))
                {
                    int offset = 0;

                    offset = Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Count;

                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].lastSearch;
                    searchTextBoxText1 = offset.ToString();

                    Countries[tabDynamic.SelectedIndex].fbGroupListCollections[Countries[tabDynamic.SelectedIndex].
                        fbGroupListCollections.Count - 1].fbGroupMoreOptionText = "loading...";

                    Thread th = new Thread(MoreFbGroupsForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }

            // add a tabItem with + in header 
            aTabItem plusOne = new aTabItem() { Header = gV.fbGroup_LabelContent };
            plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/fb.png";
            plusOne.mySearch = "";
            plusOne.lastSearch = Countries[tabDynamic.SelectedIndex].lastSearch;
            plusOne.type = "fbGroup";
            plusOne.fbUserProfileLinkVisibility = false;
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = false;
            plusOne.fbPageDivVisbility = false;
            plusOne.fbGroupDivVisbility = true;
            plusOne.fbUserDivVisbility = false;
            plusOne.twitterTweetListDivVisbility = false;
            plusOne.headerCloseIconVisibility = true;

            plusOne.fbPageTabInfo_ImageSource = gV.fbGroup_ImageSource;
            plusOne.fbPageTabInfo_LabelContent = gV.fbGroup_LabelContent;
            plusOne.fbPageTabInfo_ID = gV.fbGroup_ID;

            plusOne.twitterUserTweetListLoadingVisibility = true;

            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...
            int len = Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Count;

            for (int i = 0; i < len; i++)
            {
                if (i == len - 1 && Countries[tabDynamic.SelectedIndex].fbGroupListCollections[i].fbGroupMoreOptionVisibility == true)
                {
                    fbPageListItems it1 = new fbPageListItems()
                    {
                        fbPage_ID = Countries[tabDynamic.SelectedIndex].fbGroupListCollections[i].fbGroup_ID,
                        fbPage_ImageSource = Countries[tabDynamic.SelectedIndex].fbGroupListCollections[i].fbGroup_ImageSource,
                        fbPage_LabelContent = Countries[tabDynamic.SelectedIndex].fbGroupListCollections[i].fbGroup_LabelContent,
                        fbPageMoreOptionVisibility = true,
                        fbPageMoreBehindOptionVisibility = false,
                        fbPageMoreOptionText="more",
                        fbPage_BgColor="#ffffff"
                    };

                    plusOne.fbPageListCollections.Add(it1);
                    break;
                }

                fbPageListItems it = new fbPageListItems()
                {
                    fbPage_ID = Countries[tabDynamic.SelectedIndex].fbGroupListCollections[i].fbGroup_ID,
                    fbPage_ImageSource = Countries[tabDynamic.SelectedIndex].fbGroupListCollections[i].fbGroup_ImageSource,
                    fbPage_LabelContent = Countries[tabDynamic.SelectedIndex].fbGroupListCollections[i].fbGroup_LabelContent,
                    fbPageMoreOptionVisibility=false,
                    fbPageMoreBehindOptionVisibility=true
                };

                plusOne.fbPageListCollections.Add(it);
                if (gV.fbGroup_ID.Equals(plusOne.fbPageListCollections[i].fbPage_ID))
                {
                    plusOne.fbPageListCollections[i].fbPage_BgColor = "#dddddd";
                    plusOne.fbPageListCollections[i].fbPage_SelectedUser = i;
                }
                else
                    plusOne.fbPageListCollections[i].fbPage_BgColor = "#ffffff";

                plusOne.fbPageListCollections[i].fbPage_myIndex = i;

            }//for loop...
            ///////////////////////////////////////////////////////////

            searchTextBoxText = gV.fbGroup_ID;

            //String bbddd = plusOne.twitterUserListCollections[0].GridViewColumn_BgColor;

            Thread fbGroupInfoInParallelThread = new Thread(getfBGroupInfoInParallel);
            fbGroupInfoInParallelThread.Start();

            //ts.TW_UserTweetsByID(gV.GridViewColumnName_ID);

            Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

            tabDynamic.SelectedItem = plusOne;

        }//func...

        private void fbUserList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;

            fbUserListItems gV = lVItem.Content as fbUserListItems;

            if (gV.fbUserMoreOptionVisibility == true)
            {
                if (gV.fbUserMoreOptionText.Equals("more") || gV.fbUserMoreOptionText.Equals("More"))
                {
                    int offset = 0;

                    offset = Countries[tabDynamic.SelectedIndex].fbUserListCollections.Count;

                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].lastSearch;
                    searchTextBoxText1 = offset.ToString();

                    Countries[tabDynamic.SelectedIndex].fbUserListCollections[Countries[tabDynamic.SelectedIndex].
                        fbUserListCollections.Count - 1].fbUserMoreOptionText = "loading...";

                    Thread th = new Thread(MoreFbUsersForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }


            // add a tabItem with + in header 
            aTabItem plusOne = new aTabItem() { Header = gV.GridViewColumnName_LabelContent };

            plusOne.mySearch = Countries[tabDynamic.SelectedIndex].lastSearch;
            plusOne.type = "fbUser";
            plusOne.fbUserProfileLink = "https://facebook.com/" + gV.GridViewColumnName_ID;
            plusOne.fbUserProfileLinkVisibility = true;
            plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/fb.png";
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = false;
            plusOne.fbPageDivVisbility = false;
            plusOne.fbGroupDivVisbility = false;
            plusOne.fbUserDivVisbility = true;
            plusOne.twitterTweetListDivVisbility = false;
            plusOne.headerCloseIconVisibility = true;

            plusOne.fbPageTabInfo_ImageSource = gV.GridViewColumnName_ImageSource;
            plusOne.fbPageTabInfo_LabelContent = gV.GridViewColumnName_LabelContent;
            plusOne.fbPageTabInfo_ID = gV.GridViewColumnName_ID;

            plusOne.twitterUserTweetListLoadingVisibility = true;

            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...
            int len = Countries[tabDynamic.SelectedIndex].fbUserListCollections.Count;

            for (int i = 0; i < len; i++)
            {
                if (i == len - 1 && Countries[tabDynamic.SelectedIndex].fbUserListCollections[i].fbUserMoreOptionVisibility == true)
                {
                    fbPageListItems it1 = new fbPageListItems()
                    {
                        fbPage_ID = Countries[tabDynamic.SelectedIndex].fbUserListCollections[i].GridViewColumnName_ID,
                        fbPageMoreBehindOptionVisibility = false,
                        fbPageMoreOptionVisibility = true,
                        fbPageMoreOptionText="more",
                        fbPage_BgColor="#ffffff"
                    };

                    plusOne.fbPageListCollections.Add(it1);

                    break;

                }//if (i == len - 1 && Countries[tabDynamic.SelectedIndex].fbUserListCollections[i].fbUserMoreOptionVisibility == true)...

                fbPageListItems it = new fbPageListItems()
                {
                    fbPage_ID = Countries[tabDynamic.SelectedIndex].fbUserListCollections[i].GridViewColumnName_ID,
                    fbPage_ImageSource = Countries[tabDynamic.SelectedIndex].fbUserListCollections[i].GridViewColumnName_ImageSource,
                    fbPage_LabelContent = Countries[tabDynamic.SelectedIndex].fbUserListCollections[i].GridViewColumnName_LabelContent,
                    fbPageMoreBehindOptionVisibility=true,
                    fbPageMoreOptionVisibility=false,
                };

                plusOne.fbPageListCollections.Add(it);
                if (gV.GridViewColumnName_ID.Equals(plusOne.fbPageListCollections[i].fbPage_ID))
                {
                    plusOne.fbPageListCollections[i].fbPage_BgColor = "#dddddd";
                    plusOne.fbPageListCollections[i].fbPage_SelectedUser = i;
                }
                else
                    plusOne.fbPageListCollections[i].fbPage_BgColor = "#ffffff";

                plusOne.fbPageListCollections[i].fbPage_myIndex = i;

            }//for loop...
            ///////////////////////////////////////////////////////////

            searchTextBoxText = gV.GridViewColumnName_ID;

            //String bbddd = plusOne.twitterUserListCollections[0].GridViewColumn_BgColor;

            Thread fbUserInfoInParallelThread = new Thread(getfBUserInfoInParallel);
            fbUserInfoInParallelThread.Start();

            //ts.TW_UserTweetsByID(gV.GridViewColumnName_ID);

            Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

            tabDynamic.SelectedItem = plusOne;

        }//func...

        public void twitterTweetToReport_ClickStatic()
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return;
            }

            Button but = buttonSender as Button;

            String ind = "";

            if (but.Tag != null)
            {
                ind = but.Tag.ToString();

                int index = Convert.ToInt32(ind);

                if (index >= 0)
                {

                    String desc = Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_desc;

                    Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

                    insertTweetIntoDocument(Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_name,
                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_screenName,
                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_desc,
                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_dateTime,
                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_userDp);

                }//if(index>=0)...

            }//if (but.Tag != null)...

        }//func...

        private void twitterTweetToReport_Click(object sender, MouseButtonEventArgs e)
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return; 
            }

            TextBox but = sender as TextBox;

            String ind = "";

            if (but.Tag != null)
            {
                 ind = but.Tag.ToString();

                int index=Convert.ToInt32(ind);

                if(index>=0){
                   
                    String desc = Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_desc;

                    Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

                    insertTweetIntoDocument(Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_name,
                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_screenName,
                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_desc,
                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_dateTime,
                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_userDp);

                }//if(index>=0)...

            }//if (but.Tag != null)...
            
        }//func...

        public void twitterAllTweetsToReport_ClickStatic()
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return;
            }

            //String desc = Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_desc;

            //Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

            insertAllTweetsIntoDocumentWithDocX(Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections);

        }//func...

        private void twitterAllTweetsToReport_Click(object sender, MouseButtonEventArgs e)
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return;
            }

                    //String desc = Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_desc;

                    //Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

            insertAllTweetsIntoDocumentWithDocX(Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections);

        }//func...

        private void fbPageAllStatusesToReport_Click(object sender, MouseButtonEventArgs e)
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return;
            }

            //String desc = Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_desc;

            //Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

            insertAllTweetsIntoDocumentWithDocX(Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections);

        }//func...

        private void fbGroupAllStatusesToReport_Click(object sender, MouseButtonEventArgs e)
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return;
            }

            //String desc = Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_desc;

            //Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

            insertAllTweetsIntoDocumentWithDocX(Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections);

        }//func...

        public void twitterUserInfoToReport_Click(object sender, MouseButtonEventArgs e)
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return;
            }
            
                    //Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

                    insertTwitterUserProfileInfoIntoDocumentUsingDocX(Countries[tabDynamic.SelectedIndex].twitterProfileUserTweetCount,
                        Countries[tabDynamic.SelectedIndex].twitterProfileUserName,
                        Countries[tabDynamic.SelectedIndex].twitterProfileUserId,
                        Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowingCount,
                        Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowerCount,
                        Countries[tabDynamic.SelectedIndex].twitterProfileUserDp,
                        Countries[tabDynamic.SelectedIndex].twitterProfileUserCityCountry,
                        Countries[tabDynamic.SelectedIndex].twitterProfileUserAge,
                        Countries[tabDynamic.SelectedIndex].twitterProfileUserAboutMe,
                        Countries[tabDynamic.SelectedIndex].twitterUserProfileUrl
                        );

            
        }//func...

        public void twitterUserInfoToReport_ClickStatic()
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return;
            }

            //Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

            insertTwitterUserProfileInfoIntoDocumentUsingDocX(Countries[tabDynamic.SelectedIndex].twitterProfileUserTweetCount,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserName,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserId,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowingCount,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowerCount,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserDp,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserCityCountry,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserAge,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserAboutMe,
                Countries[tabDynamic.SelectedIndex].twitterUserProfileUrl
                );


        }//func...

        public void removeBookmark_ClickStatic() 
        {
            MessageBox.Show("Hello...");
        }//func...

        public void twitterBookmarkUser_ClickStatic()
        {
            /**
                Countries[tabDynamic.SelectedIndex].twitterProfileUserTweetCount,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserName,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserId,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowingCount,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowerCount,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserDp,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserCityCountry,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserAge,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserAboutMe,
                Countries[tabDynamic.SelectedIndex].twitterUserProfileUrl
             */
            try
            {
                String name = "",type="",socialMedia="",id="",imag_url="",tweetCount="",followingCount="0",followersCount="0",cityCountry="",age="",aboutMe="",profile_url="";

                name= Countries[tabDynamic.SelectedIndex].twitterProfileUserName;
                type="user";socialMedia="twitter";
                id=Countries[tabDynamic.SelectedIndex].twitterProfileUserId;
                imag_url=Countries[tabDynamic.SelectedIndex].twitterProfileUserDp;

                if (alreadyBookmarked(id, type, socialMedia) == true)
                {
                    MessageBox.Show("Already bookmarked!");
                    return;
                }//if (alreadyBookmarked(id, type, socialMedia) == true)...

                tweetCount = Countries[tabDynamic.SelectedIndex].twitterProfileUserTweetCount;
                followingCount = Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowingCount;
                followersCount = Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowerCount;
                cityCountry = Countries[tabDynamic.SelectedIndex].twitterProfileUserCityCountry;
                age = Countries[tabDynamic.SelectedIndex].twitterProfileUserAge;
                aboutMe = Countries[tabDynamic.SelectedIndex].twitterProfileUserAboutMe;
                profile_url = Countries[tabDynamic.SelectedIndex].twitterUserProfileUrl;

                //File.AppendAllText("bookmarks.txt", name + ",.,.,.,.,." + type + ",.,.,.,.,." + socialMedia + ",.,.,.,.,." + id + ",.,.,.,.,." + imag_url + ",.,.,.,.,.");
                using (StreamWriter w = File.AppendText("bookmarks.txt"))
                {
                    w.WriteLine(name + ",.,.,.,.,." + type + ",.,.,.,.,." + socialMedia + ",.,.,.,.,." + id + ",.,.,.,.,." + imag_url + ",.,.,.,.,." +
                    tweetCount + ",.,.,.,.,." + followingCount + ",.,.,.,.,." + followersCount + ",.,.,.,.,." + cityCountry + ",.,.,.,.,." + age + ",.,.,.,.,." +
                    aboutMe + ",.,.,.,.,." + profile_url + ",.,.,.,.,.", "");
                }

                bookmarkListCollections.Add(new bookmarkListItems()
                {
                    bookmarkMoreOptionBehindVisibility = true,
                    bookmarkMoreOptionVisibility = true,
                    GridViewColumnName_ID = id,
                    GridViewColumnName_ImageSource = imag_url,
                    GridViewColumnName_LabelContent = name,
                    GridViewColumnSocialMedia = socialMedia,
                    GridViewColumnType = type,
                    
                    GridViewColumnIconFbOrTwitter="/WpfApplication2;component/Resources/twitter.png",
                    
                    GridViewColumnName_tweetsCount=tweetCount,
                    GridViewColumnName_FollowingCount=followingCount,
                    GridViewColumnName_FollowersCount=followersCount,
                    GridViewColumnName_CityCountry=cityCountry,
                    GridViewColumnName_Age=age,
                    GridViewColumnName_AboutMe=aboutMe,
                    twitterUserProfileUrl=profile_url
                });

                MessageBox.Show("User Bookmarked!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Oops! cannot be done right now...");
            }//catch...
        }//func...

        private void fbUserInfoToReport_Click(object sender, MouseButtonEventArgs e)
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return;
            }

            //Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

            insertfbUserProfileInfoIntoDocumentUsingDocX(Countries[tabDynamic.SelectedIndex].fbPageTabInfo_LabelContent,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_Category,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_location,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_desc
                );

        }//func...

        private void fbPageInfoToReport_Click(object sender, MouseButtonEventArgs e)
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return;
            }

            //Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

            insertfbPageProfileInfoIntoDocumentUsingDocX(Countries[tabDynamic.SelectedIndex].fbPageTabInfo_LabelContent,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_userName,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_Category,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_location,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_about,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_awards,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_canPost,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_checkIns,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_coverSource,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_description,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_hasAddedApp,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_isCommunityPage,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_isPublished,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_link,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_website,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_wereHere
                );


        }//func...

        private void fbGroupInfoToReport_Click(object sender, MouseButtonEventArgs e)
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return;
            }

            //Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

            insertfbGroupProfileInfoIntoDocumentUsingDocX(Countries[tabDynamic.SelectedIndex].fbPageTabInfo_LabelContent,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_link,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_userName,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_Category,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_location,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_awards,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_description,
                Countries[tabDynamic.SelectedIndex].fbPageTabInfo_checkIns
                );

        }//func...

        private void googleUserInfoToReport_Click(object sender, MouseButtonEventArgs e)
        {
            if (reportFileLocation == null || reportFileLocation.Length == 0 || reportFileLocation.Contains(".doc") == false)
            {
                MessageBox.Show("Please select Word File first to save report. Goto Configuration -> Change Reporting File to select Word file to save your report");
                return;
            }

            //Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections[index].twitterUserTweetListItems_reportButtonText = "Add to report Again";

            insertTwitterUserProfileInfoIntoDocumentUsingDocX(Countries[tabDynamic.SelectedIndex].twitterProfileUserTweetCount,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserName,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserId,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowingCount,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowerCount,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserDp,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserCityCountry,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserAge,
                Countries[tabDynamic.SelectedIndex].twitterProfileUserAboutMe,
                Countries[tabDynamic.SelectedIndex].twitterUserProfileUrl
                );


        }//func...

        public void getGoogleUserPostsInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                GS = new GoogleSearch("904980209954-1lcou02m0c5a3auodim9i0ue0dbg2lnt.apps.googleusercontent.com", "_p5GYoAWngP2a4PdfrpgYLqD");

                IList<Activity> data = GS.GP_GetAllActivities(GS.Service, searchTextBoxText,50,"");

                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    //IList<Activity> data1 = data;
                    
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = false;

                    foreach(Activity item in data){

                        if (item.Url == null) 
                        {
                            //here do paging...

                            break;
                        }//if (item.Url == null) ...

                    bool googleUserTab_dataImgVisibility = false;

                    string ActorDisplayName = "", Access = "no info", ActorDp = "", ActorId = "", Address = "no info", Annotation = "no info", CrosspostSource = "no info", Id = "",
                    ImageUrl = "", Geocode = "no info", ETag = "no info",
                    Kind = "no info", Location = "no info", PlaceId = "no info", PlaceName = "no info", plusOneCount = "", Provider = "no info", Published = "no info", PublishedRaw = "no info",
                    Radius = "no info", repliesCount = "", Verb = "no info",
                    ResharersCount = "", Title = "", updated = "no info", updatedRaw = "no info", Url = "no info";

                    ActorDisplayName = item.Actor.DisplayName; ActorDp = item.Actor.Image.Url; ActorId = item.Actor.Id;

                    if (item.Access != null) Access = item.Access.Description;
                    if (item.Address != null) Address = item.Address;
                    if (item.Annotation != null) Annotation = item.Annotation;
                    if (item.CrosspostSource!=null) CrosspostSource = item.CrosspostSource;
                    if (item.ETag != null) ETag = item.ETag;
                    if (item.Geocode != null) Geocode = item.Geocode;
                    if (item.Id != null) Id = item.Id;
                    if (item.Object__ != null && item.Object__.Attachments!=null&&item.Object__.Attachments[0].Image != null && item.Object__.Attachments[0].Image.Url != null && item.Object__.Attachments[0].Image.Url.Contains(".jpg"))
                    { ImageUrl = item.Object__.Attachments[0].Image.Url; googleUserTab_dataImgVisibility = true; }
                    if (item.Kind != null) Kind = item.Kind;
                    if (item.Location != null) Location = item.Location.Address.Formatted;
                    if (item.PlaceId != null) PlaceId = item.PlaceId;
                    if (item.PlaceName != null) PlaceName = item.PlaceName;
                    if (item.Provider != null) Provider = item.Provider.ToString();
                    if (item.Published != null) Published = item.Published.Value.ToLongTimeString()+" " + item.Published.Value.ToLongDateString();
                    if (item.PublishedRaw != null) PublishedRaw = item.PublishedRaw;
                    if (item.Radius != null) Radius = item.Radius;
                    
                    if (item.Object__.Plusoners.TotalItems != null) plusOneCount = item.Object__.Plusoners.TotalItems.ToString();
                    if (item.Object__.Replies.TotalItems != null) repliesCount = item.Object__.Replies.TotalItems.ToString();
                    if (item.Object__.Resharers.TotalItems != null) ResharersCount = item.Object__.Resharers.TotalItems.ToString();

                    if (item.Title != null) Title = item.Title;
                    if (item.Updated != null) updated = item.Updated.Value.ToLongTimeString() + " " + item.Updated.Value.ToLongDateString();
                    if (item.UpdatedRaw != null) updatedRaw = item.UpdatedRaw;
                    if (item.Verb != null) Verb = item.Verb;

                    Countries[tabDynamic.SelectedIndex].googleUserTabListCollections.Add(new googleUserPostsListItems() {
                    ActorDisplayName=ActorDisplayName,
                    Access="Access: "+Access,
                    ActorDp=ActorDp,
                    ActorId=ActorId,
                    Address="Address: "+Address,
                    Annotation="Annotation: "+Annotation,
                    CrosspostSource = "CrosspostSource: " + CrosspostSource,
                    ETag="ETag: "+ETag,
                    Geocode="Geocode: "+Geocode,
                    googleUserTab_dataImgVisibility=googleUserTab_dataImgVisibility,
                    Id=Id,
                    ImageUrl=ImageUrl,
                    Kind="Kind: "+Kind,
                    Location="Location: "+Location,
                    PlaceId="PlaceId: "+PlaceId,
                    PlaceName="PlaceName: "+PlaceName,
                    plusOneCount=plusOneCount,
                    Provider="Provide: "+Provider,
                    Published="Published: "+Published,
                    PublishedRaw="PublishedRaw: "+PublishedRaw,
                    Radius="Radius: "+Radius,
                    repliesCount=repliesCount,
                    ResharersCount=ResharersCount,
                    Title=Title,
                    updated="Updated: "+updated,
                    updatedRaw="UpdatedRaw: "+updatedRaw,
                    Url="Url: "+Url,
                    Verb="Verb: "+Verb ,
                    });
                    }//foreach...

                    if (data != null && (data.Count == 0 || (data.Count == 1 && data[0].Url == null)))
                    {
                        Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = true;

                        Countries[tabDynamic.SelectedIndex].fbUserProfileLink = @"https://plus.google.com/" + Countries[tabDynamic.SelectedIndex].GoogleUserId;
                        Countries[tabDynamic.SelectedIndex].fbUserProfileLinkVisibility = true;
                    }
                    else
                    {
                        Countries[tabDynamic.SelectedIndex].fbUserProfileLinkVisibility = false;
                    }//else...

                    Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;
                }));

            }//else...

        }//end of func... 

        public void getfBPageInfoInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                fs = new FacebookSearch(fbSessionId);
                fs1 = new FacebookSearch(fbSessionId);
                fs2 = new FacebookSearch(fbSessionId);

                fs.GetPageGeneralData(searchTextBoxText);

                JsonObject data1 = ((JsonObject)fs.FacebookData);

                String pageId="",likes_count = "", talking_about_count = "", category = "category: none", location = "location: none", descrip = "";

                String about = "about: none", awards = "awards: none", description = "description: none",link="link: none",username="Facebook Username: none",website="website: none",coverSource="cover_source: none";
                    String can_post="can_post: false",has_added_app="has_added_app: false",is_community_page="is_community_page: false",is_published="is_pubished: false";

                String checkins="checkins: 0",were_here_count="were_here_count: 0";

                if (data1.ContainsKey("awards") == true) awards = "awards: "+((String)data1["awards"]);
                if (data1.ContainsKey("description") == true) description = "description: "+((String)data1["description"]);
                if (data1.ContainsKey("link") == true) link = "link: "+((String)data1["link"]);
                if (data1.ContainsKey("username") == true) username = "Facebook Username: "+((String)data1["username"]);
                if (data1.ContainsKey("website") == true) website = "website: "+((String)data1["website"]);

                if (data1.ContainsKey("can_post") == true) can_post = "can_post: "+ Convert.ToString(((bool)data1["can_post"]));
                if (data1.ContainsKey("has_added_app") == true) has_added_app = "has_added_app: "+Convert.ToString(((bool)data1["has_added_app"]));
                if (data1.ContainsKey("is_community_page") == true) is_community_page = "is_community_page: "+Convert.ToString(((bool)data1["is_community_page"]));
                if (data1.ContainsKey("is_published") == true) is_published = "is_published: " + Convert.ToString(((bool)data1["is_published"]));

                if (data1.ContainsKey("checkins") == true) checkins = "checkins: "+Convert.ToString(((long)data1["checkins"]));
                if (data1.ContainsKey("were_here_count") == true) were_here_count = "were_here_count: "+Convert.ToString(((long)data1["were_here_count"]));

                if (data1.ContainsKey("about") == true) about = "about: "+((String)data1["about"]);
                if (data1.ContainsKey("id") == true) pageId = "Facebook id: "+((String)data1["id"]);
                if (data1.ContainsKey("likes") == true) likes_count = Convert.ToString(((long)data1["likes"]));
                if (data1.ContainsKey("talking_about_count") == true) talking_about_count = Convert.ToString(((long)data1["talking_about_count"]));
                if (data1.ContainsKey("category") == true) category = "category: "+((String)data1["category"]);
                if (data1.ContainsKey("location") == true) 
                {
                    String city = "", country = "";

                    JsonObject loca = ((JsonObject)data1["location"]);
                    if(loca.ContainsKey("city"))city=(String)(loca["city"]);
                    if (loca.ContainsKey("country")) country = (String)(loca["country"]);

                    if (city.Length > 0 && country.Length > 0) location = "location: "+city + "," + country;
                    else location = "location: " + city + country;
                    //pageId = ((String)data1["id"]); 
                }
                if (data1.ContainsKey("cover") == true)
                {
                    JsonObject cov = ((JsonObject)data1["cover"]);
                    if (cov.ContainsKey("source")) coverSource = "cover_source: "+(String)(cov["source"]);
                }
                
                App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_Category = category;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_location = location;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_about = about;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_likesCount = likes_count;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_talkingAboutCount = talking_about_count;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID = pageId;

                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_awards = awards;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_description = description;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_link = link;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_userName = username;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_website = website;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_canPost = can_post;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_hasAddedApp = has_added_app;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_isCommunityPage = is_community_page;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_isPublished = is_published;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_checkIns = checkins;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_wereHere = were_here_count;
                        Countries[tabDynamic.SelectedIndex].fbPageTabInfo_coverSource = coverSource;

                    }));

                fs = new FacebookSearch(fbSessionId);
                fs.GetPageDataByID(searchTextBoxText, "50","0");

                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = false;

                    }));
                
                //((string)(((JsonObject)fs.FacebookData)["name"]));...
                JsonArray data = null;
                
                if(fs.FacebookData!=null)
                {
                data=((JsonArray)((JsonObject)fs.FacebookData)["data"]);

                foreach (JsonObject status in data) 
                {
                    JsonObject from = ((JsonObject)status["from"]);
                    string data_img = "";string desc = "";bool dataImgVisibility=false;

                    string created_time = "",updated_time="";

                    string likesCount = "0", commentCount = "0", shareCount = "0";

                    if (((String)status["type"]).Equals("photo") && status.ContainsKey("full_picture")) 
                    {
                        data_img = ((String)status["full_picture"]); 
                        dataImgVisibility=true;
                    }//if (((String)status["type"]).Equals("photo") && status.ContainsKey("picture")) ...

                    if (status.ContainsKey("likes") == true) 
                    {
                        JsonObject likes= ((JsonObject)status["likes"]);
                        if (likes.ContainsKey("summary") == true) { likesCount = Convert.ToString(((long)((JsonObject)likes["summary"])["total_count"])); }
                    }

                    if (status.ContainsKey("comments") == true)
                    {
                        JsonObject comments = ((JsonObject)status["comments"]);
                        if (comments.ContainsKey("summary") == true) { commentCount = Convert.ToString(((long)((JsonObject)comments["summary"])["total_count"])); }
                    }
                    if (status.ContainsKey("shares") == true)
                    {
                        JsonObject share = ((JsonObject)status["shares"]);
                        shareCount = Convert.ToString((long)(share["count"]));
                    }
                    if (status.ContainsKey("created_time") == true) created_time = ((String)status["created_time"]);
                    if (status.ContainsKey("updated_time") == true) updated_time = ((String)status["updated_time"]);

                    if (status.ContainsKey("story") == true) desc = ((String)status["story"]);
                    else if (status.ContainsKey("message")==true)desc=((String)status["message"]);
                    else if (status.ContainsKey("description") == true) desc = ((String)status["description"]);

                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        //Do something here...
                        Countries[tabDynamic.SelectedIndex].fbPageTabListCollections.Add(new fbPageTabListItems()
                        {
                           fbPageTab_ID = ((String)status["id"]),
                           fbPageTab_ImageSource = "https://graph.facebook.com/"+((String)from["id"])+"/picture?type=large",
                           fbPageTab_LabelContent = ((String)from["name"]),
                           fbPageTab_desc = desc,//((String)status["story"]),
                           fbPageTab_type = ((String)status["type"]),
                           fbPageTab_createdTime="Created: "+fbDateTimeConverter(created_time)/*created_time*/,
                           fbPageTab_updatedTime = "Updated: " + fbDateTimeConverter(updated_time)/*updated_time*/,
                           fbPageTab_PhotoUrl=data_img,
                           fbPageTab_dataImgVisibility=dataImgVisibility,
                           fbPageTab_likesCount=likesCount,
                           fbPageTab_commentCount=commentCount,
                           fbPageTab_shareCount=shareCount
                        });

                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;
                        
                    }));

                }//foreach...
            }//if fs.facebook!=null...

                if (data != null && data.Count == 0)
                {
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = true;
                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;

                        Countries[tabDynamic.SelectedIndex].fbUserProfileLink = "https://facebook.com/" + searchTextBoxText;
                        Countries[tabDynamic.SelectedIndex].fbUserProfileLinkVisibility = true;
            

                    }));
                }
                else if (data == null)
                {
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = false;
                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;

                        //Countries[tabDynamic.SelectedIndex].fbUserProfileLink = "https://facebook.com/" + gV.fbPage_ID;
                        Countries[tabDynamic.SelectedIndex].fbUserProfileLinkVisibility = false;
            

                    }));
                }//else if data==null...

            }//else...

        }//end of func... 

        public void getfBGroupInfoInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                fs = new FacebookSearch(fbSessionId);
                fs1 = new FacebookSearch(fbSessionId);
                fs2 = new FacebookSearch(fbSessionId);

                fs.GetPageGeneralData(searchTextBoxText);

                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = false;

                }));

                JsonObject data1 = ((JsonObject)fs.FacebookData);

                String pageId = "", likes_count = "", talking_about_count = "", category = "owner_id: none", location = "location: none";

                String awards = "Privacy: none", description = "description: none", link = "link: none", username = "owner_name: none", website = "email: none";
                String can_post = "can_post: false", has_added_app = "has_added_app: false", is_community_page = "is_community_page: false", is_published = "is_pubished: false";

                String checkins = "updated_time: 0";

                if (data1.ContainsKey("privacy") == true) awards = "privacy: " + ((String)data1["privacy"]);
                if (data1.ContainsKey("description") == true) description = "description: " + ((String)data1["description"]);
                if (data1.ContainsKey("link") == true) link = "link: " + ((String)data1["link"]);
                ///////if (data1.ContainsKey("owner_name") == true) username = "owner_name: " + ((String)data1["owner_name"]);
                if (data1.ContainsKey("email") == true) website = "email: " + ((String)data1["email"]);

                if (data1.ContainsKey("can_post") == true) can_post = "can_post: " + Convert.ToString(((bool)data1["can_post"]));
                if (data1.ContainsKey("has_added_app") == true) has_added_app = "has_added_app: " + Convert.ToString(((bool)data1["has_added_app"]));
                if (data1.ContainsKey("is_community_page") == true) is_community_page = "is_community_page: " + Convert.ToString(((bool)data1["is_community_page"]));
                if (data1.ContainsKey("is_published") == true) is_published = "is_published: " + Convert.ToString(((bool)data1["is_published"]));

                if (data1.ContainsKey("updated_time") == true) checkins = "updated_time: " + Convert.ToString(((String)data1["updated_time"]));
                //if (data1.ContainsKey("were_here_count") == true) were_here_count = "were_here_count: " + Convert.ToString(((long)data1["were_here_count"]));

                //if (data1.ContainsKey("about") == true) about = "about: " + ((String)data1["about"]);
                if (data1.ContainsKey("id") == true) pageId = "Facebook id: " + ((String)data1["id"]);
                if (data1.ContainsKey("likes") == true) likes_count = Convert.ToString(((long)data1["likes"]));
                if (data1.ContainsKey("talking_about_count") == true) talking_about_count = Convert.ToString(((long)data1["talking_about_count"]));
                ///////////if (data1.ContainsKey("owner_id") == true) category = "owner_id: " + ((String)data1["owner_id"]);
                if (data1.ContainsKey("venue") == true)
                {
                    String city = "", country = "";

                    JsonObject loca = ((JsonObject)data1["venue"]);
                    if (loca.ContainsKey("city")) city = (String)(loca["city"]);
                    if (loca.ContainsKey("country")) country = (String)(loca["country"]);

                    if (city.Length > 0 && country.Length > 0) location = "location: " + city + "," + country;
                    else location = "location: " + city + country;
                    //pageId = ((String)data1["id"]); 
                }
                if (data1.ContainsKey("owner") == true)
                {
                    JsonObject cov = ((JsonObject)data1["owner"]);
                    if (cov.ContainsKey("name")) username = "owner_name: " + (String)(cov["name"]);
                }

                if (data1.ContainsKey("owner") == true)
                {
                    JsonObject cov = ((JsonObject)data1["owner"]);
                    if (cov.ContainsKey("id")) category = "owner_id: " + (String)(cov["id"]);
                }

                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_Category = category;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_location = location;
                    //Countries[tabDynamic.SelectedIndex].fbPageTabInfo_about = about;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_likesCount = likes_count;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_talkingAboutCount = talking_about_count;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID = pageId;

                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_awards = awards;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_description = description;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_link = link;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_userName = username;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_website = website;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_canPost = can_post;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_hasAddedApp = has_added_app;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_isCommunityPage = is_community_page;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_isPublished = is_published;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_checkIns = checkins;
                    ////Countries[tabDynamic.SelectedIndex].fbPageTabInfo_wereHere = were_here_count;
                    //Countries[tabDynamic.SelectedIndex].fbPageTabInfo_coverSource = coverSource;

                }));

                fs = new FacebookSearch(fbSessionId);
                fs.GetUserDataByID(searchTextBoxText, "50","0");

                //((string)(((JsonObject)fs.FacebookData)["name"]));...
                JsonArray data = null;

                if (fs.FacebookData != null)
                {
                    data = ((JsonArray)((JsonObject)fs.FacebookData)["data"]);

                    foreach (JsonObject status in data)
                    {
                        JsonObject from = ((JsonObject)status["from"]);
                        string data_img = ""; string desc = ""; bool dataImgVisibility = false;

                        string likesCount = "0", commentCount = "0", shareCount = "0";

                        string created_time = "", updated_time = "";

                        if (((String)status["type"]).Equals("photo") && status.ContainsKey("full_picture"))
                        {
                            data_img = ((String)status["full_picture"]);
                            dataImgVisibility = true;
                        }//if (((String)status["type"]).Equals("photo") && status.ContainsKey("picture")) ...


                        if (status.ContainsKey("likes") == true)
                        {
                            JsonObject likes = ((JsonObject)status["likes"]);
                            if (likes.ContainsKey("summary") == true) { likesCount = Convert.ToString(((long)((JsonObject)likes["summary"])["total_count"])); }
                        }

                        if (status.ContainsKey("comments") == true)
                        {
                            JsonObject comments = ((JsonObject)status["comments"]);
                            if (comments.ContainsKey("summary") == true) { commentCount = Convert.ToString(((long)((JsonObject)comments["summary"])["total_count"])); }
                        }
                        if (status.ContainsKey("shares") == true)
                        {
                            JsonObject share = ((JsonObject)status["shares"]);
                            shareCount = Convert.ToString((long)(share["count"]));
                        }

                        if (status.ContainsKey("story") == true) desc = ((String)status["story"]);
                        else if (status.ContainsKey("message") == true) desc = ((String)status["message"]);
                        else if (status.ContainsKey("caption") == true) desc = ((String)status["caption"]);

                        if (status.ContainsKey("created_time") == true) created_time = ((String)status["created_time"]);
                        if (status.ContainsKey("updated_time") == true) updated_time = ((String)status["updated_time"]);

                        App.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            //Do something here...
                            Countries[tabDynamic.SelectedIndex].fbPageTabListCollections.Add(new fbPageTabListItems()
                            {
                                fbPageTab_ID = ((String)status["id"]),
                                fbPageTab_ImageSource = "https://graph.facebook.com/" + ((String)from["id"]) + "/picture?type=large",
                                fbPageTab_LabelContent = ((String)from["name"]),
                                fbPageTab_desc = desc,//((String)status["story"]),
                                fbPageTab_type = ((String)status["type"]),
                                fbPageTab_createdTime = "Created: " + fbDateTimeConverter(created_time) /*created_time*/,
                                fbPageTab_updatedTime = "Updated: " + fbDateTimeConverter(updated_time)/*updated_time*/,
                                fbPageTab_PhotoUrl = data_img,
                                fbPageTab_dataImgVisibility = dataImgVisibility,
                                fbPageTab_likesCount = likesCount,
                                fbPageTab_commentCount = commentCount,
                                fbPageTab_shareCount = shareCount
                            });

                            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;

                        }));

                    }//foreach...
                }//if fs.facebook!=null...

                if (data != null && data.Count == 0)
                {
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = true;
                        Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;

                        Countries[tabDynamic.SelectedIndex].fbUserProfileLink = "https://facebook.com/" + searchTextBoxText;
                        Countries[tabDynamic.SelectedIndex].fbUserProfileLinkVisibility = true;

                    }));
                }
                else if (data == null)
                {
                    App.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = false;
                            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;

                            //Countries[tabDynamic.SelectedIndex].fbUserProfileLink = "https://facebook.com/" + searchTextBoxText;
                            Countries[tabDynamic.SelectedIndex].fbUserProfileLinkVisibility = false;
                        }));
                }//else if data==null...

            }//else...

        }//end of func... 

        public void getfBUserInfoInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                fs = new FacebookSearch(fbSessionId);
                fs1 = new FacebookSearch(fbSessionId);
                fs2 = new FacebookSearch(fbSessionId);

                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = false;
                
                }));

                fs.GetPageGeneralData(searchTextBoxText);

                JsonObject data1 = ((JsonObject)fs.FacebookData);

                String pageId = "", likes_count = "", talking_about_count = "", category = "category: User", location = "location: none", descrip = "description: none";

                if (data1.ContainsKey("id") == true) pageId = "Facebook id: "+((String)data1["id"]);
                if (data1.ContainsKey("likes") == true) likes_count = Convert.ToString(((long)data1["likes"]));
                if (data1.ContainsKey("talking_about_count") == true) talking_about_count = Convert.ToString(((long)data1["talking_about_count"]));
                if (data1.ContainsKey("category") == true) category = "category: "+((String)data1["category"]);
                if (data1.ContainsKey("link") == true)
                {location = "link: "+((String)data1["link"]);}
                if (data1.ContainsKey("updated_time") == true) descrip = "updated_time:"+((String)data1["updated_time"]);

                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    //Countries[tabDynamic.SelectedIndex].twitterProfileUserName = gV.GridViewColumnName_LabelContent;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_Category = category;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_location = location;
                    Countries[tabDynamic.SelectedIndex].fbPageTabInfo_desc = descrip;
                    //Countries[tabDynamic.SelectedIndex].fbPageTabInfo_likesCount = likes_count;
                    //Countries[tabDynamic.SelectedIndex].fbPageTabInfo_talkingAboutCount = talking_about_count;
                    //Countries[tabDynamic.SelectedIndex].twitterProfileUserTweetCount = gV.GridViewColumnName_tweetsCount;
                    //Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID = pageId;
                    //Countries[tabDynamic.SelectedIndex].twitterProfileUserDp = proImgUrl;//gV.GridViewColumnName_ImageSource;

                    Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = false;
                    Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = true;
                }));

                /**

                fs = new FacebookSearch(fbSessionId);
                fs.GetUserDataByID(searchTextBoxText, "10");

                //((string)(((JsonObject)fs.FacebookData)["name"]));...
                JsonArray data = null;

                if (fs.FacebookData != null)
                {
                    data = ((JsonArray)((JsonObject)fs.FacebookData)["data"]);

                    foreach (JsonObject status in data)
                    {
                        JsonObject from = ((JsonObject)status["from"]);
                        string data_img = ""; string desc = ""; bool dataImgVisibility = false;

                        if (((String)status["type"]).Equals("photo") && status.ContainsKey("picture"))
                        {
                            data_img = ((String)status["picture"]);
                            dataImgVisibility = true;
                        }//if (((String)status["type"]).Equals("photo") && status.ContainsKey("picture")) ...

                        if (status.ContainsKey("story") == true) desc = ((String)status["story"]);
                        else if (status.ContainsKey("message") == true) desc = ((String)status["message"]);
                        else if (status.ContainsKey("caption") == true) desc = ((String)status["caption"]);

                        App.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            //Do something here...
                            Countries[tabDynamic.SelectedIndex].fbPageTabListCollections.Add(new fbPageTabListItems()
                            {
                                fbPageTab_ID = ((String)status["id"]),
                                fbPageTab_ImageSource = "https://graph.facebook.com/" + ((String)from["id"]) + "/picture?type=large",
                                fbPageTab_LabelContent = ((String)from["name"]),
                                fbPageTab_desc = desc,//((String)status["story"]),
                                fbPageTab_type = ((String)status["type"]),
                                fbPageTab_PhotoUrl = data_img,
                                fbPageTab_dataImgVisibility = dataImgVisibility
                            });

                            

                        }));

                    }//foreach...
                }//if fs.facebook!=null...

                */

            }//else...

        }//end of func... 

        private void MoreTwitterUsersForSearch()
        {
            if (searchTextBoxText1 == null || searchTextBoxText1.Equals("0") || searchTextBoxText1.Equals("") || searchTextBoxText1.Equals(" "))
                return;

            ts = new TwitterSearch(
            "pNpMDTAW5Q0MACi912iQ8BhVy",
            "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
            "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
            "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

            ts.TW_UserSearchByNamePaging(searchTextBoxText, 50, Convert.ToInt32(searchTextBoxText1));

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (Countries[tabDynamic.SelectedIndex].twitterUserListCollections[Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Count - 1].twitterUserMoreOptionVisibility == true)
                    Countries[tabDynamic.SelectedIndex].twitterUserListCollections.RemoveAt(Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Count - 1);

                foreach (KeyValuePair<string, Dictionary<string, string>> item in ts.ResultSearch)
                {
                    Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                    string name = "", id = "", screenName = "", profile_image_url = "", description = "", age = "", location = "", followersCount = "", tweetsCount = "", FollowingCount = "";
                    bool tem = cc.TryGetValue("name", out name);
                    bool tem1 = cc.TryGetValue("image_url", out profile_image_url);
                    bool tem2 = cc.TryGetValue("description", out description);
                    bool tem3 = cc.TryGetValue("screenName", out age);
                    bool tem4 = cc.TryGetValue("location", out location);
                    tem4 = cc.TryGetValue("screenName", out screenName);
                    tem4 = cc.TryGetValue("id", out id);

                    tem1 = cc.TryGetValue("followersCount", out followersCount);
                    tem1 = cc.TryGetValue("tweetsCount", out tweetsCount);
                    tem1 = cc.TryGetValue("followingCount", out FollowingCount);

                    //Do something here...
                    Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Add(new twitterUserListItems()
                    {
                        GridViewColumnName_ID = id,
                        GridViewColumnName_ImageSource = profile_image_url,
                        GridViewColumnName_LabelContent = name,
                        GridViewColumnLocation = location,
                        GridViewColumnName_AboutMe = description,
                        GridViewColumnName_Age = screenName,
                        GridViewColumnName_CityCountry = location,
                        GridViewColumnName_LabelContentScreenName = "@" + screenName,
                        GridViewColumnName_FollowersCount = followersCount,
                        GridViewColumnName_FollowingCount = FollowingCount,
                        GridViewColumnName_tweetsCount = tweetsCount,
                        twitterUserMoreOptionBehindVisibility = true,
                        twitterUserMoreOptionVisibility = false,
                        GridViewColumn_BgColor="#ffffff"
                    });

                }//foreach...

                Countries[tabDynamic.SelectedIndex].twitterUserLoadingImageVisbility = false;

                Countries[tabDynamic.SelectedIndex].twitterUserListCollectionsCount = Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Count.ToString();

                if (((float)Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Count) % 50.0f == 0.0f)
                    Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Add(new twitterUserListItems()
                    {
                        twitterUserMoreOptionVisibility = true,
                        twitterUserMoreOptionBehindVisibility = false,
                        twitterUserMoreOptionText = "more",
                        GridViewColumn_BgColor="#ffffff",
                        GridViewColumnName_ID = Countries[tabDynamic.SelectedIndex].twitterUserListCollections[Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Count - 1].GridViewColumnName_ID
                    });

            }));


        }//func...

        private void MoreTwitterTweetsForSearch()
        {
            ts = new TwitterSearch(
            "pNpMDTAW5Q0MACi912iQ8BhVy",
            "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
            "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
            "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

            ICollection<KeyValuePair<String, Dictionary<string, string>>> temp = new Dictionary<String, Dictionary<string, string>>();
            ts.TW_TweetSearchByKeywordPaging(searchTextBoxText, 50,1, Convert.ToInt64(searchTextBoxText1));
            temp = ts.ResultSearchTweet;

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (Countries[tabDynamic.SelectedIndex].twitterTweetListCollections[Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count - 1].twitterTweetsMoreOptionVisibility == true)
                    Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.RemoveAt(Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count - 1);

                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    /**foreach (Tuple<String, String, String, String, String, String, String> item in temp)
                    {
                        Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Add(new twitterTweetListItems()
                        {
                            userScreenName = item.Item1,
                            desc = item.Item2,
                            dateTime = item.Item3,
                            userId = item.Item4,
                            tweetId = item.Item5,
                            userName = item.Item6,
                            userProfileImageUrl = item.Item7,
                            twitterTweetsMoreOptionBehindVisibility = true,
                            twitterTweetsMoreOptionVisibility = false
                        });
                    */
                    foreach (KeyValuePair<string, Dictionary<string, string>> item in temp)
                    {
                        Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                        DateTime dateTime;
                        String userName = "", tweetId = "", screenName = "", profile_image_url = "", description = "", photo = "", tweetUserId = "";
                        bool tem = cc.TryGetValue("userName", out userName);
                        bool tem1 = cc.TryGetValue("userprofileImgUrl", out profile_image_url);
                        bool tem2 = cc.TryGetValue("description", out description);
                        //bool tem3 = cc.TryGetValue("screenName", out age);
                        bool tem4 = cc.TryGetValue("photo", out photo);
                        tem4 = cc.TryGetValue("screenName", out screenName);
                        tem4 = cc.TryGetValue("tweetId", out tweetId);

                        tem1 = cc.TryGetValue("tweetUserId", out tweetUserId);
                        //tem1 = cc.TryGetValue("tweetsCount", out tweetsCount);
                        //tem1 = cc.TryGetValue("followingCount", out FollowingCount);

                        //tem1 = cc.TryGetValue("dateTime", out dateTime);

                        String actualDateTime;

                        tem1 = cc.TryGetValue("dateTime", out actualDateTime);

                        dateTime = DateTime.Parse(actualDateTime);

                        dateTime = dateTime.AddHours(5);

                        Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Add(new twitterTweetListItems()
                        {
                            userScreenName = screenName,
                            desc = description,
                            dateTime = dateTime.ToLongTimeString()+" "+dateTime.ToLongDateString()/*dateTime*/,
                            userId = tweetUserId,
                            tweetId = tweetId,
                            userName = userName,
                            userProfileImageUrl = profile_image_url,
                            twitterTweetsMoreOptionBehindVisibility = true,
                            twitterTweetsMoreOptionVisibility = false,
                            twitterTweetsPhotoVisibility=false,
                            twitterTweetsPhoto=photo
                        });
                    
                    }//foreach...
                    
                    Countries[tabDynamic.SelectedIndex].twitterTweetLoadingImageVisbility = false;
                    
                    Countries[tabDynamic.SelectedIndex].twitterTweetListCollectionsCount = Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count.ToString();
                }));

                Countries[tabDynamic.SelectedIndex].twitterTweetLoadingImageVisbility = false;

                Countries[tabDynamic.SelectedIndex].twitterTweetListCollectionsCount = Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count.ToString();

                if (((float)Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count) % 50.0f == 0.0f)
                    Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Add(new twitterTweetListItems()
                    {

                        twitterTweetsMoreOptionVisibility = true,
                        twitterTweetsMoreOptionBehindVisibility = false,
                        twitterTweetsMoreOptionText = "more",
                        tweetId = Countries[tabDynamic.SelectedIndex].twitterTweetListCollections[Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count - 1].tweetId
                    });
            }));

        }//func...

        private void MoreGoogleActivitiesForSearch()
        {
            IList<Activity> ActList = GS.GP_SearchActivities(GS.Service, searchTextBoxText, 20, searchTextBoxText1);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections[Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Count - 1].twitterTweetsMoreOptionVisibility == true)
                    Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.RemoveAt(Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Count - 1);

                foreach (Activity P in ActList)
                {
                    try
                    {
                        if (P.Url == null && ActList.Count == 20 + 1)
                        {
                            Countries[tabDynamic.SelectedIndex].googleActivitiesListCollectionsCount = Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Count.ToString();

                            if (Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Count > 0 && ((float)Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Count) % 20.0f == 0.0f)
                                Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Add(new twitterTweetListItems()
                                {
                                    twitterTweetsMoreOptionVisibility = true,
                                    twitterTweetsMoreOptionBehindVisibility = false,
                                    twitterTweetsMoreOptionText = "more",
                                    tweetId = ActList[ActList.Count - 1].Title
                                });

                            continue;
                        }//end of if...
                        else
                            Countries[tabDynamic.SelectedIndex].googleActivitiesListCollectionsCount = Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Count.ToString();


                        Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Add(new twitterTweetListItems()
                        {
                            tweetId = P.Id,
                            desc = P.Title,
                            dateTime = P.Published.ToString(),
                            userId = P.Actor.Id,
                            twitterTweetsMoreOptionVisibility = false,
                            twitterTweetsMoreOptionBehindVisibility = true
                        });
                    }
                    catch { }
                }

                Countries[tabDynamic.SelectedIndex].googleActivitiesLoadingImageVisbility = false;
                //Countries[tabDynamic.SelectedIndex].googleActivitiesListCollectionsCount = Countries[tabDynamic.SelectedIndex].googleActivitiesListCollections.Count.ToString();
            }));

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            /**
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                
                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    foreach (Tuple<String, String, String, String, String, String, String> item in temp)
                    {
                        Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Add(new twitterTweetListItems()
                        {
                            userScreenName = item.Item1,
                            desc = item.Item2,
                            dateTime = item.Item3,
                            userId = item.Item4,
                            tweetId = item.Item5,
                            userName = item.Item6,
                            userProfileImageUrl = item.Item7,
                            twitterTweetsMoreOptionBehindVisibility = true,
                            twitterTweetsMoreOptionVisibility = false
                        });

                    }//foreach...

                    Countries[tabDynamic.SelectedIndex].twitterTweetLoadingImageVisbility = false;

                    Countries[tabDynamic.SelectedIndex].twitterTweetListCollectionsCount = Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count.ToString();
                }));

                Countries[tabDynamic.SelectedIndex].twitterTweetLoadingImageVisbility = false;

                Countries[tabDynamic.SelectedIndex].twitterTweetListCollectionsCount = Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count.ToString();

                if (((float)Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count) % 10.0f == 0.0f)
                    Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Add(new twitterTweetListItems()
                    {

                        twitterTweetsMoreOptionVisibility = true,
                        twitterTweetsMoreOptionBehindVisibility = false,
                        twitterTweetsMoreOptionText = "more",
                        tweetId = Countries[tabDynamic.SelectedIndex].twitterTweetListCollections[Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count - 1].tweetId
                    });
            }));
            */
        }//func...

        private void MoreFbUsersForSearch()
        {
            if (searchTextBoxText1 == null || searchTextBoxText1.Equals("") || searchTextBoxText1.Equals(" "))
                return;

            fs = new FacebookSearch(fbSessionId);
            
            fs.Query(searchTextBoxText, "user", "", "50", searchTextBoxText1);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (Countries[tabDynamic.SelectedIndex].fbUserListCollections[Countries[tabDynamic.SelectedIndex].fbUserListCollections.Count - 1].fbUserMoreOptionVisibility == true)
                    Countries[tabDynamic.SelectedIndex].fbUserListCollections.RemoveAt(Countries[tabDynamic.SelectedIndex].fbUserListCollections.Count - 1);

                //*** Adding search result into some LIST listBoxSearch ****/
                foreach (KeyValuePair<string, string> item in fs.ResultSearch)
                {
                    //lbSearchResult.Items.Add(item.Value.ToString());
                    Countries[tabDynamic.SelectedIndex].fbUserListCollections.Add(new fbUserListItems()
                    {
                        GridViewColumnName_ID = item.Key,
                        GridViewColumnName_LabelContent = item.Value,
                        GridViewColumnName_ImageSource = @"https://graph.facebook.com/" + item.Key + @"/picture?type=large",
                        fbUserMoreOptionBehindVisibility = true,
                        fbUserMoreOptionVisibility = false
                    });

                    Countries[tabDynamic.SelectedIndex].fbUserLoadingImageVisbility = false;

                    Countries[tabDynamic.SelectedIndex].fbUserListCollectionsCount = Countries[tabDynamic.SelectedIndex].fbUserListCollections.Count.ToString();

                }//foreach...

                if (Countries[tabDynamic.SelectedIndex].fbUserListCollections.Count > 0 && ((float)Countries[tabDynamic.SelectedIndex].fbUserListCollections.Count) % 50.0f == 0.0f)
                    Countries[tabDynamic.SelectedIndex].fbUserListCollections.Add(new fbUserListItems()
                    {
                        fbUserMoreOptionVisibility = true,
                        fbUserMoreOptionBehindVisibility = false,
                        fbUserMoreOptionText = "more",
                        GridViewColumnName_ID = Countries[tabDynamic.SelectedIndex].fbUserListCollections[Countries[tabDynamic.SelectedIndex].fbUserListCollections.Count - 1].GridViewColumnName_ID
                    });
            }));

        }//func...

        private void MoreFbUsersInExpanderForSearch()
        {
            if (searchTextBoxText1 == null || searchTextBoxText1.Equals("") || searchTextBoxText1.Equals(" "))
                return;

            fs = new FacebookSearch(fbSessionId);

            fs.Query(searchTextBoxText, "user", "", "50", searchTextBoxText1);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (Countries[tabDynamic.SelectedIndex].fbPageListCollections[Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count - 1].fbPageMoreOptionVisibility == true)
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections.RemoveAt(Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count - 1);

                //*** Adding search result into some LIST listBoxSearch ****/
                foreach (KeyValuePair<string, string> item in fs.ResultSearch)
                {
                    //lbSearchResult.Items.Add(item.Value.ToString());
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections.Add(new fbPageListItems()
                    {
                        fbPage_ID = item.Key,
                        fbPage_LabelContent = item.Value,
                        fbPage_ImageSource = @"https://graph.facebook.com/" + item.Key + @"/picture?type=large",
                        fbPageMoreBehindOptionVisibility = true,
                        fbPageMoreOptionVisibility = false,
                        fbPage_BgColor="#ffffff"
                    });

                    Countries[tabDynamic.SelectedIndex].fbPageLoadingImageVisbility = false;

                    Countries[tabDynamic.SelectedIndex].fbPageListCollectionsCount = Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count.ToString();

                }//foreach...

                if (Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count > 0 && ((float)Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count) % 50.0f == 0.0f)
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections.Add(new fbPageListItems()
                    {
                        fbPageMoreOptionVisibility = true,
                        fbPageMoreBehindOptionVisibility = false,
                        fbPageMoreOptionText = "more",
                        fbPage_ID = Countries[tabDynamic.SelectedIndex].fbPageListCollections[Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count - 1].fbPage_ID,
                        fbPage_BgColor = "#ffffff"
                    });
            }));

        }//func...

        private void MoreFbPagesForSearch()
        {
            if (searchTextBoxText1 == null || searchTextBoxText1.Equals("") || searchTextBoxText1.Equals(" "))
                return;

            fs1 = new FacebookSearch(fbSessionId);

            fs1.Query(searchTextBoxText, "page", "", "50", searchTextBoxText1);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (Countries[tabDynamic.SelectedIndex].fbPageListCollections[Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count - 1].fbPageMoreOptionVisibility == true)
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections.RemoveAt(Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count - 1);

                //*** Adding search result into some LIST listBoxSearch ****/
                foreach (KeyValuePair<string, string> item in fs1.ResultSearch)
                {
                    //lbSearchResult.Items.Add(item.Value.ToString());
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections.Add(new fbPageListItems()
                    {
                        fbPage_ID = item.Key,
                        fbPage_LabelContent = item.Value,
                        fbPage_ImageSource = @"https://graph.facebook.com/" + item.Key + @"/picture?type=large",
                        fbPageMoreBehindOptionVisibility = true,
                        fbPageMoreOptionVisibility = false,
                        fbPage_BgColor="#ffffff"
                    });

                    Countries[tabDynamic.SelectedIndex].fbPageLoadingImageVisbility = false;
                    Countries[tabDynamic.SelectedIndex].fbPageListCollectionsCount = Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count.ToString();

                }//foreach...

                if (Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count > 0 && ((float)Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count) % 50.0f == 0.0f)
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections.Add(new fbPageListItems()
                    {
                        fbPageMoreOptionVisibility = true,
                        fbPageMoreBehindOptionVisibility = false,
                        fbPageMoreOptionText = "more",
                        fbPage_ID = Countries[tabDynamic.SelectedIndex].fbPageListCollections[Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count - 1].fbPage_ID,
                        fbPage_BgColor="#ffffff"
                    });
            }));

        }//func...

        private void MoreFbGroupsForSearch()
        {
            if (searchTextBoxText1 == null || searchTextBoxText1.Equals("") || searchTextBoxText1.Equals(" "))
                return;

            fs2 = new FacebookSearch(fbSessionId);

            fs2.Query(searchTextBoxText, "group", "", "50", searchTextBoxText1);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (Countries[tabDynamic.SelectedIndex].fbGroupListCollections[Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Count - 1].fbGroupMoreOptionVisibility == true)
                    Countries[tabDynamic.SelectedIndex].fbGroupListCollections.RemoveAt(Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Count - 1);

                //*** Adding search result into some LIST listBoxSearch ****/
                foreach (KeyValuePair<string, string> item in fs2.ResultSearch)
                {

                    //lbSearchResult.Items.Add(item.Value.ToString());
                    Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Add(new fbGroupListItems()
                    {
                        fbGroup_ID = item.Key,
                        fbGroup_LabelContent = item.Value,
                        fbGroup_ImageSource = @"https://graph.facebook.com/" + item.Key + @"/picture?type=large",
                        fbGroupMoreOptionBehindVisibility = true,
                        fbGroupMoreOptionVisibility = false
                    });

                    Countries[tabDynamic.SelectedIndex].fbGroupLoadingImageVisbility = false;
                    Countries[tabDynamic.SelectedIndex].fbGroupListCollectionsCount = Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Count.ToString();

                }//foreach...

                if (Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Count > 0 && ((float)Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Count) % 50.0f == 0.0f)
                    Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Add(new fbGroupListItems()
                    {
                        fbGroupMoreOptionVisibility = true,
                        fbGroupMoreOptionBehindVisibility = false,
                        fbGroupMoreOptionText = "more",
                        fbGroup_ID = Countries[tabDynamic.SelectedIndex].fbGroupListCollections[Countries[tabDynamic.SelectedIndex].fbGroupListCollections.Count - 1].fbGroup_ID
                    });
            }));

        }//func...

        private void MoreFbGroupsInExpanderForSearch()
        {

            if (searchTextBoxText1 == null || searchTextBoxText1.Equals("") || searchTextBoxText1.Equals(" "))
                return;

            fs2 = new FacebookSearch(fbSessionId);

            fs2.Query(searchTextBoxText, "group", "", "50", searchTextBoxText1);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (Countries[tabDynamic.SelectedIndex].fbPageListCollections[Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count - 1].fbPageMoreOptionVisibility == true)
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections.RemoveAt(Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count - 1);

                //*** Adding search result into some LIST listBoxSearch ****/
                foreach (KeyValuePair<string, string> item in fs2.ResultSearch)
                {
                    //lbSearchResult.Items.Add(item.Value.ToString());
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections.Add(new fbPageListItems()
                    {
                        fbPage_ID = item.Key,
                        fbPage_LabelContent = item.Value,
                        fbPage_ImageSource = @"https://graph.facebook.com/" + item.Key + @"/picture?type=large",
                        fbPageMoreBehindOptionVisibility = true,
                        fbPageMoreOptionVisibility = false,
                        fbPage_BgColor = "#ffffff"
                    });

                    Countries[tabDynamic.SelectedIndex].fbPageLoadingImageVisbility = false;

                    Countries[tabDynamic.SelectedIndex].fbPageListCollectionsCount = Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count.ToString();

                }//foreach...

                if (Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count > 0 && ((float)Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count) % 50.0f == 0.0f)
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections.Add(new fbPageListItems()
                    {
                        fbPageMoreOptionVisibility = true,
                        fbPageMoreBehindOptionVisibility = false,
                        fbPageMoreOptionText = "more",
                        fbPage_ID = Countries[tabDynamic.SelectedIndex].fbPageListCollections[Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count - 1].fbPage_ID,
                        fbPage_BgColor = "#ffffff"
                    });
            }));

        }//func...

        private void MoreGoogleUsersForSearch()
        {
            if (searchTextBoxText1 == null || searchTextBoxText1.Equals("") || searchTextBoxText1.Equals(" "))
                return;

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            // ** google plus...
            
            if (GS != null && GS.ResultSearch != null)
                GS.ResultSearch.Clear();
            else
                GS = new GoogleSearch("904980209954-1lcou02m0c5a3auodim9i0ue0dbg2lnt.apps.googleusercontent.com", "_p5GYoAWngP2a4PdfrpgYLqD");

            //Cursor.Current = Cursors.WaitCursor;
            IList<Person> SP = GS.GP_UserSearchByName(searchTextBoxText, 50,searchTextBoxText1);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                int sdsdsa = Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count;

                if (Countries[tabDynamic.SelectedIndex].googleUserListCollections[Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count - 1].googleUserMoreOptionVisibility == true)
                    Countries[tabDynamic.SelectedIndex].googleUserListCollections.RemoveAt(Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count - 1);

                foreach (Person P in SP)
                {
                    if (P.Url == null&&SP.Count==50+1)
                    {
                        Countries[tabDynamic.SelectedIndex].googleUserListCollectionsCount = Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count.ToString();

                        if (Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count > 0 && ((float)Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count) % 50.0f == 0.0f)
                            Countries[tabDynamic.SelectedIndex].googleUserListCollections.Add(new googleUserListItems()
                            {
                                googleUserMoreOptionVisibility = true,
                                googleUserMoreOptionBehindVisibility = false,
                                googleUserMoreOptionText = "more",
                                Id = SP[SP.Count-1].DisplayName,
                                GridViewColumn_BgColor="#ffffff"
                            });

                        continue;
                    }//end of if...
                    else
                        Countries[tabDynamic.SelectedIndex].googleUserListCollectionsCount = Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count.ToString();

                    try
                    {
                        int a = 0, b = 0;
                        bool c = false, d = false;
                        if (P.CircledByCount != null) { a = Convert.ToInt32(P.CircledByCount); }
                        if (P.PlusOneCount != null) { b = Convert.ToInt32(P.PlusOneCount); }
                        if (P.IsPlusUser != null) { c = Convert.ToBoolean(P.IsPlusUser); }
                        if (P.Verified != null) { d = Convert.ToBoolean(P.Verified); }

                        Countries[tabDynamic.SelectedIndex].googleUserListCollections.Add(new googleUserListItems()
                        {
                            Id = P.Id,
                            DisplayName = P.DisplayName,
                            ImageUrl = P.Image.Url,
                            aboutMe = P.AboutMe,
                            Birthday = P.Birthday,
                            BraggingRights = P.BraggingRights,
                            CircledByCount = a,
                            CurrentLocation = P.CurrentLocation,
                            Domain = P.Domain,
                            ETag = P.ETag,
                            Gender = P.Gender,
                            IsPlusUser = c,
                            Kind = P.Kind,
                            Language = P.Language,
                            NickName = P.Nickname,
                            ObjectType = P.ObjectType,
                            Occupation = P.Occupation,
                            PlusOneCount = b,
                            RelationshipStatus = P.RelationshipStatus,
                            Skills = P.Skills,
                            Tagline = P.Tagline,
                            Url = P.Url,
                            Verified = d,
                            googleUserMoreOptionBehindVisibility = true,
                            googleUserMoreOptionVisibility = false,
                            GridViewColumn_BgColor = "#ffffff"
                        });

                        //GS.ResultSearch.Add(new KeyValuePair<String, String>(P.Id.ToString(), P.DisplayName.ToString()));
                        //GP_lbSearchResults.Items.Add(P.DisplayName);
                    }
                    catch { }
                }

                Countries[tabDynamic.SelectedIndex].googleUserLoadingImageVisbility = false;
                //Countries[tabDynamic.SelectedIndex].googleUserListCollectionsCount = Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count.ToString();
            }));

        }//func...

        private void TwitterUserList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;
            
            twitterUserListItems gV = lVItem.Content as twitterUserListItems;

            if (gV.twitterUserMoreOptionVisibility == true) 
            {
                if (gV.twitterUserMoreOptionText.Equals("more") || gV.twitterUserMoreOptionText.Equals("More"))
                {
                    int pageNo = 0;

                    pageNo=(Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Count / 50) + 1;

                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].lastSearch;
                    searchTextBoxText1 = pageNo.ToString();

                    Countries[tabDynamic.SelectedIndex].twitterUserListCollections[Countries[tabDynamic.SelectedIndex].
                        twitterUserListCollections.Count - 1].twitterUserMoreOptionText = "loading...";

                    Thread th = new Thread(MoreTwitterUsersForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }

            String ddd=gV.GridViewColumnName_LabelContent;

            // add a tabItem with + in header...
            aTabItem plusOne = new aTabItem() { Header = gV.GridViewColumnName_LabelContent };
            plusOne.mySearch = "";
            plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/twitter.png";
            plusOne.lastSearch = Countries[tabDynamic.SelectedIndex].lastSearch;
            plusOne.type = "twitterUserProfile";
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = true;
            plusOne.twitterTweetListDivVisbility = false;
            plusOne.headerCloseIconVisibility = true;

            plusOne.twitterUserProfileTweetCountEllipseBg = "#aaaaaa";
            plusOne.twitterUserProfileFollowingCountEllipseBg = "#e1e1e1";
            plusOne.twitterUserProfileFollowersCountEllipseBg = "#e1e1e1";

            plusOne.twitterUserProfileFollowersListVisbility = false;
            plusOne.twitterUserProfileFollowingListVisbility = false;
            plusOne.twitterUserProfileTweetListVisbility = true;

            String proImgUrl = gV.GridViewColumnName_ImageSource;
            
                proImgUrl = proImgUrl.Replace("_normal.", ".");
                proImgUrl = proImgUrl.Replace("_bigger.", ".");

                String sName = gV.GridViewColumnName_LabelContentScreenName;
                if (sName.Contains("@") == false) sName = "@" + sName;

                String location = gV.GridViewColumnLocation;
                String aboutMe = gV.GridViewColumnName_AboutMe;
                if (location!=null&&location.Length == 0) { location = aboutMe; aboutMe = ""; }

            plusOne.twitterProfileUserName = gV.GridViewColumnName_LabelContent;
            plusOne.twitterProfileUserAge = sName;//"@"+gV.GridViewColumnName_LabelContentScreenName;
            plusOne.twitterProfileUserCityCountry = location;//gV.GridViewColumnLocation;
            plusOne.twitterProfileUserAboutMe = aboutMe;//gV.GridViewColumnName_AboutMe;
            plusOne.twitterProfileUserFollowingCount = gV.GridViewColumnName_FollowingCount;
            plusOne.twitterProfileUserFollowerCount =  gV.GridViewColumnName_FollowersCount;
            plusOne.twitterProfileUserTweetCount = gV.GridViewColumnName_tweetsCount;
            plusOne.twitterProfileUserId = gV.GridViewColumnName_ID;
            plusOne.twitterProfileUserDp = proImgUrl;//gV.GridViewColumnName_ImageSource;
            plusOne.twitterUserProfileUrl = gV.GridViewColumn_Url;

            plusOne.twitterUserTweetListLoadingVisibility = true;

            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...
            int len=Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Count;

            for (int i = 0; i < len; i++) 
            { 
                plusOne.twitterUserListCollections.Add(Countries[tabDynamic.SelectedIndex].twitterUserListCollections[i]);
                if (gV.GridViewColumnName_ID.Equals(plusOne.twitterUserListCollections[i].GridViewColumnName_ID))
                {
                    plusOne.twitterUserListCollections[i].GridViewColumn_BgColor = "#dddddd";
                    plusOne.twitterUserListCollections[i].GridViewColumnName_SelectedUser = i;
                }
                else
                    plusOne.twitterUserListCollections[i].GridViewColumn_BgColor = "#ffffff";

                plusOne.twitterUserListCollections[i].GridViewColumnName_myIndex = i;
            
            }//for loop...
            ///////////////////////////////////////////////////////////

            searchTextBoxText = gV.GridViewColumnName_ID;

            //String bbddd=plusOne.twitterUserListCollections[0].GridViewColumn_BgColor;

            Thread twitterUserTweetsInParallelThread = new Thread(getTwitterUserTweetsInParallel);
            twitterUserTweetsInParallelThread.Start();

            //ts.TW_UserTweetsByID(gV.GridViewColumnName_ID);
     
            Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

            tabDynamic.SelectedItem = plusOne;
        }

        private void individualPostData_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            individualPostData.Visibility = Visibility.Visible;
            backBlackForIndividualGooglePost.Visibility = Visibility.Visible;

            ListViewItem lVItem = sender as ListViewItem;

            googleUserPostsListItems gV = lVItem.Content as googleUserPostsListItems;

            ActorDp.Source = new BitmapImage(new Uri(gV.ActorDp));
            ActorDisplayName.Text = gV.ActorDisplayName;
            googleUserPostId.Text = gV.Id;
            googlePostTitle.Text = gV.Title;

            a1.Text = gV.Access;
            a2.Text = gV.Address;
            a3.Text = gV.Annotation;
            a4.Text = gV.CrosspostSource;
            a5.Text = gV.ETag;
            a6.Text = gV.Kind;
            a7.Text = gV.Location;
            a8.Text = gV.PlaceId;
            a9.Text = gV.PlaceName;
            a10.Text = gV.Provider;
            a11.Text = gV.Published;
            a12.Text = gV.PublishedRaw;
            a13.Text = gV.Radius;
            a14.Text = gV.updated;
            a15.Text = gV.updatedRaw;
            a16.Text = gV.Url;
            a17.Text = gV.Verb;

            if (gV.googleUserTab_dataImgVisibility == false)
                googleUserTab_dataImgVisibility.Visibility = Visibility.Collapsed;
            else
            {
                googleUserTab_dataImgVisibility.Visibility = Visibility.Visible;
                googleUserPostImageUrl.Source = new BitmapImage(new Uri(gV.ImageUrl));

                //googleUserPostImageUrl.Source = gV.ImageUrl;
            }

            plusOneCount.Text = gV.plusOneCount;
            repliesCount.Text = gV.repliesCount;
            ResharersCount.Text = gV.ResharersCount;
            
        }//func...

        private void googleUserList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;

            googleUserListItems P = lVItem.Content as googleUserListItems;

            if (P.googleUserMoreOptionVisibility == true)
            {
                if (P.googleUserMoreOptionText.Equals("more") || P.googleUserMoreOptionText.Equals("More"))
                {
                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].lastSearch;
                    searchTextBoxText1 = Countries[tabDynamic.SelectedIndex].googleUserListCollections[Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count - 1].Id;

                    Countries[tabDynamic.SelectedIndex].googleUserListCollections[Countries[tabDynamic.SelectedIndex].
                        googleUserListCollections.Count - 1].googleUserMoreOptionText = "loading...";

                    Thread th = new Thread(MoreGoogleUsersForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }

            String ddd = P.DisplayName;

            // add a tabItem with + in header...
            aTabItem plusOne = new aTabItem() { Header = P.DisplayName };
            plusOne.mySearch = "";
            plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/google_plus_icon_small.png";
            plusOne.type = "googleUserProfile";
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = false;
            plusOne.twitterTweetListDivVisbility = false;
            plusOne.headerCloseIconVisibility = true;
            plusOne.googleUserDivVisbility = true;
            
            plusOne.twitterUserProfileFollowersListVisbility = false;
            plusOne.twitterUserProfileFollowingListVisbility = false;
            plusOne.googleUserProfileActivitiesListVisbility = true;

            int a = 0, b = 0;
            bool c = false, d = false;
            String aboutMe = "About Me: no info", Birthday = "Birthday: no info", BraggingRights = "BraggingRights: no info", CurrentLocation = "CurrentLocation: no info",
                Domain = "Domain: no info", ETag = "ETag: no info", Gender = "Gender: no info",
                Kind = "Kind: no info", language = "Language: no info", nickName = "NickName: no info", objectType = "ObjectType: no info", occupation = "Occupation: no info",
                relationShip = "Relationships: no info", skills = "Skills: no info", tagline = "Tagline: no info", url = "Url: no info";

            if (P.CircledByCount != null) { a = Convert.ToInt32(P.CircledByCount); }
            if (P.PlusOneCount != null) { b = Convert.ToInt32(P.PlusOneCount); }
            if (P.IsPlusUser != null) { c = Convert.ToBoolean(P.IsPlusUser); }
            if (P.Verified != null) { d = Convert.ToBoolean(P.Verified); }

            if (P.aboutMe != null) aboutMe = "About Me: " + P.aboutMe;
            if (P.Birthday != null) aboutMe = "Birthday: " + P.Birthday;
            if (P.BraggingRights != null) BraggingRights = "Bragging Rights: " + P.BraggingRights;
            if (P.CurrentLocation != null) CurrentLocation = "Current Location: " + P.CurrentLocation;

            if (P.Domain != null) Domain = "Domain: " + P.Domain;
            if (P.ETag != null) ETag = "ETag: " + P.ETag;
            if (P.Gender != null) Gender = "Gender: " + P.Gender;

            if (P.Kind != null) Kind = "Kind: " + P.Kind;
            if (P.Language != null) language = "language: " + P.Language;
            if (P.NickName != null) nickName = "NickName: " + P.NickName;

            if (P.ObjectType != null) objectType = "Object Type: " + P.ObjectType;
            if (P.Occupation != null) occupation = "Ocupation: " + P.Occupation;
            if (P.RelationshipStatus != null) relationShip = "Relationship Status: " + P.RelationshipStatus;
            if (P.Skills != null) skills = "Skills: " + P.Skills;

            if (P.Tagline != null) tagline = "TagLine: " + P.Tagline;
            if (P.Url != null) url = "Url: " + P.Url;

                plusOne.GoogleUserId = P.Id;
                plusOne.GoogleUserDisplayName= P.DisplayName;
                plusOne.GoogleUserImageUrl= P.ImageUrl;
                plusOne.aboutMe = aboutMe;
                plusOne.Birthday = Birthday;
                plusOne.BraggingRights = BraggingRights;
                plusOne.CircledByCount = "CircledByCount: " + a.ToString();
                plusOne.CurrentLocation = CurrentLocation;
                plusOne.Domain = Domain;
                plusOne.ETag = ETag;
                plusOne.Gender = Gender;
                plusOne.IsPlusUser ="IsPlusUser: "+ c.ToString();
                plusOne.Kind = Kind;
                plusOne.Language = language;
                plusOne.NickName = nickName;
                plusOne.ObjectType = objectType;
                plusOne.Occupation = occupation;
                plusOne.PlusOneCount = "PlusOneCount: "+b.ToString();
                plusOne.RelationshipStatus = relationShip;
                plusOne.Skills = "Skills: "+skills;
                plusOne.Tagline = tagline;
                plusOne.Url = url;
                plusOne.Verified = "Verified: "+d.ToString();
            
            plusOne.twitterUserTweetListLoadingVisibility = true;
            
            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...
            int len = Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count;

            for (int i = 0; i < len; i++)
            {
                plusOne.googleUserListCollections.Add(Countries[tabDynamic.SelectedIndex].googleUserListCollections[i]);
                if (P.Id.Equals(plusOne.googleUserListCollections[i].Id))
                {
                    plusOne.googleUserListCollections[i].GridViewColumn_BgColor = "#dddddd";
                    plusOne.googleUserListCollections[i].GridViewColumnName_SelectedUser = i;
                }
                else
                    plusOne.googleUserListCollections[i].GridViewColumn_BgColor = "#ffffff";

                plusOne.googleUserListCollections[i].GridViewColumnName_myIndex = i;

            }//for loop...
            ///////////////////////////////////////////////////////////

            searchTextBoxText = P.Id;

            //String bbddd=plusOne.twitterUserListCollections[0].GridViewColumn_BgColor;

            Thread googleUserPostsInParallelThread = new Thread(getGoogleUserPostsInParallel);
            googleUserPostsInParallelThread.Start();

            //ts.TW_UserTweetsByID(gV.GridViewColumnName_ID);

            Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

            tabDynamic.SelectedItem = plusOne;
        }//func...

        private void TwitterFollowersListInAProfile_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;

            twitterUserListItems gV = lVItem.Content as twitterUserListItems;
            String ddd = gV.GridViewColumnName_LabelContent;

            // add a tabItem with + in header...
            aTabItem plusOne = new aTabItem() { Header = gV.GridViewColumnName_LabelContent };
            plusOne.mySearch = "";
            plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/twitter.png";
            plusOne.type = "twitterUserProfile";
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = true;
            plusOne.twitterTweetListDivVisbility = false;
            plusOne.headerCloseIconVisibility = true;

            plusOne.twitterUserProfileTweetCountEllipseBg = "#aaaaaa";
            plusOne.twitterUserProfileFollowingCountEllipseBg = "#e1e1e1";
            plusOne.twitterUserProfileFollowersCountEllipseBg = "#e1e1e1";

            plusOne.twitterUserProfileFollowersListVisbility = false;
            plusOne.twitterUserProfileFollowingListVisbility = false;
            plusOne.twitterUserProfileTweetListVisbility = true;

            String proImgUrl = gV.GridViewColumnName_ImageSource;

            proImgUrl = proImgUrl.Replace("_normal.", ".");
            proImgUrl = proImgUrl.Replace("_bigger.", ".");

            String sName = gV.GridViewColumnName_LabelContentScreenName;
            if (sName.Contains("@") == false) sName = "@" + sName;

            String location = gV.GridViewColumnLocation;
            String aboutMe = gV.GridViewColumnName_AboutMe;
            if (location != null && location.Length == 0) { location = aboutMe; aboutMe = ""; }

            plusOne.twitterProfileUserName = gV.GridViewColumnName_LabelContent;
            plusOne.twitterProfileUserAge = sName;//"@"+gV.GridViewColumnName_LabelContentScreenName;
            plusOne.twitterProfileUserCityCountry = location;//gV.GridViewColumnLocation;
            plusOne.twitterProfileUserAboutMe = aboutMe;//gV.GridViewColumnName_AboutMe;
            plusOne.twitterProfileUserFollowingCount = gV.GridViewColumnName_FollowingCount;
            plusOne.twitterProfileUserFollowerCount = gV.GridViewColumnName_FollowersCount;
            plusOne.twitterProfileUserTweetCount = gV.GridViewColumnName_tweetsCount;
            plusOne.twitterProfileUserId = gV.GridViewColumnName_ID;
            plusOne.twitterProfileUserDp = proImgUrl;//gV.GridViewColumnName_ImageSource;

            plusOne.twitterUserTweetListLoadingVisibility = true;

            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...
            int len = Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListCollections.Count;

            for (int i = 0; i < len; i++)
            {
                plusOne.twitterUserListCollections.Add(Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListCollections[i]);
                if (gV.GridViewColumnName_ID.Equals(plusOne.twitterUserListCollections[i].GridViewColumnName_ID))
                {
                    plusOne.twitterUserListCollections[i].GridViewColumn_BgColor = "#dddddd";
                    plusOne.twitterUserListCollections[i].GridViewColumnName_SelectedUser = i;
                }
                else
                    plusOne.twitterUserListCollections[i].GridViewColumn_BgColor = "#ffffff";

                plusOne.twitterUserListCollections[i].GridViewColumnName_myIndex = i;

            }//for loop...
            ///////////////////////////////////////////////////////////

            searchTextBoxText = gV.GridViewColumnName_ID;

            //String bbddd=plusOne.twitterUserListCollections[0].GridViewColumn_BgColor;

            Thread twitterUserTweetsInParallelThread = new Thread(getTwitterUserTweetsInParallel);
            twitterUserTweetsInParallelThread.Start();

            //ts.TW_UserTweetsByID(gV.GridViewColumnName_ID);

            Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

            tabDynamic.SelectedItem = plusOne;
        }

        private void TwitterFollowingListInAProfile_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;

            twitterUserListItems gV = lVItem.Content as twitterUserListItems;
            String ddd = gV.GridViewColumnName_LabelContent;

            // add a tabItem with + in header...
            aTabItem plusOne = new aTabItem() { Header = gV.GridViewColumnName_LabelContent };
            plusOne.mySearch = "";
            plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/twitter.png";
            plusOne.type = "twitterUserProfile";
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = true;
            plusOne.twitterTweetListDivVisbility = false;
            plusOne.headerCloseIconVisibility = true;

            plusOne.twitterUserProfileTweetCountEllipseBg = "#aaaaaa";
            plusOne.twitterUserProfileFollowingCountEllipseBg = "#e1e1e1";
            plusOne.twitterUserProfileFollowersCountEllipseBg = "#e1e1e1";

            plusOne.twitterUserProfileFollowersListVisbility = false;
            plusOne.twitterUserProfileFollowingListVisbility = false;
            plusOne.twitterUserProfileTweetListVisbility = true;

            String proImgUrl = gV.GridViewColumnName_ImageSource;

            proImgUrl = proImgUrl.Replace("_normal.", ".");
            proImgUrl = proImgUrl.Replace("_bigger.", ".");

            String sName = gV.GridViewColumnName_LabelContentScreenName;
            if (sName.Contains("@") == false) sName = "@" + sName;

            String location = gV.GridViewColumnLocation;
            String aboutMe = gV.GridViewColumnName_AboutMe;
            if (location != null && location.Length == 0) { location = aboutMe; aboutMe = ""; }

            plusOne.twitterProfileUserName = gV.GridViewColumnName_LabelContent;
            plusOne.twitterProfileUserAge = sName;//"@"+gV.GridViewColumnName_LabelContentScreenName;
            plusOne.twitterProfileUserCityCountry = location;//gV.GridViewColumnLocation;
            plusOne.twitterProfileUserAboutMe = aboutMe;//gV.GridViewColumnName_AboutMe;
            plusOne.twitterProfileUserFollowingCount = gV.GridViewColumnName_FollowingCount;
            plusOne.twitterProfileUserFollowerCount = gV.GridViewColumnName_FollowersCount;
            plusOne.twitterProfileUserTweetCount = gV.GridViewColumnName_tweetsCount;
            plusOne.twitterProfileUserId = gV.GridViewColumnName_ID;
            plusOne.twitterProfileUserDp = proImgUrl;//gV.GridViewColumnName_ImageSource;

            plusOne.twitterUserTweetListLoadingVisibility = true;

            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...
            int len = Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListCollections.Count;

            for (int i = 0; i < len; i++)
            {
                plusOne.twitterUserListCollections.Add(Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListCollections[i]);
                if (gV.GridViewColumnName_ID.Equals(plusOne.twitterUserListCollections[i].GridViewColumnName_ID))
                {
                    plusOne.twitterUserListCollections[i].GridViewColumn_BgColor = "#dddddd";
                    plusOne.twitterUserListCollections[i].GridViewColumnName_SelectedUser = i;
                }
                else
                    plusOne.twitterUserListCollections[i].GridViewColumn_BgColor = "#ffffff";

                plusOne.twitterUserListCollections[i].GridViewColumnName_myIndex = i;

            }//for loop...
            ///////////////////////////////////////////////////////////

            searchTextBoxText = gV.GridViewColumnName_ID;

            //String bbddd=plusOne.twitterUserListCollections[0].GridViewColumn_BgColor;

            Thread twitterUserTweetsInParallelThread = new Thread(getTwitterUserTweetsInParallel);
            twitterUserTweetsInParallelThread.Start();

            //ts.TW_UserTweetsByID(gV.GridViewColumnName_ID);

            Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

            tabDynamic.SelectedItem = plusOne;
        }

        private void TwitterTweetList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;

            twitterTweetListItems gV = lVItem.Content as twitterTweetListItems;

            if (gV.twitterTweetsMoreOptionVisibility == true)
            {
                if (gV.twitterTweetsMoreOptionText.Equals("more") || gV.twitterTweetsMoreOptionText.Equals("More"))
                {
                    //int pageNo = 0;

                    //pageNo = (Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count / 10) + 1;

                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].lastSearch;
                    searchTextBoxText1 = gV.tweetId;

                    Countries[tabDynamic.SelectedIndex].twitterTweetListCollections[Countries[tabDynamic.SelectedIndex].
                        twitterTweetListCollections.Count - 1].twitterTweetsMoreOptionText = "loading...";

                    Thread th = new Thread(MoreTwitterTweetsForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }
            
            // add a tabItem with + in header 
            aTabItem plusOne = new aTabItem() { Header = gV.userName };
            plusOne.mySearch = "";
            plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/twitter.png";
            plusOne.Header = Countries[tabDynamic.SelectedIndex].lastSearch;
            plusOne.lastSearch = Countries[tabDynamic.SelectedIndex].lastSearch;
            plusOne.type = "twitterTweetList";
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.twitterTweetPageListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = false;
            plusOne.twitterTweetListDivVisbility = true;
            plusOne.headerCloseIconVisibility = true;
            //plusOne.Header = gV.userName;
            
            int len=Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count;

            foreach (twitterTweetListItems item in Countries[tabDynamic.SelectedIndex].twitterTweetListCollections)
            {
                bool photoVisibility = false;

                if (item.twitterTweetsPhoto != null && item.twitterTweetsPhoto.Trim().Length > 0)
                    photoVisibility = true;

                plusOne.twitterTweetPageListCollections.Add(new twitterTweetPageListItems() { 
                twitterTweetPageListItems_dateTime=item.dateTime,
                twitterTweetPageListItems_desc=item.desc,
                twitterTweetPageListItems_Id=item.tweetId,
                twitterTweetPageListItems_name=item.userName,
                twitterTweetPageListItems_screenName=item.userScreenName,
                twitterTweetPageListItems_userDp=item.userProfileImageUrl,
                twitterTweetsPhoto=item.twitterTweetsPhoto,
                twitterTweetsPhotoVisibility=photoVisibility
                
                });
            }//foreach loop...

            plusOne.twitterTweetTweetListLoadingVisibility = false;

            Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

            tabDynamic.SelectedItem = plusOne;
            
        }//end of func...

        private void googleActivitiesList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;

            twitterTweetListItems gV = lVItem.Content as twitterTweetListItems;

            if (gV.twitterTweetsMoreOptionVisibility == true)
            {
                if (gV.twitterTweetsMoreOptionText.Equals("more") || gV.twitterTweetsMoreOptionText.Equals("More"))
                {
                    //int pageNo = 0;

                    //pageNo = (Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count / 10) + 1;

                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].lastSearch;
                    searchTextBoxText1 = gV.tweetId;

                    Countries[tabDynamic.SelectedIndex].twitterTweetListCollections[Countries[tabDynamic.SelectedIndex].
                        twitterTweetListCollections.Count - 1].twitterTweetsMoreOptionText = "loading...";

                    Thread th = new Thread(MoreGoogleActivitiesForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }

            // add a tabItem with + in header 
            aTabItem plusOne = new aTabItem() { Header = gV.userName };
            plusOne.mySearch = "";
            plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/google_plus_icon_small.png";
            plusOne.Header = Countries[tabDynamic.SelectedIndex].lastSearch;
            plusOne.lastSearch = Countries[tabDynamic.SelectedIndex].lastSearch;
            plusOne.type = "twitterTweetList";
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.twitterTweetPageListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = false;
            plusOne.twitterTweetListDivVisbility = true;
            plusOne.headerCloseIconVisibility = true;
            
            int len = Countries[tabDynamic.SelectedIndex].twitterTweetListCollections.Count;

            foreach (twitterTweetListItems item in Countries[tabDynamic.SelectedIndex].twitterTweetListCollections)
            {
                plusOne.twitterTweetPageListCollections.Add(new twitterTweetPageListItems()
                {
                    twitterTweetPageListItems_dateTime = item.dateTime,
                    twitterTweetPageListItems_desc = item.desc,
                    twitterTweetPageListItems_Id = item.tweetId,
                    twitterTweetPageListItems_name = item.userName,
                    twitterTweetPageListItems_screenName = item.userScreenName,
                    twitterTweetPageListItems_userDp = item.userProfileImageUrl
                });
            }//foreach loop...

            plusOne.twitterTweetTweetListLoadingVisibility = false;

            Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

            tabDynamic.SelectedItem = plusOne;

        }//end of func...

        private void TwitterUserInExpanderList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;
            //ListView lV = lVItem.TemplatedParent as ListView;
            //Button button = sender as Button;
            //int index = _myListBoxName.Items.IndexOf(button.DataContext);

            twitterUserListItems gV = lVItem.Content as twitterUserListItems;

            if (gV.twitterUserMoreOptionVisibility == true)
            {
                if (gV.twitterUserMoreOptionText.Equals("more") || gV.twitterUserMoreOptionText.Equals("More"))
                {
                    int pageNo = 0;

                    pageNo = (Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Count / 50) + 1;

                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].lastSearch;
                    searchTextBoxText1 = pageNo.ToString();

                    Countries[tabDynamic.SelectedIndex].twitterUserListCollections[Countries[tabDynamic.SelectedIndex].
                        twitterUserListCollections.Count - 1].twitterUserMoreOptionText = "loading...";

                    Thread th = new Thread(MoreTwitterUsersForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }
            
            String ddd = gV.GridViewColumnName_LabelContent;

            // if same user is clicked then no need to do anything...
            if (gV.GridViewColumnName_ID.Equals(Countries[tabDynamic.SelectedIndex].twitterProfileUserId))
                return;

            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListCollections.Clear();
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListCollections.Clear();
            Countries[tabDynamic.SelectedIndex].twitterUserTweetListCollections.Clear();

            // add a tabItem with + in header 
            //aTabItem plusOne = new aTabItem() { Header = gV.GridViewColumnName_LabelContent };
            Countries[tabDynamic.SelectedIndex].mySearch = "";
            Countries[tabDynamic.SelectedIndex].type = "twitterUserProfile";
            Countries[tabDynamic.SelectedIndex].fbUserListCollections.Clear();
            //Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Clear();
            Countries[tabDynamic.SelectedIndex].tab_number = tabDynamic.Items.Count.ToString();
            Countries[tabDynamic.SelectedIndex].searchUserDivVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserDivVisbility = true;
            Countries[tabDynamic.SelectedIndex].twitterTweetListDivVisbility = false;
            Countries[tabDynamic.SelectedIndex].headerCloseIconVisibility = true;

            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetCountEllipseBg = "#aaaaaa";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingCountEllipseBg = "#e1e1e1";
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersCountEllipseBg = "#e1e1e1";

            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowersListVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileFollowingListVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserProfileTweetListVisbility = true;

            for(int h=0;h<Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Count;h++)
            {
                if (Countries[tabDynamic.SelectedIndex].twitterUserListCollections[h].GridViewColumn_BgColor.Equals("#dddddd"))
                    Countries[tabDynamic.SelectedIndex].twitterUserListCollections[h].GridViewColumn_BgColor = "#ffffff";

                Countries[tabDynamic.SelectedIndex].twitterUserListCollections[h].GridViewColumnName_SelectedUser = gV.GridViewColumnName_myIndex;
            }//for loop...

            Countries[tabDynamic.SelectedIndex].twitterUserListCollections[gV.GridViewColumnName_myIndex].GridViewColumn_BgColor = "#dddddd";

            String proImgUrl = gV.GridViewColumnName_ImageSource;

            proImgUrl = proImgUrl.Replace("_normal.", ".");
            proImgUrl = proImgUrl.Replace("_bigger.", ".");

            String sName = gV.GridViewColumnName_LabelContentScreenName;
            if (sName.Contains("@") == false) sName = "@" + sName;

            Countries[tabDynamic.SelectedIndex].twitterProfileUserName = gV.GridViewColumnName_LabelContent;
            Countries[tabDynamic.SelectedIndex].twitterProfileUserAge = sName;//"@" + gV.GridViewColumnName_LabelContentScreenName;
            Countries[tabDynamic.SelectedIndex].twitterProfileUserCityCountry = gV.GridViewColumnLocation;
            Countries[tabDynamic.SelectedIndex].twitterProfileUserAboutMe = gV.GridViewColumnName_AboutMe;
            Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowingCount = gV.GridViewColumnName_FollowingCount;
            Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowerCount = gV.GridViewColumnName_FollowersCount;
            Countries[tabDynamic.SelectedIndex].twitterProfileUserTweetCount = gV.GridViewColumnName_tweetsCount;
            Countries[tabDynamic.SelectedIndex].twitterProfileUserId = gV.GridViewColumnName_ID;
            Countries[tabDynamic.SelectedIndex].twitterProfileUserDp = proImgUrl;//gV.GridViewColumnName_ImageSource;

            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = true;

            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...
          
            searchTextBoxText = gV.GridViewColumnName_ID;
            
            Thread twitterUserTweetsInParallelThread = new Thread(getTwitterUserTweetsInParallel);
                twitterUserTweetsInParallelThread.Start();
            
        }//end of func...

        private void bookmarkUserInExpanderList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;

            Object dddd = e.OriginalSource;

            TextBlock tB123 = null; Run run123 = null;

            if (e.OriginalSource as TextBlock != null)
                tB123 = e.OriginalSource as TextBlock;
            else if (e.OriginalSource as Run != null)
                run123 = e.OriginalSource as Run;

            Boolean removeUserClicked = false;

            if (run123 != null) 
            {
                if (run123.Text != null && run123.Text.Contains("Remove") == true) removeUserClicked = true;
            }//if run123!=null...
            else if (tB123 != null)
            {
                if (tB123.Text != null && tB123.Text.Contains("Remove") == true) removeUserClicked = true;
            }//if tB123!=null...

            bookmarkListItems gV = lVItem.Content as bookmarkListItems;

            String ddd = gV.GridViewColumnName_LabelContent;

            parentTab.SelectedIndex = 0;

            if (removeUserClicked == false)
            {
                // add a tabItem with + in header...
                aTabItem plusOne = new aTabItem() { Header = gV.GridViewColumnName_LabelContent };
                plusOne.mySearch = "";
                plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/twitter.png";
                plusOne.lastSearch = Countries[tabDynamic.SelectedIndex].lastSearch;
                plusOne.type = "twitterUserProfile";
                plusOne.fbUserListCollections.Clear();
                plusOne.twitterUserListCollections.Clear();
                plusOne.tab_number = tabDynamic.Items.Count.ToString();
                plusOne.searchUserDivVisbility = false;
                plusOne.twitterUserDivVisbility = true;
                plusOne.twitterTweetListDivVisbility = false;
                plusOne.headerCloseIconVisibility = true;

                plusOne.twitterUserProfileTweetCountEllipseBg = "#aaaaaa";
                plusOne.twitterUserProfileFollowingCountEllipseBg = "#e1e1e1";
                plusOne.twitterUserProfileFollowersCountEllipseBg = "#e1e1e1";

                plusOne.twitterUserProfileFollowersListVisbility = false;
                plusOne.twitterUserProfileFollowingListVisbility = false;
                plusOne.twitterUserProfileTweetListVisbility = true;

                String proImgUrl = gV.GridViewColumnName_ImageSource;

                proImgUrl = proImgUrl.Replace("_normal.", ".");
                proImgUrl = proImgUrl.Replace("_bigger.", ".");

                String sName = gV.GridViewColumnName_Age;
                if (sName == null) sName = "";
                if (sName.Contains("@") == false) sName = "@" + sName;

                String location = gV.GridViewColumnLocation;
                String aboutMe = gV.GridViewColumnName_AboutMe;
                if (location != null && location.Length == 0) { location = aboutMe; aboutMe = ""; }

                plusOne.twitterProfileUserName = gV.GridViewColumnName_LabelContent;
                plusOne.twitterProfileUserAge = sName;//"@"+gV.GridViewColumnName_LabelContentScreenName;
                plusOne.twitterProfileUserCityCountry = location;//gV.GridViewColumnLocation;
                plusOne.twitterProfileUserAboutMe = aboutMe;//gV.GridViewColumnName_AboutMe;
                plusOne.twitterProfileUserFollowingCount = gV.GridViewColumnName_FollowingCount;
                plusOne.twitterProfileUserFollowerCount = gV.GridViewColumnName_FollowersCount;
                plusOne.twitterProfileUserTweetCount = gV.GridViewColumnName_tweetsCount;
                plusOne.twitterProfileUserId = gV.GridViewColumnName_ID;
                plusOne.twitterProfileUserDp = proImgUrl;//gV.GridViewColumnName_ImageSource;
                plusOne.twitterUserProfileUrl = gV.twitterUserProfileUrl;

                plusOne.twitterUserTweetListLoadingVisibility = true;

                ///////////////////////////////////////////////////////////
                // now filling that user list in expander...
                int len = Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Count;

                for (int i = 0; i < len; i++)
                {
                    plusOne.twitterUserListCollections.Add(Countries[tabDynamic.SelectedIndex].twitterUserListCollections[i]);
                    if (gV.GridViewColumnName_ID.Equals(plusOne.twitterUserListCollections[i].GridViewColumnName_ID))
                    {
                        plusOne.twitterUserListCollections[i].GridViewColumn_BgColor = "#dddddd";
                        plusOne.twitterUserListCollections[i].GridViewColumnName_SelectedUser = i;
                    }
                    else
                        plusOne.twitterUserListCollections[i].GridViewColumn_BgColor = "#ffffff";

                    plusOne.twitterUserListCollections[i].GridViewColumnName_myIndex = i;

                }//for loop...
                ///////////////////////////////////////////////////////////

                searchTextBoxText = gV.GridViewColumnName_ID;

                //String bbddd=plusOne.twitterUserListCollections[0].GridViewColumn_BgColor;

                Thread twitterUserTweetsInParallelThread = new Thread(getTwitterUserTweetsInParallel);
                twitterUserTweetsInParallelThread.Start();

                //ts.TW_UserTweetsByID(gV.GridViewColumnName_ID);

                Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

                tabDynamic.SelectedItem = plusOne;

            }//if removeBookmarkClicked==false...
            else 
            {
                int resutli = removeBookmark(gV.GridViewColumnName_ID, gV.GridViewColumnSocialMedia);

                if(resutli>=0)
                {
                    bookmarkListCollections.Remove(gV);
                }//if(resutli>=0)...

            }//else removeBookmarkClicked==true...

        }//end of func...

        private void fbPageInExpanderList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;
            //ListView lV = lVItem.TemplatedParent as ListView;
            //Button button = sender as Button;
            //int index = _myListBoxName.Items.IndexOf(button.DataContext);

            fbPageListItems gV = lVItem.Content as fbPageListItems;

            if (gV.fbPageMoreOptionVisibility == true)
            {
                if (gV.fbPageMoreOptionText.Equals("more") || gV.fbPageMoreOptionText.Equals("More"))
                {
                    int offset = 0;

                    offset = Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count;

                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].lastSearch;
                    searchTextBoxText1 = offset.ToString();

                    Countries[tabDynamic.SelectedIndex].fbPageListCollections[Countries[tabDynamic.SelectedIndex].
                        fbPageListCollections.Count - 1].fbPageMoreOptionText = "loading...";

                    Thread th = new Thread(MoreFbPagesForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }

            String ddd = gV.fbPage_LabelContent;

            // if same user is clicked then no need to do anything...
            if (gV.fbPage_ID.Equals(Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID))
                return;

            // add a tabItem with + in header 
            //aTabItem plusOne = new aTabItem() { Header = gV.GridViewColumnName_LabelContent };
            Countries[tabDynamic.SelectedIndex].mySearch = "";
            Countries[tabDynamic.SelectedIndex].type = "fbPageProfile";
            Countries[tabDynamic.SelectedIndex].fbUserListCollections.Clear();
            //Countries[tabDynamic.SelectedIndex].fbUserProfileLink = "https://facebook.com/" + searchTextBoxText;
            Countries[tabDynamic.SelectedIndex].fbUserProfileLinkVisibility = false;
            
            //Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Clear();
            Countries[tabDynamic.SelectedIndex].tab_number = tabDynamic.Items.Count.ToString();
            Countries[tabDynamic.SelectedIndex].searchUserDivVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserDivVisbility = true;
            Countries[tabDynamic.SelectedIndex].twitterTweetListDivVisbility = false;
            Countries[tabDynamic.SelectedIndex].headerCloseIconVisibility = true;

            for (int h = 0; h < Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count; h++)
            {
                if (Countries[tabDynamic.SelectedIndex].fbPageListCollections[h].fbPage_BgColor.Equals("#dddddd"))
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections[h].fbPage_BgColor = "#ffffff";

                Countries[tabDynamic.SelectedIndex].fbPageListCollections[h].fbPage_SelectedUser = gV.fbPage_myIndex;
            }//for loop...

            Countries[tabDynamic.SelectedIndex].fbPageListCollections[gV.fbPage_myIndex].fbPage_BgColor = "#dddddd";

            
            Countries[tabDynamic.SelectedIndex].fbPageTabInfo_LabelContent = gV.fbPage_LabelContent;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserAge = "@" + gV.GridViewColumnName_LabelContentScreenName;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserCityCountry = gV.GridViewColumnLocation;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserAboutMe = gV.GridViewColumnName_AboutMe;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowingCount = gV.GridViewColumnName_FollowingCount;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowerCount = gV.GridViewColumnName_FollowersCount;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserTweetCount = gV.GridViewColumnName_tweetsCount;
            Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID = gV.fbPage_ID;
            Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ImageSource = gV.fbPage_ImageSource;

            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = true;
            Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = false;

            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...

            searchTextBoxText = gV.fbPage_ID;

            Countries[tabDynamic.SelectedIndex].fbPageTabListCollections.Clear();

            Thread twitterUserTweetsInParallelThread = new Thread(getfBPageInfoInParallel);
            twitterUserTweetsInParallelThread.Start();

        }//end of func...

        private void googleUserInExpanderList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;
            
            googleUserListItems gV = lVItem.Content as googleUserListItems;
            //String ddd = gV.fbPage_LabelContent;

            if (gV.googleUserMoreOptionVisibility == true)
            {
                if (gV.googleUserMoreOptionText.Equals("more") ||gV.googleUserMoreOptionText.Equals("More"))
                {
                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].Header;
                    searchTextBoxText1 = Countries[tabDynamic.SelectedIndex].googleUserListCollections[Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count - 1].Id;

                    Countries[tabDynamic.SelectedIndex].googleUserListCollections[Countries[tabDynamic.SelectedIndex].
                        googleUserListCollections.Count - 1].googleUserMoreOptionText = "loading...";

                    Thread th = new Thread(MoreGoogleUsersForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }

            // if same user is clicked then no need to do anything...
            if (gV.Id.Equals(Countries[tabDynamic.SelectedIndex].GoogleUserId))
                return;

            // add a tabItem with + in header...
            //aTabItem plusOne = new aTabItem() { Header = gV.GridViewColumnName_LabelContent };
            Countries[tabDynamic.SelectedIndex].mySearch = "";
            Countries[tabDynamic.SelectedIndex].type = "googleUserProfile";
            Countries[tabDynamic.SelectedIndex].fbUserListCollections.Clear();
            //Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Clear();
            Countries[tabDynamic.SelectedIndex].tab_number = tabDynamic.Items.Count.ToString();
            Countries[tabDynamic.SelectedIndex].searchUserDivVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserDivVisbility = false;
            Countries[tabDynamic.SelectedIndex].googleUserDivVisbility = true;
            Countries[tabDynamic.SelectedIndex].twitterTweetListDivVisbility = false;
            Countries[tabDynamic.SelectedIndex].headerCloseIconVisibility = true;

            for (int h = 0; h < Countries[tabDynamic.SelectedIndex].googleUserListCollections.Count; h++)
            {
                if (Countries[tabDynamic.SelectedIndex].googleUserListCollections[h].GridViewColumn_BgColor.Equals("#dddddd"))
                    Countries[tabDynamic.SelectedIndex].googleUserListCollections[h].GridViewColumn_BgColor = "#ffffff";

                Countries[tabDynamic.SelectedIndex].googleUserListCollections[h].GridViewColumnName_SelectedUser = gV.GridViewColumnName_myIndex;
            }//for loop...

            Countries[tabDynamic.SelectedIndex].googleUserListCollections[gV.GridViewColumnName_myIndex].GridViewColumn_BgColor = "#dddddd";
            
            Countries[tabDynamic.SelectedIndex].GoogleUserDisplayName = gV.DisplayName;

            googleUserListItems P= gV;

            int a = 0, b = 0;
            bool c = false, d = false;
            String aboutMe = "About Me: no info", Birthday = "Birthday: no info", BraggingRights = "BraggingRights: no info", CurrentLocation = "CurrentLocation: no info",
                Domain = "Domain: no info", ETag = "ETag: no info", Gender = "Gender: no info",
                Kind = "Kind: no info", language = "Language: no info", nickName = "NickName: no info", objectType = "ObjectType: no info", occupation = "Occupation: no info",
                relationShip = "Relationships: no info", skills = "Skills: no info", tagline = "Tagline: no info", url = "Url: no info";

            if (P.CircledByCount != null) { a = Convert.ToInt32(P.CircledByCount); }
            if (P.PlusOneCount != null) { b = Convert.ToInt32(P.PlusOneCount); }
            if (P.IsPlusUser != null) { c = Convert.ToBoolean(P.IsPlusUser); }
            if (P.Verified != null) { d = Convert.ToBoolean(P.Verified); }

            if (P.aboutMe != null) aboutMe = "About Me: " + P.aboutMe;
            if (P.Birthday != null) aboutMe = "Birthday: " + P.Birthday;
            if (P.BraggingRights != null) BraggingRights = "Bragging Rights: " + P.BraggingRights;
            if (P.CurrentLocation != null) CurrentLocation = "Current Location: " + P.CurrentLocation;

            if (P.Domain != null) Domain = "Domain: " + P.Domain;
            if (P.ETag != null) ETag = "ETag: " + P.ETag;
            if (P.Gender != null) Gender = "Gender: " + P.Gender;

            if (P.Kind != null) Kind = "Kind: " + P.Kind;
            if (P.Language != null) language = "language: " + P.Language;
            if (P.NickName != null) nickName = "NickName: " + P.NickName;

            if (P.ObjectType != null) objectType = "Object Type: " + P.ObjectType;
            if (P.Occupation != null) occupation = "Ocupation: " + P.Occupation;
            if (P.RelationshipStatus != null) relationShip = "Relationship Status: " + P.RelationshipStatus;
            if (P.Skills != null) skills = "Skills: " + P.Skills;

            if (P.Tagline != null) tagline = "TagLine: " + P.Tagline;
            if (P.Url != null) url = "Url: " + P.Url;

                Countries[tabDynamic.SelectedIndex].GoogleUserId = P.Id;
                Countries[tabDynamic.SelectedIndex].GoogleUserDisplayName= P.DisplayName;
                Countries[tabDynamic.SelectedIndex].GoogleUserImageUrl= P.ImageUrl;
                Countries[tabDynamic.SelectedIndex].aboutMe = aboutMe;
                Countries[tabDynamic.SelectedIndex].Birthday = Birthday;
                Countries[tabDynamic.SelectedIndex].BraggingRights = BraggingRights;
                Countries[tabDynamic.SelectedIndex].CircledByCount = "CircledByCount: " + a.ToString();
                Countries[tabDynamic.SelectedIndex].CurrentLocation = CurrentLocation;
                Countries[tabDynamic.SelectedIndex].Domain = Domain;
                Countries[tabDynamic.SelectedIndex].ETag = ETag;
                Countries[tabDynamic.SelectedIndex].Gender = Gender;
                Countries[tabDynamic.SelectedIndex].IsPlusUser = "IsPlusUser: " + c.ToString();
                Countries[tabDynamic.SelectedIndex].Kind = Kind;
                Countries[tabDynamic.SelectedIndex].Language = language;
                Countries[tabDynamic.SelectedIndex].NickName = nickName;
                Countries[tabDynamic.SelectedIndex].ObjectType = objectType;
                Countries[tabDynamic.SelectedIndex].Occupation = occupation;
                Countries[tabDynamic.SelectedIndex].PlusOneCount = "PlusOneCount: " + b.ToString();
                Countries[tabDynamic.SelectedIndex].RelationshipStatus = relationShip;
                Countries[tabDynamic.SelectedIndex].Skills = "Skills: " + skills;
                Countries[tabDynamic.SelectedIndex].Tagline = tagline;
                Countries[tabDynamic.SelectedIndex].Url = url;
                Countries[tabDynamic.SelectedIndex].Verified = "Verified: " + d.ToString();
            
            /**
            Countries[tabDynamic.SelectedIndex].Kind = gV.Kind;
            Countries[tabDynamic.SelectedIndex].Language = gV.Language;
            Countries[tabDynamic.SelectedIndex].NickName = gV.NickName;
            Countries[tabDynamic.SelectedIndex].ObjectType = gV.ObjectType;
            Countries[tabDynamic.SelectedIndex].Occupation = gV.Occupation;
            Countries[tabDynamic.SelectedIndex].PlusOneCount = "PlusOneCount: "+gV.PlusOneCount.ToString();
            Countries[tabDynamic.SelectedIndex].RelationshipStatus = gV.RelationshipStatus;
            Countries[tabDynamic.SelectedIndex].Skills = gV.Skills;
            Countries[tabDynamic.SelectedIndex].Tagline = gV.aboutMe;
            Countries[tabDynamic.SelectedIndex].Url = gV.aboutMe;
            Countries[tabDynamic.SelectedIndex].Verified = "Verified: "+gV.Verified.ToString();

            Countries[tabDynamic.SelectedIndex].aboutMe = gV.aboutMe;
            Countries[tabDynamic.SelectedIndex].Birthday = gV.Birthday;
            Countries[tabDynamic.SelectedIndex].BraggingRights = gV.BraggingRights;
            Countries[tabDynamic.SelectedIndex].CircledByCount = "Circled By Count: "+gV.CircledByCount.ToString() ;
            Countries[tabDynamic.SelectedIndex].CurrentLocation = gV.CurrentLocation;
            Countries[tabDynamic.SelectedIndex].Domain = gV.Domain;
            Countries[tabDynamic.SelectedIndex].ETag = gV.ETag;
            Countries[tabDynamic.SelectedIndex].Gender = gV.Gender;
            Countries[tabDynamic.SelectedIndex].IsPlusUser = "IsPlusUser: "+gV.IsPlusUser.ToString();
            Countries[tabDynamic.SelectedIndex].Kind = gV.Kind;
            */
            Countries[tabDynamic.SelectedIndex].GoogleUserId = gV.Id;
            Countries[tabDynamic.SelectedIndex].GoogleUserImageUrl = gV.ImageUrl;

            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = true;
            Countries[tabDynamic.SelectedIndex].nothingToShowTextForSpecificUserOrPageListLoadingVisibility = false;
            //Countries[tabDynamic.SelectedIndex].fbUserProfileLink = Countries[tabDynamic.SelectedIndex].Url;
            Countries[tabDynamic.SelectedIndex].fbUserProfileLink = "";
            Countries[tabDynamic.SelectedIndex].fbUserProfileLinkVisibility = false;

            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...

            searchTextBoxText = gV.Id;

            Countries[tabDynamic.SelectedIndex].googleUserTabListCollections.Clear();

            Thread twitterUserTweetsInParallelThread = new Thread(getGoogleUserPostsInParallel);
            twitterUserTweetsInParallelThread.Start();

        }//end of func...

        private void fbGroupInExpanderList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;
            //ListView lV = lVItem.TemplatedParent as ListView;
            //Button button = sender as Button;
            //int index = _myListBoxName.Items.IndexOf(button.DataContext);

            fbPageListItems gV = lVItem.Content as fbPageListItems;
            String ddd = gV.fbPage_LabelContent;

            if (gV.fbPageMoreOptionVisibility == true)
            {
                if (gV.fbPageMoreOptionText.Equals("more") || gV.fbPageMoreOptionText.Equals("More"))
                {
                    int offset = 0;

                    offset = Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count;

                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].lastSearch;
                    searchTextBoxText1 = offset.ToString();

                    Countries[tabDynamic.SelectedIndex].fbPageListCollections[Countries[tabDynamic.SelectedIndex].
                        fbPageListCollections.Count - 1].fbPageMoreOptionText = "loading...";

                    Thread th = new Thread(MoreFbGroupsInExpanderForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }

            // if same user is clicked then no need to do anything...
            if (gV.fbPage_ID.Equals(Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID))
                return;

            // add a tabItem with + in header 
            //aTabItem plusOne = new aTabItem() { Header = gV.GridViewColumnName_LabelContent };
            Countries[tabDynamic.SelectedIndex].mySearch = "";
            Countries[tabDynamic.SelectedIndex].type = "fbPageProfile";
            Countries[tabDynamic.SelectedIndex].fbUserListCollections.Clear();
            //Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Clear();
            Countries[tabDynamic.SelectedIndex].tab_number = tabDynamic.Items.Count.ToString();
            Countries[tabDynamic.SelectedIndex].searchUserDivVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserDivVisbility = true;
            Countries[tabDynamic.SelectedIndex].twitterTweetListDivVisbility = false;
            Countries[tabDynamic.SelectedIndex].headerCloseIconVisibility = true;

            for (int h = 0; h < Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count; h++)
            {
                if (Countries[tabDynamic.SelectedIndex].fbPageListCollections[h].fbPage_BgColor.Equals("#dddddd"))
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections[h].fbPage_BgColor = "#ffffff";

                Countries[tabDynamic.SelectedIndex].fbPageListCollections[h].fbPage_SelectedUser = gV.fbPage_myIndex;
            }//for loop...

            Countries[tabDynamic.SelectedIndex].fbPageListCollections[gV.fbPage_myIndex].fbPage_BgColor = "#dddddd";


            Countries[tabDynamic.SelectedIndex].fbPageTabInfo_LabelContent = gV.fbPage_LabelContent;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserAge = "@" + gV.GridViewColumnName_LabelContentScreenName;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserCityCountry = gV.GridViewColumnLocation;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserAboutMe = gV.GridViewColumnName_AboutMe;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowingCount = gV.GridViewColumnName_FollowingCount;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowerCount = gV.GridViewColumnName_FollowersCount;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserTweetCount = gV.GridViewColumnName_tweetsCount;
            Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID = gV.fbPage_ID;
            Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ImageSource = gV.fbPage_ImageSource;

            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = true;
            Countries[tabDynamic.SelectedIndex].fbUserProfileLinkVisibility = false;

            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...

            searchTextBoxText = gV.fbPage_ID;

            Countries[tabDynamic.SelectedIndex].fbPageTabListCollections.Clear();

            Thread twitterUserTweetsInParallelThread = new Thread(getfBGroupInfoInParallel);
            twitterUserTweetsInParallelThread.Start();

        }//end of func...

        private void fbUserInExpanderList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem lVItem = sender as ListViewItem;
            //ListView lV = lVItem.TemplatedParent as ListView;
            //Button button = sender as Button;
            //int index = _myListBoxName.Items.IndexOf(button.DataContext);

            fbPageListItems gV = lVItem.Content as fbPageListItems;

            if (gV.fbPageMoreOptionVisibility == true)
            {
                if (gV.fbPageMoreOptionText.Equals("more") || gV.fbPageMoreOptionText.Equals("More"))
                {
                    int offset = 0;

                    offset = Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count;

                    searchTextBoxText = Countries[tabDynamic.SelectedIndex].mySearch;
                    searchTextBoxText1 = offset.ToString();

                    Countries[tabDynamic.SelectedIndex].fbPageListCollections[Countries[tabDynamic.SelectedIndex].
                        fbPageListCollections.Count - 1].fbPageMoreOptionText = "loading...";

                    Thread th = new Thread(MoreFbUsersInExpanderForSearch);
                    th.Start();
                }//if(gV.twitterUserMoreOptionText.Equals("more"))...
                //else

                return;
            }

            String ddd = gV.fbPage_LabelContent;

            // if same user is clicked then no need to do anything...
            if (gV.fbPage_ID.Equals(Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID))
                return;

            // add a tabItem with + in header 
            //aTabItem plusOne = new aTabItem() { Header = gV.GridViewColumnName_LabelContent };
            Countries[tabDynamic.SelectedIndex].mySearch = "";
            Countries[tabDynamic.SelectedIndex].type = "fbUserProfile";
            Countries[tabDynamic.SelectedIndex].fbUserListCollections.Clear();
            //Countries[tabDynamic.SelectedIndex].twitterUserListCollections.Clear();
            Countries[tabDynamic.SelectedIndex].tab_number = tabDynamic.Items.Count.ToString();
            Countries[tabDynamic.SelectedIndex].fbUserProfileLink = "https://facebook.com/" + gV.fbPage_ID;
            Countries[tabDynamic.SelectedIndex].fbUserProfileLinkVisibility = true;
            Countries[tabDynamic.SelectedIndex].searchUserDivVisbility = false;
            Countries[tabDynamic.SelectedIndex].twitterUserDivVisbility = true;
            Countries[tabDynamic.SelectedIndex].twitterTweetListDivVisbility = false;
            Countries[tabDynamic.SelectedIndex].headerCloseIconVisibility = true;

            for (int h = 0; h < Countries[tabDynamic.SelectedIndex].fbPageListCollections.Count; h++)
            {
                if (Countries[tabDynamic.SelectedIndex].fbPageListCollections[h].fbPage_BgColor.Equals("#dddddd"))
                    Countries[tabDynamic.SelectedIndex].fbPageListCollections[h].fbPage_BgColor = "#ffffff";

                Countries[tabDynamic.SelectedIndex].fbPageListCollections[h].fbPage_SelectedUser = gV.fbPage_myIndex;
            }//for loop...

            Countries[tabDynamic.SelectedIndex].fbPageListCollections[gV.fbPage_myIndex].fbPage_BgColor = "#dddddd";


            Countries[tabDynamic.SelectedIndex].fbPageTabInfo_LabelContent = gV.fbPage_LabelContent;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserAge = "@" + gV.GridViewColumnName_LabelContentScreenName;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserCityCountry = gV.GridViewColumnLocation;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserAboutMe = gV.GridViewColumnName_AboutMe;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowingCount = gV.GridViewColumnName_FollowingCount;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserFollowerCount = gV.GridViewColumnName_FollowersCount;
            //Countries[tabDynamic.SelectedIndex].twitterProfileUserTweetCount = gV.GridViewColumnName_tweetsCount;
            Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ID = gV.fbPage_ID;
            Countries[tabDynamic.SelectedIndex].fbPageTabInfo_ImageSource = gV.fbPage_ImageSource;

            Countries[tabDynamic.SelectedIndex].twitterUserTweetListLoadingVisibility = true;

            ///////////////////////////////////////////////////////////
            // now filling that user list in expander...

            searchTextBoxText = gV.fbPage_ID;

            Countries[tabDynamic.SelectedIndex].fbPageTabListCollections.Clear();

            Thread twitterUserTweetsInParallelThread = new Thread(getfBUserInfoInParallel);
            twitterUserTweetsInParallelThread.Start();

        }//end of func...

        private void aaaaaa_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
            Grid tab = sender as Grid;
            DockPanel twitterDiv = tab.FindName("TwitterProfileTab") as DockPanel;
            DockPanel searchDiv = tab.FindName("searchTab") as DockPanel;

            String tab_number = tab.Tag.ToString();

            if (Countries[Convert.ToInt32(tab_number)].type.Equals("Result"))
            {
                searchDiv.Visibility = Visibility.Visible;
                twitterDiv.Visibility = Visibility.Hidden;
            }
            else if (Countries[Convert.ToInt32(tab_number)].type.Equals("twitterUserProfile"))
            {
                twitterDiv.Visibility = Visibility.Visible;
                searchDiv.Visibility = Visibility.Hidden;
            }
            else
            {
                searchDiv.Visibility = Visibility.Visible;
                twitterDiv.Visibility = Visibility.Hidden;
            }//else...
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("sdas");
        }

        /// <summary>
        /// /////////////////////////////////
        /// // A single tab header clicked...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aTabClicked(object sender, RoutedEventArgs e)
        {
            TabItem aasa = sender as TabItem;
            aTabItem thistabItem=aasa.Header as aTabItem;

            if (thistabItem.type.Equals("add"))
            {
                int nextIndex = (Convert.ToInt32(MainWindow.tabMaxIndex))+1;
                // add a tabItem with + in header 
                aTabItem plusOne = new aTabItem() { Header = "Result" };
                plusOne.mySearch = "";
                plusOne.HeaderImgSrc = "/WpfApplication2;component/Resources/search.png";
                plusOne.tab_number = nextIndex.ToString();//**(tabDynamic.Items.Count - 1).ToString();
                plusOne.type = "Result";
                plusOne.fbUserListCollections.Clear();
                plusOne.twitterUserListCollections.Clear();
                //**plusOne.tab_number = tabDynamic.Items.Count.ToString();
                plusOne.searchUserDivVisbility = true;
                plusOne.twitterUserDivVisbility = false;
                plusOne.headerCloseIconVisibility = true;

                MainWindow.tabMaxIndex = nextIndex.ToString();

                Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

                    tabDynamic.SelectedItem=plusOne;
                
                e.Handled = true;
            }//if (thistabItem.type.Equals("add"))...
            else 
            {
            
            }//else...

        }

        private void osintTabClicked(object sender, RoutedEventArgs e)
        {
            TabItem aasa = sender as TabItem;
            osintItem thistabItem = aasa.Header as osintItem;

            if (thistabItem.type.Equals("add"))
            {
                searchTextBoxText = "";

                // add a tabItem with + in header 
                osintItem plusOne = new osintItem() { Header = "Result" };
                plusOne.mySearch1 = "";
                plusOne.tab_number = (tabDynamic1.Items.Count - 1).ToString();
                plusOne.type = "Result";
                plusOne.tab_number = tabDynamic1.Items.Count.ToString();
                plusOne.headerCloseIconVisibility = true;
                plusOne.sADivVisbility = true;

                plusOne.sourceCodeSenti = @"http://localhost:80/TwitterWizLocal/";//**@"http://www.csc.ncsu.edu/faculty/healey/tweet_viz/tweet_app/";

                osintBased.Insert(tabDynamic1.Items.Count - 1, plusOne);

                tabDynamic1.SelectedItem = plusOne;

                e.Handled = true;
            }//if (thistabItem.type.Equals("add"))...
            else
            {

            }//else...

        }//func...

        private void carebasedTabClicked(object sender, RoutedEventArgs e)
        {
            TabItem aasa = sender as TabItem;
            osintItem thistabItem = aasa.Header as osintItem;

            if (thistabItem.type.Equals("add"))
            {
                searchTextBoxText = "";

                // add a tabItem with + in header 
                osintItem plusOne = new osintItem() { Header = "Result" };
                plusOne.mySearch1 = "";
                plusOne.tab_number = (tabDynamic1.Items.Count - 1).ToString();
                plusOne.type = "Result";
                plusOne.tab_number = tabDynamic1.Items.Count.ToString();
                plusOne.headerCloseIconVisibility = true;
                plusOne.sADivVisbility = true;

                //string curDir = Directory.GetCurrentDirectory();
                //string iii = curDir + @"\work\index.html";

                plusOne.sourceCodeSenti = @"http://localhost:80/analysis/";//iii;//@".\work\index.html";

                careSentimentBased.Insert(tabDynamic2.Items.Count - 1, plusOne);

                tabDynamic2.SelectedItem = plusOne;

                e.Handled = true;
            }//if (thistabItem.type.Equals("add"))...
            else
            {

            }//else...

        }//func...

        private void showList_text_Click(object sender, RoutedEventArgs e)
        {
            TextBlock tB = sender as TextBlock;

            String which_one = tB.Tag.ToString();

            if (which_one == "fbUser") 
            {
                Countries[tabDynamic.SelectedIndex].fbUserListVisibility = true;
                Countries[tabDynamic.SelectedIndex].fbPageListVisibility = false;
                Countries[tabDynamic.SelectedIndex].fbGroupListVisibility = false;
                Countries[tabDynamic.SelectedIndex].Shapes[7].ImageSource = "/CAREsma;component/Resources/fb_color.png";
                Countries[tabDynamic.SelectedIndex].Shapes[8].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[9].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
            }//if (which_one == "fbUserText") ...
            else if (which_one == "fbGroup")
            {
                Countries[tabDynamic.SelectedIndex].fbUserListVisibility = false;
                Countries[tabDynamic.SelectedIndex].fbPageListVisibility = false;
                Countries[tabDynamic.SelectedIndex].fbGroupListVisibility = true;
                Countries[tabDynamic.SelectedIndex].Shapes[7].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[8].ImageSource = "/CAREsma;component/Resources/fb_color.png";
                Countries[tabDynamic.SelectedIndex].Shapes[9].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
            }//if (which_one == "fbGroupText") ...
            else if (which_one == "fbPage")
            {
                Countries[tabDynamic.SelectedIndex].fbUserListVisibility = false;
                Countries[tabDynamic.SelectedIndex].fbPageListVisibility = true;
                Countries[tabDynamic.SelectedIndex].fbGroupListVisibility = false;
                Countries[tabDynamic.SelectedIndex].Shapes[7].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[8].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[9].ImageSource = "/CAREsma;component/Resources/fb_color.png";
            }//if (which_one == "fbPageText") ...
            else if (which_one == "twUser")
            {
                Countries[tabDynamic.SelectedIndex].twUserListVisibility = true;
                Countries[tabDynamic.SelectedIndex].twTweetListVisibility = false;
                Countries[tabDynamic.SelectedIndex].Shapes[16].ImageSource = "/CAREsma;component/Resources/twitter_color.png";
                Countries[tabDynamic.SelectedIndex].Shapes[17].ImageSource = "/CAREsma;component/Resources/twitter_color_fade.png";
            }//if (which_one == "fbGroupText") ...
            else if (which_one == "twTweet")
            {
                Countries[tabDynamic.SelectedIndex].twUserListVisibility = false;
                Countries[tabDynamic.SelectedIndex].twTweetListVisibility = true;
                Countries[tabDynamic.SelectedIndex].Shapes[16].ImageSource = "/CAREsma;component/Resources/twitter_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[17].ImageSource = "/CAREsma;component/Resources/twitter_color.png";
            }//if (which_one == "fbPageText") ...
            else if (which_one == "gUser")
            {
                Countries[tabDynamic.SelectedIndex].gUserListVisibility = true;
                Countries[tabDynamic.SelectedIndex].gActivityListVisibility = false;
                Countries[tabDynamic.SelectedIndex].Shapes[23].ImageSource = "/CAREsma;component/Resources/google_color.png";
                Countries[tabDynamic.SelectedIndex].Shapes[24].ImageSource = "/CAREsma;component/Resources/google_color_fade.png";
            }//if (which_one == "fbGroupText") ...
            else if (which_one == "gActivity")
            {
                Countries[tabDynamic.SelectedIndex].gUserListVisibility = false;
                Countries[tabDynamic.SelectedIndex].gActivityListVisibility = true;
                Countries[tabDynamic.SelectedIndex].Shapes[23].ImageSource = "/CAREsma;component/Resources/google_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[24].ImageSource = "/CAREsma;component/Resources/google_color.png";
            }//if (which_one == "fbPageText") ...
            else
            {
                Countries[tabDynamic.SelectedIndex].fbUserListVisibility = true;
                Countries[tabDynamic.SelectedIndex].fbPageListVisibility = false;
                Countries[tabDynamic.SelectedIndex].fbGroupListVisibility = false;
            }//if (which_one == "fbPageText") ...

            int i = 0;
            int j = 0;

        }//func...

        private void showList_circle_Click(object sender, RoutedEventArgs e)
        {
            Ellipse el = sender as Ellipse;

            String which_one = el.Tag.ToString();

            if (which_one == "fbUser")
            {
                Countries[tabDynamic.SelectedIndex].fbUserListVisibility = true;
                Countries[tabDynamic.SelectedIndex].fbPageListVisibility = false;
                Countries[tabDynamic.SelectedIndex].fbGroupListVisibility = false;
                Countries[tabDynamic.SelectedIndex].Shapes[7].ImageSource = "/CAREsma;component/Resources/fb_color.png";
                Countries[tabDynamic.SelectedIndex].Shapes[8].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[9].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
            }//if (which_one == "fbUserText") ...
            else if (which_one == "fbGroup")
            {
                Countries[tabDynamic.SelectedIndex].fbUserListVisibility = false;
                Countries[tabDynamic.SelectedIndex].fbPageListVisibility = false;
                Countries[tabDynamic.SelectedIndex].fbGroupListVisibility = true;
                Countries[tabDynamic.SelectedIndex].Shapes[7].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[8].ImageSource = "/CAREsma;component/Resources/fb_color.png";
                Countries[tabDynamic.SelectedIndex].Shapes[9].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
            }//if (which_one == "fbGroupText") ...
            else if (which_one == "fbPage")
            {
                Countries[tabDynamic.SelectedIndex].fbUserListVisibility = false;
                Countries[tabDynamic.SelectedIndex].fbPageListVisibility = true;
                Countries[tabDynamic.SelectedIndex].fbGroupListVisibility = false;
                Countries[tabDynamic.SelectedIndex].Shapes[7].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[8].ImageSource = "/CAREsma;component/Resources/fb_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[9].ImageSource = "/CAREsma;component/Resources/fb_color.png";
            }//if (which_one == "fbPageText") ...
            else if (which_one == "twUser")
            {
                Countries[tabDynamic.SelectedIndex].twUserListVisibility = true;
                Countries[tabDynamic.SelectedIndex].twTweetListVisibility = false;
                Countries[tabDynamic.SelectedIndex].Shapes[16].ImageSource = "/CAREsma;component/Resources/twitter_color.png";
                Countries[tabDynamic.SelectedIndex].Shapes[17].ImageSource = "/CAREsma;component/Resources/twitter_color_fade.png";
            }//if (which_one == "fbGroupText") ...
            else if (which_one == "twTweet")
            {
                Countries[tabDynamic.SelectedIndex].twUserListVisibility = false;
                Countries[tabDynamic.SelectedIndex].twTweetListVisibility = true;
                Countries[tabDynamic.SelectedIndex].Shapes[16].ImageSource = "/CAREsma;component/Resources/twitter_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[17].ImageSource = "/CAREsma;component/Resources/twitter_color.png";
            }//if (which_one == "fbPageText") ...
            else if (which_one == "gUser")
            {
                Countries[tabDynamic.SelectedIndex].gUserListVisibility = true;
                Countries[tabDynamic.SelectedIndex].gActivityListVisibility = false;
                Countries[tabDynamic.SelectedIndex].Shapes[23].ImageSource = "/CAREsma;component/Resources/google_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[24].ImageSource = "/CAREsma;component/Resources/google_color.png";
            }//if (which_one == "fbGroupText") ...
            else if (which_one == "gActivity")
            {
                Countries[tabDynamic.SelectedIndex].gUserListVisibility = false;
                Countries[tabDynamic.SelectedIndex].gActivityListVisibility = true;
                Countries[tabDynamic.SelectedIndex].Shapes[23].ImageSource = "/CAREsma;component/Resources/google_color_fade.png";
                Countries[tabDynamic.SelectedIndex].Shapes[24].ImageSource = "/CAREsma;component/Resources/google_color.png";
            }//if (which_one == "fbPageText") ...
            else
            {
                Countries[tabDynamic.SelectedIndex].fbUserListVisibility = true;
                Countries[tabDynamic.SelectedIndex].fbPageListVisibility = false;
                Countries[tabDynamic.SelectedIndex].fbGroupListVisibility = false;
            }//if (which_one == "fbPageText") ...

            int i = 0;
            int j = 0;

        }//func...

        private void searchOsint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (osintBased[tabDynamic1.SelectedIndex].mySearch1 == null || osintBased[tabDynamic1.SelectedIndex].mySearch1.Trim().Length == 0)
                {
                    MessageBox.Show("Nothing to search for!");
                    return;
                }//if (searchTextBoxText == null || searchTextBoxText.Trim().Length == 0) ...

                Button but = sender as Button;
                StackPanel sp = but.Parent as StackPanel;
                Border bor = sp.Parent as Border;
                DockPanel dockP = bor.Parent as DockPanel;

                dPGlobal = dockP;

                try
                {
                    searchTextBoxText = osintBased[tabDynamic1.SelectedIndex].mySearch1;

                    connection = new MySqlConnection(MyConnectionString);
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("DELETE FROM twitter1", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM twitter2", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM twitter3", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM twitter4", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();

                    WebBrowser wB = null;

                    if (dPGlobal != null)
                        wB = dPGlobal.FindName("browse") as WebBrowser;

                    if (wB != null && wB.Document != null)
                    {
                        searchTextBoxText = osintBased[tabDynamic1.SelectedIndex].mySearch1;

                        osintBased[tabDynamic1.SelectedIndex].Header = searchTextBoxText;
                        osintBased[tabDynamic1.SelectedIndex].lastSearch1 = searchTextBoxText;

                        HTMLDocument doc = (HTMLDocument)wB.Document;

                        if (doc != null)
                        {
                            IHTMLElement theElement = doc.getElementById("query-inp");

                            if (theElement != null)
                            {
                                theElement.innerText = searchTextBoxText;

                                theElement = doc.getElementById("query-btn123");
                                theElement.click();
                            }//theElement!=null...

                        }//if doc!=null...
                    }//if (wB != null && wB.Document != null)...

                    Thread twitter1Thread = new Thread(getTwitter1Data);

                    twitter1Thread.Start();

                    /////////////////////////////////////////////////////////////////////////////////////////////////////

                }//try...
                catch
                {

                }//catch...

            }//try...
            catch (Exception ex)
            {
                MessageBox.Show("Cannot Be done right now... Some issue with Internet!" + "\n" + ex.Message);
            }//catch...

        }//func...

        public void getTwitter1Data() 
        {
            ts = new TwitterSearch(
            "pNpMDTAW5Q0MACi912iQ8BhVy",
            "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
            "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
            "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");
            
            ICollection<KeyValuePair<String, Dictionary<string, string>>> temp = new Dictionary<String, Dictionary<string, string>>();
            /*temp =*/
            //long? asdf = null;
            ts.TW_TweetSearchByKeyword(searchTextBoxText, "200");
            //ts.TW_TweetSearchByKeywordPaging(searchTextBoxText, 100,1,-1);
            temp = ts.ResultSearchTweet;

            //Tuple<String, String, String, String, String, String, String> aaaa = temp[0];

            connection = new MySqlConnection(MyConnectionString);

            connection.Open();

            cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO twitter1 VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
            cmd.Parameters.Add("@text", MySqlDbType.VarChar);
            cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
            cmd.Parameters.Add("@username", MySqlDbType.VarChar);
            cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
            cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
            cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                int i = 0;

                foreach (KeyValuePair<string, Dictionary<string, string>> item in temp)
                {
                    Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                    DateTime dateTime = new DateTime();
                    String userName = "", tweetId = "", screenName = "", profile_image_url = "", description = "", photo = "", tweetUserId = "";
                    bool tem = cc.TryGetValue("userName", out userName);
                    bool tem1 = cc.TryGetValue("userprofileImgUrl", out profile_image_url);
                    bool tem2 = cc.TryGetValue("description", out description);
                    //bool tem3 = cc.TryGetValue("screenName", out age);
                    bool tem4 = cc.TryGetValue("photo", out photo);
                    tem4 = cc.TryGetValue("screenName", out screenName);
                    tem4 = cc.TryGetValue("tweetId", out tweetId);

                    tem1 = cc.TryGetValue("tweetUserId", out tweetUserId);
                    //tem1 = cc.TryGetValue("tweetsCount", out tweetsCount);
                    //tem1 = cc.TryGetValue("followingCount", out FollowingCount);

                    //item.GetType().GetProperty("dateTime").GetValue(item, null);

                    String actualDateTime;

                    tem1 = cc.TryGetValue("dateTime", out actualDateTime);

                    dateTime = DateTime.Parse(actualDateTime);

                    dateTime = dateTime.AddHours(5);

                    //dateTime=item.GetType().GetProperty("CreatedDate").GetValue(item, null);//cc.TryGetValue("dateTime", out dateTime);

                    try
                    {
                        cmd.Parameters["@text"].Value = description;
                        cmd.Parameters["@screenname"].Value = screenName;
                        cmd.Parameters["@username"].Value = userName;
                        cmd.Parameters["@datetime"].Value = dateTime.ToLongTimeString() + " " + dateTime.ToLongDateString();
                        cmd.Parameters["@coordinates"].Value = "";
                        cmd.Parameters["@profileLink"].Value = profile_image_url;
                        cmd.ExecuteNonQuery();
                    }//try...
                    catch (Exception ex)
                    {
                        int i22 = 1;
                        int sfdsfsd = 0;
                    }//catch...

                    i++;

                    if ((temp.Count % 100 == 0 && i == 99)||(temp.Count==i))
                    {
                        searchTextBoxText1 = tweetId;
                    }

                }//foreach...

                connection.Close();

            }));//outer dispatcher...

            if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)
            {

                Thread twitter2Thread = new Thread(getTwitter2Data);

                twitter2Thread.Start();

            }//if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)...
            else
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {

                    WebBrowser wB = null;

                    if (dPGlobal != null)
                        wB = dPGlobal.FindName("browse") as WebBrowser;

                    if (wB != null && wB.Document != null)
                    {
                        searchTextBoxText = osintBased[tabDynamic1.SelectedIndex].mySearch1;

                        osintBased[tabDynamic1.SelectedIndex].Header = searchTextBoxText;
                        osintBased[tabDynamic1.SelectedIndex].lastSearch1 = searchTextBoxText;

                        HTMLDocument doc = (HTMLDocument)wB.Document;

                        if (doc != null)
                        {
                            IHTMLElement theElement = doc.getElementById("query-inp");

                            if (theElement != null)
                            {
                                theElement.innerText = searchTextBoxText;

                                theElement = doc.getElementById("query-btn");
                                theElement.click();
                            }//theElement!=null...

                        }//if doc!=null...
                    }

                    //**MessageBox.Show("loaded into db");
                }//dispatcher...
        ));

            }//else...

        }//func...

        public void getTwitter2Data()
        {
            ts = new TwitterSearch(
            "pNpMDTAW5Q0MACi912iQ8BhVy",
            "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
            "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
            "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

            ICollection<KeyValuePair<String, Dictionary<string, string>>> temp = new Dictionary<String, Dictionary<string, string>>();
            /*temp =*/
            ts.TW_TweetSearchByKeywordPaging(searchTextBoxText,100,1,Convert.ToInt64(searchTextBoxText1));
            temp = ts.ResultSearchTweet;

            //Tuple<String, String, String, String, String, String, String> aaaa = temp[0];

            connection = new MySqlConnection(MyConnectionString);

            connection.Open();

            cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO twitter2 VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
            cmd.Parameters.Add("@text", MySqlDbType.VarChar);
            cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
            cmd.Parameters.Add("@username", MySqlDbType.VarChar);
            cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
            cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
            cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                int i = 0;

                foreach (KeyValuePair<string, Dictionary<string, string>> item in temp)
                {
                    Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                    DateTime dateTime = new DateTime();
                    String userName = "", tweetId = "", screenName = "", profile_image_url = "", description = "", photo = "", tweetUserId = "";
                    bool tem = cc.TryGetValue("userName", out userName);
                    bool tem1 = cc.TryGetValue("userprofileImgUrl", out profile_image_url);
                    bool tem2 = cc.TryGetValue("description", out description);
                    //bool tem3 = cc.TryGetValue("screenName", out age);
                    bool tem4 = cc.TryGetValue("photo", out photo);
                    tem4 = cc.TryGetValue("screenName", out screenName);
                    tem4 = cc.TryGetValue("tweetId", out tweetId);

                    tem1 = cc.TryGetValue("tweetUserId", out tweetUserId);
                    //tem1 = cc.TryGetValue("tweetsCount", out tweetsCount);
                    //tem1 = cc.TryGetValue("followingCount", out FollowingCount);

                    //item.GetType().GetProperty("dateTime").GetValue(item, null);

                    String actualDateTime;

                    tem1 = cc.TryGetValue("dateTime", out actualDateTime);

                    dateTime = DateTime.Parse(actualDateTime);

                    dateTime = dateTime.AddHours(5);

                    //dateTime=item.GetType().GetProperty("CreatedDate").GetValue(item, null);//cc.TryGetValue("dateTime", out dateTime);

                    try
                    {
                        cmd.Parameters["@text"].Value = description;
                        cmd.Parameters["@screenname"].Value = screenName;
                        cmd.Parameters["@username"].Value = userName;
                        cmd.Parameters["@datetime"].Value = dateTime.ToLongTimeString() + " " + dateTime.ToLongDateString();
                        cmd.Parameters["@coordinates"].Value = "";
                        cmd.Parameters["@profileLink"].Value = profile_image_url;
                        cmd.ExecuteNonQuery();
                    }//try...
                    catch (Exception ex)
                    {
                        int i22 = 1;
                        int sfdsfsd = 0;
                    }//catch...

                    i++;

                    if ((temp.Count % 100 == 0 && i == 99) || (temp.Count == i))
                    {
                        searchTextBoxText1 = tweetId;
                    }

                }//foreach...

                connection.Close();

                
            }));//dispatcher...

            if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)
            {

                Thread twitter2Thread = new Thread(getTwitter2aData);

                twitter2Thread.Start();

            }//if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)...
            else
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {

                    WebBrowser wB = null;

                    if (dPGlobal != null)
                        wB = dPGlobal.FindName("browse") as WebBrowser;

                    if (wB != null && wB.Document != null)
                    {
                        searchTextBoxText = osintBased[tabDynamic1.SelectedIndex].mySearch1;

                        osintBased[tabDynamic1.SelectedIndex].Header = searchTextBoxText;
                        osintBased[tabDynamic1.SelectedIndex].lastSearch1 = searchTextBoxText;

                        HTMLDocument doc = (HTMLDocument)wB.Document;

                        if (doc != null)
                        {
                            IHTMLElement theElement = doc.getElementById("query-inp");

                            if (theElement != null)
                            {
                                theElement.innerText = searchTextBoxText;

                                theElement = doc.getElementById("query-btn");
                                theElement.click();
                            }//theElement!=null...

                        }//if doc!=null...
                    }

                    //**MessageBox.Show("loaded into db");
                }//dispatcher...
        ));

            }//else...

        }//func...

        public void getTwitter2aData()
        {
            ts = new TwitterSearch(
            "pNpMDTAW5Q0MACi912iQ8BhVy",
            "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
            "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
            "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

            ICollection<KeyValuePair<String, Dictionary<string, string>>> temp = new Dictionary<String, Dictionary<string, string>>();
            /*temp =*/
            ts.TW_TweetSearchByKeywordPaging(searchTextBoxText, 100, 1, Convert.ToInt64(searchTextBoxText1));
            temp = ts.ResultSearchTweet;

            //Tuple<String, String, String, String, String, String, String> aaaa = temp[0];

            connection = new MySqlConnection(MyConnectionString);

            connection.Open();

            cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO twitter2 VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
            cmd.Parameters.Add("@text", MySqlDbType.VarChar);
            cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
            cmd.Parameters.Add("@username", MySqlDbType.VarChar);
            cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
            cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
            cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                int i = 0;

                foreach (KeyValuePair<string, Dictionary<string, string>> item in temp)
                {
                    Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                    DateTime dateTime = new DateTime();
                    String userName = "", tweetId = "", screenName = "", profile_image_url = "", description = "", photo = "", tweetUserId = "";
                    bool tem = cc.TryGetValue("userName", out userName);
                    bool tem1 = cc.TryGetValue("userprofileImgUrl", out profile_image_url);
                    bool tem2 = cc.TryGetValue("description", out description);
                    //bool tem3 = cc.TryGetValue("screenName", out age);
                    bool tem4 = cc.TryGetValue("photo", out photo);
                    tem4 = cc.TryGetValue("screenName", out screenName);
                    tem4 = cc.TryGetValue("tweetId", out tweetId);

                    tem1 = cc.TryGetValue("tweetUserId", out tweetUserId);
                    //tem1 = cc.TryGetValue("tweetsCount", out tweetsCount);
                    //tem1 = cc.TryGetValue("followingCount", out FollowingCount);

                    //item.GetType().GetProperty("dateTime").GetValue(item, null);

                    String actualDateTime;

                    tem1 = cc.TryGetValue("dateTime", out actualDateTime);

                    dateTime = DateTime.Parse(actualDateTime);

                    dateTime = dateTime.AddHours(5);

                    //dateTime=item.GetType().GetProperty("CreatedDate").GetValue(item, null);//cc.TryGetValue("dateTime", out dateTime);

                    try
                    {
                        cmd.Parameters["@text"].Value = description;
                        cmd.Parameters["@screenname"].Value = screenName;
                        cmd.Parameters["@username"].Value = userName;
                        cmd.Parameters["@datetime"].Value = dateTime.ToLongTimeString() + " " + dateTime.ToLongDateString();
                        cmd.Parameters["@coordinates"].Value = "";
                        cmd.Parameters["@profileLink"].Value = profile_image_url;
                        cmd.ExecuteNonQuery();
                    }//try...
                    catch (Exception ex)
                    {
                        int i22 = 1;
                        int sfdsfsd = 0;
                    }//catch...

                    i++;

                    if ((temp.Count % 100 == 0 && i == 99) || (temp.Count == i))
                    {
                        searchTextBoxText1 = tweetId;
                    }

                }//foreach...

                connection.Close();
                
            }));//outer dispatcher...

            if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)
            {

                Thread twitter2Thread = new Thread(getTwitter3Data);

                twitter2Thread.Start();

            }//if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)...
            else
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {

                    WebBrowser wB = null;

                    if (dPGlobal != null)
                        wB = dPGlobal.FindName("browse") as WebBrowser;

                    if (wB != null && wB.Document != null)
                    {
                        searchTextBoxText = osintBased[tabDynamic1.SelectedIndex].mySearch1;

                        osintBased[tabDynamic1.SelectedIndex].Header = searchTextBoxText;
                        osintBased[tabDynamic1.SelectedIndex].lastSearch1 = searchTextBoxText;

                        HTMLDocument doc = (HTMLDocument)wB.Document;

                        if (doc != null)
                        {
                            IHTMLElement theElement = doc.getElementById("query-inp");

                            if (theElement != null)
                            {
                                theElement.innerText = searchTextBoxText;

                                theElement = doc.getElementById("query-btn");
                                theElement.click();
                            }//theElement!=null...

                        }//if doc!=null...
                    }

                    //**MessageBox.Show("loaded into db");
                }//dispatcher...
        ));

            }//else...

        }//func...

        public void getTwitter3Data()
        {
            ts = new TwitterSearch(
           "pNpMDTAW5Q0MACi912iQ8BhVy",
           "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
           "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
           "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

            ICollection<KeyValuePair<String, Dictionary<string, string>>> temp = new Dictionary<String, Dictionary<string, string>>();
            /*temp =*/
            ts.TW_TweetSearchByKeywordPaging(searchTextBoxText, 100, 1, Convert.ToInt64(searchTextBoxText1));
            temp = ts.ResultSearchTweet;

            //Tuple<String, String, String, String, String, String, String> aaaa = temp[0];

            connection = new MySqlConnection(MyConnectionString);

            connection.Open();

            cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO twitter3 VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
            cmd.Parameters.Add("@text", MySqlDbType.VarChar);
            cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
            cmd.Parameters.Add("@username", MySqlDbType.VarChar);
            cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
            cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
            cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                int i = 0;

                foreach (KeyValuePair<string, Dictionary<string, string>> item in temp)
                {
                    Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                    DateTime dateTime = new DateTime();
                    String userName = "", tweetId = "", screenName = "", profile_image_url = "", description = "", photo = "", tweetUserId = "";
                    bool tem = cc.TryGetValue("userName", out userName);
                    bool tem1 = cc.TryGetValue("userprofileImgUrl", out profile_image_url);
                    bool tem2 = cc.TryGetValue("description", out description);
                    //bool tem3 = cc.TryGetValue("screenName", out age);
                    bool tem4 = cc.TryGetValue("photo", out photo);
                    tem4 = cc.TryGetValue("screenName", out screenName);
                    tem4 = cc.TryGetValue("tweetId", out tweetId);

                    tem1 = cc.TryGetValue("tweetUserId", out tweetUserId);
                    //tem1 = cc.TryGetValue("tweetsCount", out tweetsCount);
                    //tem1 = cc.TryGetValue("followingCount", out FollowingCount);

                    //item.GetType().GetProperty("dateTime").GetValue(item, null);

                    String actualDateTime;

                    tem1 = cc.TryGetValue("dateTime", out actualDateTime);

                    dateTime = DateTime.Parse(actualDateTime);

                    dateTime = dateTime.AddHours(5);

                    //dateTime=item.GetType().GetProperty("CreatedDate").GetValue(item, null);//cc.TryGetValue("dateTime", out dateTime);

                    try
                    {
                        cmd.Parameters["@text"].Value = description;
                        cmd.Parameters["@screenname"].Value = screenName;
                        cmd.Parameters["@username"].Value = userName;
                        cmd.Parameters["@datetime"].Value = dateTime.ToLongTimeString() + " " + dateTime.ToLongDateString();
                        cmd.Parameters["@coordinates"].Value = "";
                        cmd.Parameters["@profileLink"].Value = profile_image_url;
                        cmd.ExecuteNonQuery();
                    }//try...
                    catch (Exception ex)
                    {
                        int i22 = 1;
                        int sfdsfsd = 0;
                    }//catch...

                    i++;

                    if ((temp.Count % 100 == 0 && i == 99) || (temp.Count == i))
                    {
                        searchTextBoxText1 = tweetId;
                    }

                }//foreach...

                connection.Close();

            }));//outer dispatcher...

            if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)
            {

                Thread twitter2Thread = new Thread(getTwitter3aData);

                twitter2Thread.Start();

            }//if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)...
            else
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {

                    WebBrowser wB = null;

                    if (dPGlobal != null)
                        wB = dPGlobal.FindName("browse") as WebBrowser;

                    if (wB != null && wB.Document != null)
                    {
                        searchTextBoxText = osintBased[tabDynamic1.SelectedIndex].mySearch1;

                        osintBased[tabDynamic1.SelectedIndex].Header = searchTextBoxText;
                        osintBased[tabDynamic1.SelectedIndex].lastSearch1 = searchTextBoxText;

                        HTMLDocument doc = (HTMLDocument)wB.Document;

                        if (doc != null)
                        {
                            IHTMLElement theElement = doc.getElementById("query-inp");

                            if (theElement != null)
                            {
                                theElement.innerText = searchTextBoxText;

                                theElement = doc.getElementById("query-btn");
                                theElement.click();
                            }//theElement!=null...

                        }//if doc!=null...
                    }

                    //**MessageBox.Show("loaded into db");
                }//dispatcher...
        ));

            }//else...

        }//func...

        public void getTwitter3aData()
        {
            ts = new TwitterSearch(
           "pNpMDTAW5Q0MACi912iQ8BhVy",
           "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
           "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
           "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

            ICollection<KeyValuePair<String, Dictionary<string, string>>> temp = new Dictionary<String, Dictionary<string, string>>();
            /*temp =*/
            ts.TW_TweetSearchByKeywordPaging(searchTextBoxText, 100, 1, Convert.ToInt64(searchTextBoxText1));
            temp = ts.ResultSearchTweet;

            //Tuple<String, String, String, String, String, String, String> aaaa = temp[0];

            connection = new MySqlConnection(MyConnectionString);

            connection.Open();

            cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO twitter3 VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
            cmd.Parameters.Add("@text", MySqlDbType.VarChar);
            cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
            cmd.Parameters.Add("@username", MySqlDbType.VarChar);
            cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
            cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
            cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                int i = 0;

                foreach (KeyValuePair<string, Dictionary<string, string>> item in temp)
                {
                    Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                    DateTime dateTime = new DateTime();
                    String userName = "", tweetId = "", screenName = "", profile_image_url = "", description = "", photo = "", tweetUserId = "";
                    bool tem = cc.TryGetValue("userName", out userName);
                    bool tem1 = cc.TryGetValue("userprofileImgUrl", out profile_image_url);
                    bool tem2 = cc.TryGetValue("description", out description);
                    //bool tem3 = cc.TryGetValue("screenName", out age);
                    bool tem4 = cc.TryGetValue("photo", out photo);
                    tem4 = cc.TryGetValue("screenName", out screenName);
                    tem4 = cc.TryGetValue("tweetId", out tweetId);

                    tem1 = cc.TryGetValue("tweetUserId", out tweetUserId);
                    //tem1 = cc.TryGetValue("tweetsCount", out tweetsCount);
                    //tem1 = cc.TryGetValue("followingCount", out FollowingCount);

                    //item.GetType().GetProperty("dateTime").GetValue(item, null);

                    String actualDateTime;

                    tem1 = cc.TryGetValue("dateTime", out actualDateTime);

                    dateTime = DateTime.Parse(actualDateTime);

                    dateTime = dateTime.AddHours(5);

                    //dateTime=item.GetType().GetProperty("CreatedDate").GetValue(item, null);//cc.TryGetValue("dateTime", out dateTime);

                    try
                    {
                        cmd.Parameters["@text"].Value = description;
                        cmd.Parameters["@screenname"].Value = screenName;
                        cmd.Parameters["@username"].Value = userName;
                        cmd.Parameters["@datetime"].Value = dateTime.ToLongTimeString() + " " + dateTime.ToLongDateString();
                        cmd.Parameters["@coordinates"].Value = "";
                        cmd.Parameters["@profileLink"].Value = profile_image_url;
                        cmd.ExecuteNonQuery();
                    }//try...
                    catch (Exception ex)
                    {
                        int i22 = 1;
                        int sfdsfsd = 0;
                    }//catch...

                    i++;

                    if ((temp.Count % 100 == 0 && i == 99) || (temp.Count == i))
                    {
                        searchTextBoxText1 = tweetId;
                    }

                }//foreach...

                connection.Close();

            }));//outer diapatcher...

            if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)
            {

                Thread twitter2Thread = new Thread(getTwitter3bData);

                twitter2Thread.Start();

            }//if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)...
            else
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {

                    WebBrowser wB = null;

                    if (dPGlobal != null)
                        wB = dPGlobal.FindName("browse") as WebBrowser;

                    if (wB != null && wB.Document != null)
                    {
                        searchTextBoxText = osintBased[tabDynamic1.SelectedIndex].mySearch1;

                        osintBased[tabDynamic1.SelectedIndex].Header = searchTextBoxText;
                        osintBased[tabDynamic1.SelectedIndex].lastSearch1 = searchTextBoxText;

                        HTMLDocument doc = (HTMLDocument)wB.Document;

                        if (doc != null)
                        {
                            IHTMLElement theElement = doc.getElementById("query-inp");

                            if (theElement != null)
                            {
                                theElement.innerText = searchTextBoxText;

                                theElement = doc.getElementById("query-btn");
                                theElement.click();
                            }//theElement!=null...

                        }//if doc!=null...
                    }

                    //**MessageBox.Show("loaded into db");
                }//dispatcher...
        ));

            }//else...

        }//func...

        public void getTwitter3bData()
        {
            ts = new TwitterSearch(
           "pNpMDTAW5Q0MACi912iQ8BhVy",
           "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
           "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
           "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

            ICollection<KeyValuePair<String, Dictionary<string, string>>> temp = new Dictionary<String, Dictionary<string, string>>();
            /*temp =*/
            ts.TW_TweetSearchByKeywordPaging(searchTextBoxText, 100, 1, Convert.ToInt64(searchTextBoxText1));
            temp = ts.ResultSearchTweet;

            //Tuple<String, String, String, String, String, String, String> aaaa = temp[0];

            connection = new MySqlConnection(MyConnectionString);

            connection.Open();

            cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO twitter3 VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
            cmd.Parameters.Add("@text", MySqlDbType.VarChar);
            cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
            cmd.Parameters.Add("@username", MySqlDbType.VarChar);
            cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
            cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
            cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                int i = 0;

                foreach (KeyValuePair<string, Dictionary<string, string>> item in temp)
                {
                    Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                    DateTime dateTime = new DateTime();
                    String userName = "", tweetId = "", screenName = "", profile_image_url = "", description = "", photo = "", tweetUserId = "";
                    bool tem = cc.TryGetValue("userName", out userName);
                    bool tem1 = cc.TryGetValue("userprofileImgUrl", out profile_image_url);
                    bool tem2 = cc.TryGetValue("description", out description);
                    //bool tem3 = cc.TryGetValue("screenName", out age);
                    bool tem4 = cc.TryGetValue("photo", out photo);
                    tem4 = cc.TryGetValue("screenName", out screenName);
                    tem4 = cc.TryGetValue("tweetId", out tweetId);

                    tem1 = cc.TryGetValue("tweetUserId", out tweetUserId);
                    //tem1 = cc.TryGetValue("tweetsCount", out tweetsCount);
                    //tem1 = cc.TryGetValue("followingCount", out FollowingCount);

                    //item.GetType().GetProperty("dateTime").GetValue(item, null);

                    String actualDateTime;

                    tem1 = cc.TryGetValue("dateTime", out actualDateTime);

                    dateTime = DateTime.Parse(actualDateTime);

                    dateTime = dateTime.AddHours(5);

                    //dateTime=item.GetType().GetProperty("CreatedDate").GetValue(item, null);//cc.TryGetValue("dateTime", out dateTime);

                    try
                    {
                        cmd.Parameters["@text"].Value = description;
                        cmd.Parameters["@screenname"].Value = screenName;
                        cmd.Parameters["@username"].Value = userName;
                        cmd.Parameters["@datetime"].Value = dateTime.ToLongTimeString() + " " + dateTime.ToLongDateString();
                        cmd.Parameters["@coordinates"].Value = "";
                        cmd.Parameters["@profileLink"].Value = profile_image_url;
                        cmd.ExecuteNonQuery();
                    }//try...
                    catch (Exception ex)
                    {
                        int i22 = 1;
                        int sfdsfsd = 0;
                    }//catch...

                    i++;

                    if ((temp.Count % 100 == 0 && i == 99) || (temp.Count == i))
                    {
                        searchTextBoxText1 = tweetId;
                    }

                }//foreach...

                connection.Close();

            }));//outer dispatcher...

            if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)
            {

                Thread twitter2Thread = new Thread(getTwitter4Data);

                twitter2Thread.Start();

            }//if (searchTextBoxText1 != null & searchTextBoxText1.Trim().Length > 0)...
            else
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {

                    WebBrowser wB = null;

                    if (dPGlobal != null)
                        wB = dPGlobal.FindName("browse") as WebBrowser;

                    if (wB != null && wB.Document != null)
                    {
                        searchTextBoxText = osintBased[tabDynamic1.SelectedIndex].mySearch1;

                        osintBased[tabDynamic1.SelectedIndex].Header = searchTextBoxText;
                        osintBased[tabDynamic1.SelectedIndex].lastSearch1 = searchTextBoxText;

                        HTMLDocument doc = (HTMLDocument)wB.Document;

                        if (doc != null)
                        {
                            IHTMLElement theElement = doc.getElementById("query-inp");

                            if (theElement != null)
                            {
                                theElement.innerText = searchTextBoxText;

                                theElement = doc.getElementById("query-btn");
                                theElement.click();
                            }//theElement!=null...

                        }//if doc!=null...
                    }

                    //**MessageBox.Show("loaded into db");
                }//dispatcher...
        ));

            }//else...

        }//func...

        public void getTwitter4Data()
        {

            ts = new TwitterSearch(
           "pNpMDTAW5Q0MACi912iQ8BhVy",
           "NyhsqogTpY2996YUQv5fLMzleIVsZxbLtLLCUYEfVhKrmgAHKO",
           "355691359-jTZR3A9ZYj4yZxfuDcGs0tWz2z9IzaFo5wMkpGua",
           "oP2Zh0h9oScRoIV6hOP7jtMk0qJoYW5gvDkSm2z568ESI");

            ICollection<KeyValuePair<String, Dictionary<string, string>>> temp = new Dictionary<String, Dictionary<string, string>>();
            /*temp =*/
            ts.TW_TweetSearchByKeywordPaging(searchTextBoxText, 100, 1, Convert.ToInt64(searchTextBoxText1));
            temp = ts.ResultSearchTweet;

            //Tuple<String, String, String, String, String, String, String> aaaa = temp[0];

            connection = new MySqlConnection(MyConnectionString);

            connection.Open();

            cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO twitter4 VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
            cmd.Parameters.Add("@text", MySqlDbType.VarChar);
            cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
            cmd.Parameters.Add("@username", MySqlDbType.VarChar);
            cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
            cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
            cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                int i = 0;

                foreach (KeyValuePair<string, Dictionary<string, string>> item in temp)
                {
                    Dictionary<string, string> cc = item.Value as Dictionary<string, string>;
                    DateTime dateTime = new DateTime();
                    String userName = "", tweetId = "", screenName = "", profile_image_url = "", description = "", photo = "", tweetUserId = "";
                    bool tem = cc.TryGetValue("userName", out userName);
                    bool tem1 = cc.TryGetValue("userprofileImgUrl", out profile_image_url);
                    bool tem2 = cc.TryGetValue("description", out description);
                    //bool tem3 = cc.TryGetValue("screenName", out age);
                    bool tem4 = cc.TryGetValue("photo", out photo);
                    tem4 = cc.TryGetValue("screenName", out screenName);
                    tem4 = cc.TryGetValue("tweetId", out tweetId);

                    tem1 = cc.TryGetValue("tweetUserId", out tweetUserId);
                    //tem1 = cc.TryGetValue("tweetsCount", out tweetsCount);
                    //tem1 = cc.TryGetValue("followingCount", out FollowingCount);

                    //item.GetType().GetProperty("dateTime").GetValue(item, null);

                    String actualDateTime;

                    tem1 = cc.TryGetValue("dateTime", out actualDateTime);

                    dateTime = DateTime.Parse(actualDateTime);

                    dateTime = dateTime.AddHours(5);

                    //dateTime=item.GetType().GetProperty("CreatedDate").GetValue(item, null);//cc.TryGetValue("dateTime", out dateTime);

                    try
                    {
                        cmd.Parameters["@text"].Value = description;
                        cmd.Parameters["@screenname"].Value = screenName;
                        cmd.Parameters["@username"].Value = userName;
                        cmd.Parameters["@datetime"].Value = dateTime.ToLongTimeString() + " " + dateTime.ToLongDateString();
                        cmd.Parameters["@coordinates"].Value = "";
                        cmd.Parameters["@profileLink"].Value = profile_image_url;
                        cmd.ExecuteNonQuery();
                    }//try...
                    catch (Exception ex)
                    {
                        int i22 = 1;
                        int sfdsfsd = 0;
                    }//catch...

                    i++;

                    searchTextBoxText1 = "";

                }//foreach...

                connection.Close();

            }));

            App.Current.Dispatcher.Invoke((Action)(() =>
            {

                WebBrowser wB = null;

                if (dPGlobal != null)
                    wB = dPGlobal.FindName("browse") as WebBrowser;

                if (wB != null && wB.Document != null)
                {
                    searchTextBoxText = osintBased[tabDynamic1.SelectedIndex].mySearch1;

                    osintBased[tabDynamic1.SelectedIndex].Header = searchTextBoxText;
                    osintBased[tabDynamic1.SelectedIndex].lastSearch1 = searchTextBoxText;

                    HTMLDocument doc = (HTMLDocument)wB.Document;

                    if (doc != null)
                    {
                        IHTMLElement theElement = doc.getElementById("query-inp");

                        if (theElement != null)
                        {
                            theElement.innerText = searchTextBoxText;

                            theElement = doc.getElementById("query-btn");
                            theElement.click();
                        }//theElement!=null...

                    }//if doc!=null...
                }

                //**MessageBox.Show("loaded into db");
            }//dispatcher...
                ));


        }//func...

        private void searchOsintGoogle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (osintGoogleBased[tabDynamicGoogle.SelectedIndex].mySearch1 == null || osintGoogleBased[tabDynamicGoogle.SelectedIndex].mySearch1.Trim().Length == 0)
                {
                    MessageBox.Show("Nothing to search for!");
                    return;
                }//if (searchTextBoxText == null || searchTextBoxText.Trim().Length == 0) ...

                
                Button but = sender as Button;
                StackPanel sp = but.Parent as StackPanel;
                Border bor = sp.Parent as Border;
                DockPanel dockP = bor.Parent as DockPanel;

                dPGlobal = dockP;

                /**

                if (tweetsToReportList != null)
                    tweetsToReportList.Clear();
                else
                    tweetsToReportList = new ObservableCollection<tweetToReportItem>();

                //tweetsToReportList.where...

                WebBrowser wB = dockP.FindName("browse") as WebBrowser;

                if (wB != null && wB.Document != null)
                {
                    searchTextBoxText = osintBased[tabDynamic1.SelectedIndex].mySearch1;

                    osintBased[tabDynamic1.SelectedIndex].Header = searchTextBoxText;
                    osintBased[tabDynamic1.SelectedIndex].lastSearch1 = searchTextBoxText;

                    HTMLDocument doc = (HTMLDocument)wB.Document;

                    if (doc != null)
                    {
                        IHTMLElement theElement = doc.getElementById("query-inp");

                        if (theElement != null)
                        {
                            theElement.innerText = searchTextBoxText;

                            theElement = doc.getElementById("query-btn");
                            theElement.click();
                        }//theElement!=null...

                    }//if doc!=null...
                }
                */

                   try
                   {
                       searchTextBoxText = osintGoogleBased[tabDynamicGoogle.SelectedIndex].mySearch1;

                       connection = new MySqlConnection(MyConnectionString);
                       connection.Open();

                       using (MySqlCommand command = new MySqlCommand("DELETE FROM google1", connection))
                       {
                           command.ExecuteNonQuery();
                       }
                       using (MySqlCommand command = new MySqlCommand("DELETE FROM google2", connection))
                       {
                           command.ExecuteNonQuery();
                       }
                       using (MySqlCommand command = new MySqlCommand("DELETE FROM google3", connection))
                       {
                           command.ExecuteNonQuery();
                       }
                       using (MySqlCommand command = new MySqlCommand("DELETE FROM google4", connection))
                       {
                           command.ExecuteNonQuery();
                       }
                       connection.Close();

                       WebBrowser wB = null;

                       if (dPGlobal != null)
                           wB = dPGlobal.FindName("browse") as WebBrowser;

                       if (wB != null && wB.Document != null)
                       {
                           searchTextBoxText = osintGoogleBased[tabDynamicGoogle.SelectedIndex].mySearch1;

                           osintGoogleBased[tabDynamicGoogle.SelectedIndex].Header = searchTextBoxText;
                           osintGoogleBased[tabDynamicGoogle.SelectedIndex].lastSearch1 = searchTextBoxText;

                           HTMLDocument doc = (HTMLDocument)wB.Document;

                           if (doc != null)
                           {
                               IHTMLElement theElement = doc.getElementById("query-inp");

                               if (theElement != null)
                               {
                                   theElement.innerText = searchTextBoxText;

                                   theElement = doc.getElementById("query-btn123");
                                   theElement.click();
                               }//theElement!=null...

                           }//if doc!=null...
                       }//if (wB != null && wB.Document != null)...
                
                      Thread google1Thread = new Thread(getGoogle1Data);

                      google1Thread.Start();

                       /////////////////////////////////////////////////////////////////////////////////////////////////////

                       /**
                       connection = new MySqlConnection(MyConnectionString);

                       connection.Open();
 
                       cmd = connection.CreateCommand();
                       cmd.CommandText = "INSERT INTO tweets VALUES(NULL, @v, @a, @username, @datetime, @tweet);";
                       cmd.Parameters.Add("@v", MySqlDbType.Double);
                       cmd.Parameters.Add("@a", MySqlDbType.Double);
                       cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                       cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
                       cmd.Parameters.Add("@tweet", MySqlDbType.VarChar);

                       foreach (tweetToReportItem v in tweetsToReportList)
                       {
                           try
                           {
                               cmd.Parameters["@username"].Value =  v.userName;
                               cmd.Parameters["@datetime"].Value = v.dateTime;
                               cmd.Parameters["@screenname"].Value =  v.dateTime;
                               cmd.Parameters["@text"].Value =  v.tweet;
                               cmd.Parameters["@coordinates"].Value = v.tweet;
                               cmd.ExecuteNonQuery();
                           }
                           catch { }
                       }//foreach...
                        
                       connection.Close();
                        */
                   }//try...
                   catch 
                   {

                   }//catch...
                
            }//try...
            catch (Exception ex)
            {
                MessageBox.Show("Cannot Be done right now... Some issue with Internet!" + "\n" + ex.Message);
            }//catch...

        }//func...

        public void getGoogle1Data()
        {

            string nextPageToken = "";

            for (int i = 0; i < 4; i++)
            {
                if (i > 0 && nextPageToken.Length == 0) break;

                IList<Activity> ActList = GS.GP_SearchActivities(GS.Service, searchTextBoxText, 20, nextPageToken);

                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    if (ActList != null && ActList.Count > 0)
                    {
                        connection = new MySqlConnection(MyConnectionString);

                        connection.Open();

                        cmd = connection.CreateCommand();
                        cmd.CommandText = "INSERT INTO google1 VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
                        cmd.Parameters.Add("@text", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

                        foreach (Activity P in ActList)
                        {
                            try
                            {
                                if (P.Url == null && P.Title != null && P.Title.Trim().Length > 0)
                                { nextPageToken = P.Title; searchTextBoxText1 = nextPageToken; }

                                if (P.Url == null || P.Title == null || P.Title.Trim().Length == 0) continue;

                                try
                                {
                                    cmd.Parameters["@text"].Value = P.Title;
                                    cmd.Parameters["@screenname"].Value = P.Actor.DisplayName;
                                    cmd.Parameters["@username"].Value = P.Actor.DisplayName;
                                    cmd.Parameters["@datetime"].Value = P.Published.ToString();
                                    cmd.Parameters["@coordinates"].Value = "";
                                    cmd.Parameters["@profileLink"].Value = P.Url;
                                    cmd.ExecuteNonQuery();
                                }//try...
                                catch (Exception ex)
                                {
                                    int i22 = 1;
                                    int sfdsfsd = 0;
                                }


                                int i6666 = 0;
                                int j = 0;
                            }//try...
                            catch
                            {

                            }//catch...

                        }//foreach...

                        connection.Close();

                    }//if(ActList!=null)...

                    //MessageBox.Show("done");

                }));

            }//end of 80 wala loop...

            if (nextPageToken != null & nextPageToken.Trim().Length > 0)
            {

                Thread google2Thread = new Thread(getGoogle2Data);

                google2Thread.Start();

            }//if (nextPageToken != null & nextPageToken.Trim().Length > 0)...

        }//func...

        public void getGoogle2Data()
        {
            string nextPageToken = searchTextBoxText1;

            for (int i = 0; i < 4; i++)
            {
                if (i > 0 && nextPageToken.Length == 0) break;

                IList<Activity> ActList = GS.GP_SearchActivities(GS.Service, searchTextBoxText, 20, nextPageToken);

                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    if (ActList != null && ActList.Count > 0)
                    {
                        connection = new MySqlConnection(MyConnectionString);

                        connection.Open();

                        cmd = connection.CreateCommand();
                        cmd.CommandText = "INSERT INTO google2 VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
                        cmd.Parameters.Add("@text", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

                        foreach (Activity P in ActList)
                        {
                            try
                            {
                                if (P.Url == null && P.Title != null && P.Title.Trim().Length > 0)
                                { nextPageToken = P.Title; searchTextBoxText1 = nextPageToken; }

                                if (P.Url == null || P.Title == null || P.Title.Trim().Length == 0) continue;

                                try
                                {
                                    cmd.Parameters["@text"].Value = P.Title;
                                    cmd.Parameters["@screenname"].Value = P.Actor.DisplayName;
                                    cmd.Parameters["@username"].Value = P.Actor.DisplayName;
                                    cmd.Parameters["@datetime"].Value = P.Published.ToString();
                                    cmd.Parameters["@coordinates"].Value = "";
                                    cmd.Parameters["@ProfileLink"].Value = P.Url;
                                    cmd.ExecuteNonQuery();
                                }//try...
                                catch (Exception ex)
                                {
                                    int i22 = 1;
                                    int sfdsfsd = 0;
                                }


                                int i6666 = 0;
                                int j = 0;
                            }//try...
                            catch
                            {

                            }//catch...

                        }//foreach...

                        connection.Close();

                    }//if(ActList!=null)...

                    //MessageBox.Show("done");

                }));

            }//end of 80 wala loop...

            if (nextPageToken != null & nextPageToken.Trim().Length > 0)
            {

                Thread google3Thread = new Thread(getGoogle3Data);

                google3Thread.Start();

            }//if (nextPageToken != null & nextPageToken.Trim().Length > 0)...

        }//func...

        public void getGoogle3Data()
        {
            string nextPageToken = searchTextBoxText1;

            for (int i = 0; i < 4; i++)
            {
                if (i > 0 && nextPageToken.Length == 0) break;

                IList<Activity> ActList = GS.GP_SearchActivities(GS.Service, searchTextBoxText, 20, nextPageToken);

                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    if (ActList != null && ActList.Count > 0)
                    {
                        connection = new MySqlConnection(MyConnectionString);

                        connection.Open();

                        cmd = connection.CreateCommand();
                        cmd.CommandText = "INSERT INTO google3 VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
                        cmd.Parameters.Add("@text", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

                        foreach (Activity P in ActList)
                        {
                            try
                            {
                                if (P.Url == null && P.Title != null && P.Title.Trim().Length > 0)
                                { nextPageToken = P.Title; searchTextBoxText1 = nextPageToken; }

                                if (P.Url == null || P.Title == null || P.Title.Trim().Length == 0) continue;

                                try
                                {
                                    cmd.Parameters["@text"].Value = P.Title;
                                    cmd.Parameters["@screenname"].Value = P.Actor.DisplayName;
                                    cmd.Parameters["@username"].Value = P.Actor.DisplayName;
                                    cmd.Parameters["@datetime"].Value = P.Published.ToString();
                                    cmd.Parameters["@coordinates"].Value = "";
                                    cmd.Parameters["@profileLink"].Value = P.Url;
                                    cmd.ExecuteNonQuery();
                                }//try...
                                catch (Exception ex)
                                {
                                    int i22 = 1;
                                    int sfdsfsd = 0;
                                }


                                int i6666 = 0;
                                int j = 0;
                            }//try...
                            catch
                            {

                            }//catch...

                        }//foreach...

                        connection.Close();

                    }//if(ActList!=null)...

                    //MessageBox.Show("done");

                }));

            }//end of 80 wala loop...

            if (nextPageToken != null & nextPageToken.Trim().Length > 0)
            {

                Thread google3Thread = new Thread(getGoogle4Data);

                google3Thread.Start();

            }//if (nextPageToken != null & nextPageToken.Trim().Length > 0)...

        }//func...

        public void getGoogle4Data()
        {
            string nextPageToken = searchTextBoxText1;

            for (int i = 0; i < 4; i++)
            {
                if (i > 0 && nextPageToken.Length == 0) break;

                IList<Activity> ActList = GS.GP_SearchActivities(GS.Service, searchTextBoxText, 20, nextPageToken);

                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    if (ActList != null && ActList.Count > 0)
                    {
                        connection = new MySqlConnection(MyConnectionString);

                        connection.Open();

                        cmd = connection.CreateCommand();
                        cmd.CommandText = "INSERT INTO google4 VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
                        cmd.Parameters.Add("@text", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

                        foreach (Activity P in ActList)
                        {
                            try
                            {
                                if (P.Url == null && P.Title != null && P.Title.Trim().Length > 0)
                                { nextPageToken = P.Title; searchTextBoxText1 = nextPageToken; }

                                if (P.Url == null || P.Title == null || P.Title.Trim().Length == 0) continue;

                                try
                                {
                                    cmd.Parameters["@text"].Value = P.Title;
                                    cmd.Parameters["@screenname"].Value = P.Actor.DisplayName;
                                    cmd.Parameters["@username"].Value = P.Actor.DisplayName;
                                    cmd.Parameters["@datetime"].Value = P.Published.ToString();
                                    cmd.Parameters["@coordinates"].Value = "";
                                    cmd.Parameters["@profileLink"].Value = P.Url;
                                    cmd.ExecuteNonQuery();
                                }//try...
                                catch (Exception ex)
                                {
                                    int i22 = 1;
                                    int sfdsfsd = 0;
                                }


                                int i6666 = 0;
                                int j = 0;
                            }//try...
                            catch
                            {

                            }//catch...

                        }//foreach...

                        connection.Close();

                    }//if(ActList!=null)...

                    //MessageBox.Show("done");

                }));

            }//end of 80 wala loop...

            App.Current.Dispatcher.Invoke((Action)(() =>
                {

                    WebBrowser wB = null;
                    
                    if(dPGlobal!=null)
                    wB=dPGlobal.FindName("browse") as WebBrowser;

                    if (wB != null && wB.Document != null)
                    {
                        searchTextBoxText = osintGoogleBased[tabDynamic1.SelectedIndex].mySearch1;

                        osintGoogleBased[tabDynamicGoogle.SelectedIndex].Header = searchTextBoxText;
                        osintGoogleBased[tabDynamicGoogle.SelectedIndex].lastSearch1 = searchTextBoxText;

                        HTMLDocument doc = (HTMLDocument)wB.Document;

                        if (doc != null)
                        {
                            IHTMLElement theElement = doc.getElementById("query-inp");

                            if (theElement != null)
                            {
                                theElement.innerText = searchTextBoxText;

                                theElement = doc.getElementById("query-btn");
                                theElement.click();
                            }//theElement!=null...

                        }//if doc!=null...
                    }
                
                    //**MessageBox.Show("loaded into db");
                }//dispatcher...
                ));

        }//func...

        private void searchOsintfb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (osintfbBased[tabDynamicfb.SelectedIndex].mySearch1 == null || osintfbBased[tabDynamicfb.SelectedIndex].mySearch1.Trim().Length == 0)
                {
                    MessageBox.Show("Nothing to search for!");
                    return;
                }//if (searchTextBoxText == null || searchTextBoxText.Trim().Length == 0) ...


                Button but = sender as Button;
                StackPanel sp = but.Parent as StackPanel;
                Border bor = sp.Parent as Border;
                DockPanel dockP = bor.Parent as DockPanel;

                dPGlobal = dockP;

                try
                {
                    searchTextBoxText = osintfbBased[tabDynamicfb.SelectedIndex].mySearch1;

                    connection = new MySqlConnection(MyConnectionString);
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("DELETE FROM fb1", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM fb2", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM fb3", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM fb4", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();

                    WebBrowser wB = null;

                    if (dPGlobal != null)
                        wB = dPGlobal.FindName("browse") as WebBrowser;

                    if (wB != null && wB.Document != null)
                    {
                        searchTextBoxText = osintfbBased[tabDynamicfb.SelectedIndex].mySearch1;

                        osintfbBased[tabDynamicfb.SelectedIndex].Header = searchTextBoxText;
                        osintfbBased[tabDynamicfb.SelectedIndex].lastSearch1 = searchTextBoxText;

                        HTMLDocument doc = (HTMLDocument)wB.Document;

                        if (doc != null)
                        {
                            IHTMLElement theElement = doc.getElementById("query-inp");

                            if (theElement != null)
                            {
                                theElement.innerText = searchTextBoxText;

                                theElement = doc.getElementById("query-btn123");
                                theElement.click();
                            }//theElement!=null...

                        }//if doc!=null...
                    }//if (wB != null && wB.Document != null)...
                

                    Thread google1Thread = new Thread(getAllFbPagesFirst);

                    google1Thread.Start();

                    /////////////////////////////////////////////////////////////////////////////////////////////////////

                }//try...
                catch
                {

                }//catch...

            }//try...
            catch (Exception ex)
            {
                MessageBox.Show("Cannot Be done right now... Some issue with Internet!" + "\n" + ex.Message);
            }//catch...

        }//func...

        public void getAllFbPagesFirst() 
        {
            fs1 = new FacebookSearch(fbSessionId);
            fs1.Query(searchTextBoxText, "page", "", "16","0");
                    
                       int pageCount = 0;

                    //*** Adding search result into some LIST listBoxSearch ****/
                    foreach (KeyValuePair<string, string> item in fs1.ResultSearch)
                    {
                        connection = new MySqlConnection(MyConnectionString);

                        connection.Open();

                        String which_table="fb1";

                        if (pageCount >= 4 && pageCount <= 7) which_table = "fb2";
                        if (pageCount >= 8 && pageCount <= 11) which_table = "fb3";
                        if (pageCount >= 12 && pageCount <= 15) which_table = "fb4";

                        cmd = connection.CreateCommand();
                        cmd.CommandText = "INSERT INTO "+which_table+" VALUES(NULL, @text, @screenname, @username, @datetime, @coordinates,@profileLink);";
                        cmd.Parameters.Add("@text", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@screenname", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@coordinates", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@profileLink", MySqlDbType.VarChar);

                        // fbPage_ID = item.Key,

                        // now getting statues of each page individually in above foreach loop...

                        fs = new FacebookSearch(fbSessionId);
                        fs.GetPageDataByID(item.Key, "50", "0");

                        //((string)(((JsonObject)fs.FacebookData)["name"]));...
                        JsonArray data = null;

                        if (fs.FacebookData != null)
                        {
                            data = ((JsonArray)((JsonObject)fs.FacebookData)["data"]);

                            foreach (JsonObject status in data)
                            {
                                JsonObject from = ((JsonObject)status["from"]);
                                string data_img = ""; string desc = ""; bool dataImgVisibility = false;

                                string created_time = "", updated_time = "";

                                string likesCount = "0", commentCount = "0", shareCount = "0";

                                if (((String)status["type"]).Equals("photo") && status.ContainsKey("full_picture"))
                                {
                                    data_img = ((String)status["full_picture"]);
                                    dataImgVisibility = true;
                                }//if (((String)status["type"]).Equals("photo") && status.ContainsKey("picture")) ...

                                if (status.ContainsKey("likes") == true)
                                {
                                    JsonObject likes = ((JsonObject)status["likes"]);
                                    if (likes.ContainsKey("summary") == true) { likesCount = Convert.ToString(((long)((JsonObject)likes["summary"])["total_count"])); }
                                }

                                if (status.ContainsKey("comments") == true)
                                {
                                    JsonObject comments = ((JsonObject)status["comments"]);
                                    if (comments.ContainsKey("summary") == true) { commentCount = Convert.ToString(((long)((JsonObject)comments["summary"])["total_count"])); }
                                }
                                if (status.ContainsKey("shares") == true)
                                {
                                    JsonObject share = ((JsonObject)status["shares"]);
                                    shareCount = Convert.ToString((long)(share["count"]));
                                }
                                if (status.ContainsKey("created_time") == true) created_time = ((String)status["created_time"]);
                                if (status.ContainsKey("updated_time") == true) updated_time = ((String)status["updated_time"]);

                                if (status.ContainsKey("story") == true) desc = ((String)status["story"]);
                                else if (status.ContainsKey("message") == true) desc = ((String)status["message"]);
                                else if (status.ContainsKey("description") == true) desc = ((String)status["description"]);

                                if (desc == null || desc.Trim().Length == 0) continue;
                                
                                    try
                                    {
                                        cmd.Parameters["@text"].Value = desc;//P.Title;
                                        cmd.Parameters["@screenname"].Value = ((String)from["name"]);//P.Actor.DisplayName;
                                        cmd.Parameters["@username"].Value = ((String)from["name"]);//P.Actor.DisplayName;
                                        cmd.Parameters["@datetime"].Value = fbDateTimeConverter(created_time);//P.Published.ToString();
                                        cmd.Parameters["@coordinates"].Value = "";
                                        cmd.Parameters["@profileLink"].Value = "https://www.facebook.com/" + ((String)from["id"]); //P.Url;
                                        cmd.ExecuteNonQuery();
                                    }//try...
                                    catch (Exception ex)
                                    {
                                        int i22 = 1;
                                        int sfdsfsd = 0;
                                    }


                            }//foreach...

                        }//if fs.facebook!=null...

                        /////////////////////////////////////////////////////////////////////////

                        if(connection!=null)
                        connection.Close();

                        pageCount++;

                    }//foreach...

                    App.Current.Dispatcher.Invoke((Action)(() =>
                     {
            
                    WebBrowser wB = null;

                    if (dPGlobal != null)
                        wB = dPGlobal.FindName("browse") as WebBrowser;

                    if (wB != null && wB.Document != null)
                    {
                        searchTextBoxText = osintfbBased[tabDynamicfb.SelectedIndex].mySearch1;

                        osintfbBased[tabDynamicfb.SelectedIndex].Header = searchTextBoxText;
                        osintfbBased[tabDynamicfb.SelectedIndex].lastSearch1 = searchTextBoxText;

                        HTMLDocument doc = (HTMLDocument)wB.Document;

                        if (doc != null)
                        {
                            IHTMLElement theElement = doc.getElementById("query-inp");

                            if (theElement != null)
                            {
                                theElement.innerText = searchTextBoxText;

                                theElement = doc.getElementById("query-btn");
                                theElement.click();
                            }//theElement!=null...

                        }//if doc!=null...
                    }//if (wB != null && wB.Document != null)...
                
                   }));//dispatcher...

        }//func...

        private void addTweetsToReport_Click(object sender, RoutedEventArgs e)
        {
            if (osintBased[tabDynamic1.SelectedIndex].lastSearch1 == null || osintBased[tabDynamic1.SelectedIndex].lastSearch1.Trim().Length == 0)
            {
                MessageBox.Show("Please search first!");
                return;
            }//if (tweetsToReportList.Count == 0) ...

            Button but = sender as Button;

            StackPanel sp = but.Parent as StackPanel;

            Border bbr = sp.Parent as Border;

            DockPanel dpp = bbr.Parent as DockPanel;

            WebBrowser wB = dpp.FindName("browse") as WebBrowser;

            if (wB!=null&&wB.Document != null)
            {
                HTMLDocument doc = (HTMLDocument)wB.Document;
                IHTMLElement theElement = doc.getElementById("tweet-tbl");
                
            
                if(theElement!=null&&theElement.children!=null/*&&theElement.children[1].childNodes!=null&&theElement.children[1].childNodes.length>0*/)
                {
                    //theElement.children[1].childNodes.length
                    IHTMLElementCollection aChild = theElement.children as IHTMLElementCollection;

                    int index = 0;

                    foreach (IHTMLElement tBody in aChild)
                    {
                        if (index == 0) { index++; continue; }

                        IHTMLElementCollection aChild1 = tBody.children as IHTMLElementCollection;

                        foreach (IHTMLElement tr in aChild1)
                        {
                            IHTMLElementCollection aChild2 = tr.children as IHTMLElementCollection;

                            int index1 = 0;

                            tweetToReportItem item = new tweetToReportItem(); 

                            foreach (IHTMLElement td in aChild2)
                            {
                                if (index1 == 0) { item.dateTime = td.innerText; index1 = 1; }
                                else if (index1 == 1) { item.userName = td.innerText; index1 = 2; }
                                else if (index1 == 2) { item.screenName = td.innerText; index1 = 3; }
                                else if (index1 == 3) 
                                {
                                    item.tweet = /**Convert.ToDouble(*/td.innerText/**)*/; 
                                    index1 = 4; 
                                }
                                //else if (index1 == 4) { item.a = Convert.ToDouble(td.innerText); index1 = 5; }
                                //else if (index1 == 5) { item.tweet = td.innerText; index1 = 6; }
                                //int i = 0;
                            }//inner foreach...

                            if(item.tweet!=null&&item.tweet.Trim().Length>0)
                            tweetsToReportList.Add(item);

                        }//inner foreach...

                    }//foreach...
            
                    //int i = 0;
                }

                //ObservableCollection<tweetToReportItem> iiiiii= tweetsToReportList;

                if (tweetsToReportList != null&&tweetsToReportList.Count>0)
                {
                    insertAllTweetsInfoIntoDocumentUsingDocX(tweetsToReportList);

                    /**

                    try
                    {
                        connection = new MySqlConnection(MyConnectionString);

                    connection.Open();
 
                        cmd = connection.CreateCommand();
                        cmd.CommandText = "INSERT INTO tweets VALUES(NULL, @v, @a, @username, @datetime, @tweet);";
                        cmd.Parameters.Add("@v", MySqlDbType.Double);
                        cmd.Parameters.Add("@a", MySqlDbType.Double);
                        cmd.Parameters.Add("@username", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@datetime", MySqlDbType.VarChar);
                        cmd.Parameters.Add("@tweet", MySqlDbType.VarChar);

                        foreach (tweetToReportItem v in tweetsToReportList)
                        {
                            try
                            {
                                cmd.Parameters["@v"].Value =  v.v;
                                cmd.Parameters["@a"].Value =  v.a;
                                cmd.Parameters["@username"].Value =  v.userName;
                                cmd.Parameters["@datetime"].Value =  v.dateTime;
                                cmd.Parameters["@tweet"].Value =  v.tweet;
                                cmd.ExecuteNonQuery();
                            }
                            catch { }
                        }//foreach...
                        
                        connection.Close();
                    }//try...
                    catch 
                    {

                    }//catch...

                    */

                    MessageBox.Show("Added All Tweets to report successfully !");

                }//if (tweetsToReportList != null&&tweetsToReportList.Count>0)...

            }//if (wb!=null&&wB.Document != null)...

            /**

            mshtml.IHTMLElementCollection c = ((mshtml.HTMLDocumentClass)(wB.Document)).getElementsByTagName("div");
            foreach (IHTMLElement div in c)
            {
                //IHTMLElement child = null;
                //child.className = "ui-widget";

                if (div.className == "ui-widget")
                {
                    //div.setAttribute("display", "none", 1);
                    div.style.display = "none";
                }
                if (div.id == "control-div")
                {
                    //div.setAttribute("display", "none", 1);
                    div.style.display = "none";
                }
            }
            
            if (wB.Document != null)
            {
                HTMLDocument doc = (HTMLDocument)wB.Document;
                IHTMLElement theElement = doc.getElementById("query-inp");
                theElement.innerText = searchTextBoxText;

                theElement = doc.getElementById("query-btn");
                theElement.click();
            }
            */
           
        }//func...

        private void addActivitiesToReport_Click(object sender, RoutedEventArgs e)
        {
            if (osintGoogleBased[tabDynamicGoogle.SelectedIndex].lastSearch1 == null || osintGoogleBased[tabDynamicGoogle.SelectedIndex].lastSearch1.Trim().Length == 0)
            {
                MessageBox.Show("Please search first!");
                return;
            }//if (tweetsToReportList.Count == 0) ...

                tweetsToReportList = new ObservableCollection<tweetToReportItem>();

            Button but = sender as Button;

            StackPanel sp = but.Parent as StackPanel;

            Border bbr = sp.Parent as Border;

            DockPanel dpp = bbr.Parent as DockPanel;

            WebBrowser wB = dpp.FindName("browse") as WebBrowser;

            if (wB != null && wB.Document != null)
            {
                HTMLDocument doc = (HTMLDocument)wB.Document;
                IHTMLElement theElement = doc.getElementById("tweet-tbl");


                if (theElement != null && theElement.children != null/*&&theElement.children[1].childNodes!=null&&theElement.children[1].childNodes.length>0*/)
                {
                    //theElement.children[1].childNodes.length
                    IHTMLElementCollection aChild = theElement.children as IHTMLElementCollection;

                    int index = 0;

                    foreach (IHTMLElement tBody in aChild)
                    {
                        if (index == 0) { index++; continue; }

                        IHTMLElementCollection aChild1 = tBody.children as IHTMLElementCollection;

                        foreach (IHTMLElement tr in aChild1)
                        {
                            IHTMLElementCollection aChild2 = tr.children as IHTMLElementCollection;

                            int index1 = 0;

                            tweetToReportItem item = new tweetToReportItem();

                            foreach (IHTMLElement td in aChild2)
                            {
                                if (index1 == 0) { item.dateTime = td.innerText; index1 = 1; }
                                else if (index1 == 1) { item.userName = td.innerText; index1 = 2; }
                                else if (index1 == 2) { item.screenName = td.innerText; index1 = 3; }
                                else if (index1 == 3)
                                {
                                    item.tweet = /**Convert.ToDouble(*/td.innerText/**)*/;
                                    index1 = 4;
                                }
                                //else if (index1 == 4) { item.a = Convert.ToDouble(td.innerText); index1 = 5; }
                                //else if (index1 == 5) { item.tweet = td.innerText; index1 = 6; }
                                //int i = 0;
                            }//inner foreach...

                            if (item.tweet != null && item.tweet.Trim().Length > 0)
                                tweetsToReportList.Add(item);

                        }//inner foreach...

                    }//foreach...

                    //int i = 0;
                }

                //ObservableCollection<tweetToReportItem> iiiiii= tweetsToReportList;

                if (tweetsToReportList != null && tweetsToReportList.Count > 0)
                {
                    //insertAllTweetsInfoIntoDocumentUsingDocX(tweetsToReportList);

                    insertAllActivitiesInfoIntoDocumentUsingDocX(tweetsToReportList);

                    MessageBox.Show("Added All Google Activities to report successfully !");

                }//if (tweetsToReportList != null&&tweetsToReportList.Count>0)...

            }//if (wb!=null&&wB.Document != null)...

            /**

            mshtml.IHTMLElementCollection c = ((mshtml.HTMLDocumentClass)(wB.Document)).getElementsByTagName("div");
            foreach (IHTMLElement div in c)
            {
                //IHTMLElement child = null;
                //child.className = "ui-widget";

                if (div.className == "ui-widget")
                {
                    //div.setAttribute("display", "none", 1);
                    div.style.display = "none";
                }
                if (div.id == "control-div")
                {
                    //div.setAttribute("display", "none", 1);
                    div.style.display = "none";
                }
            }
            
            if (wB.Document != null)
            {
                HTMLDocument doc = (HTMLDocument)wB.Document;
                IHTMLElement theElement = doc.getElementById("query-inp");
                theElement.innerText = searchTextBoxText;

                theElement = doc.getElementById("query-btn");
                theElement.click();
            }
            */

        }//func...

        private void addfbPostsToReport_Click(object sender, RoutedEventArgs e)
        {
            if (osintfbBased[tabDynamicfb.SelectedIndex].lastSearch1 == null || osintfbBased[tabDynamicfb.SelectedIndex].lastSearch1.Trim().Length == 0)
            {
                MessageBox.Show("Please search first!");
                return;
            }//if (tweetsToReportList.Count == 0) ...

            tweetsToReportList = new ObservableCollection<tweetToReportItem>();

            Button but = sender as Button;

            StackPanel sp = but.Parent as StackPanel;

            Border bbr = sp.Parent as Border;

            DockPanel dpp = bbr.Parent as DockPanel;

            WebBrowser wB = dpp.FindName("browse") as WebBrowser;

            if (wB != null && wB.Document != null)
            {
                HTMLDocument doc = (HTMLDocument)wB.Document;
                IHTMLElement theElement = doc.getElementById("tweet-tbl");


                if (theElement != null && theElement.children != null/*&&theElement.children[1].childNodes!=null&&theElement.children[1].childNodes.length>0*/)
                {
                    //theElement.children[1].childNodes.length
                    IHTMLElementCollection aChild = theElement.children as IHTMLElementCollection;

                    int index = 0;

                    foreach (IHTMLElement tBody in aChild)
                    {
                        if (index == 0) { index++; continue; }

                        IHTMLElementCollection aChild1 = tBody.children as IHTMLElementCollection;

                        foreach (IHTMLElement tr in aChild1)
                        {
                            IHTMLElementCollection aChild2 = tr.children as IHTMLElementCollection;

                            int index1 = 0;

                            tweetToReportItem item = new tweetToReportItem();

                            foreach (IHTMLElement td in aChild2)
                            {
                                if (index1 == 0) { item.dateTime = td.innerText; index1 = 1; }
                                else if (index1 == 1) { item.userName = td.innerText; index1 = 2; }
                                else if (index1 == 2) { item.screenName = td.innerText; index1 = 3; }
                                else if (index1 == 3)
                                {
                                    item.tweet = /**Convert.ToDouble(*/td.innerText/**)*/;
                                    index1 = 4;
                                }
                                //else if (index1 == 4) { item.a = Convert.ToDouble(td.innerText); index1 = 5; }
                                //else if (index1 == 5) { item.tweet = td.innerText; index1 = 6; }
                                //int i = 0;
                            }//inner foreach...

                            if (item.tweet != null && item.tweet.Trim().Length > 0)
                                tweetsToReportList.Add(item);

                        }//inner foreach...

                    }//foreach...

                    //int i = 0;
                }

                //ObservableCollection<tweetToReportItem> iiiiii= tweetsToReportList;

                if (tweetsToReportList != null && tweetsToReportList.Count > 0)
                {
                    //insertAllTweetsInfoIntoDocumentUsingDocX(tweetsToReportList);

                    insertAllFbPostsInfoIntoDocumentUsingDocX(tweetsToReportList);

                    MessageBox.Show("Added All Facebook posts to report successfully !");

                }//if (tweetsToReportList != null&&tweetsToReportList.Count>0)...

            }//if (wb!=null&&wB.Document != null)...

        }//func...

        private void DisplayTweetsInADoc_ClickWithoutDb(object sender, RoutedEventArgs e) 
        {
            if (osintBased[tabDynamic1.SelectedIndex].lastSearch1 == null || osintBased[tabDynamic1.SelectedIndex].lastSearch1.Trim().Length == 0)
            {
                MessageBox.Show("Please search first!");
                return;
            }//if (tweetsToReportList.Count == 0) ...

            Button but = sender as Button;

            StackPanel sp = but.Parent as StackPanel;

            Border bbr = sp.Parent as Border;

            DockPanel dpp = bbr.Parent as DockPanel;

            WebBrowser wB = dpp.FindName("browse") as WebBrowser;

            if (wB != null && wB.Document != null&&(tweetsToReportList==null||tweetsToReportList.Count==0))
            {
                HTMLDocument doc = (HTMLDocument)wB.Document;
                IHTMLElement theElement = doc.getElementById("tweet-tbl");

                if (theElement != null && theElement.children != null/*&&theElement.children[1].childNodes!=null&&theElement.children[1].childNodes.length>0*/)
                {
                    //theElement.children[1].childNodes.length
                    IHTMLElementCollection aChild = theElement.children as IHTMLElementCollection;

                    int index = 0;

                    foreach (IHTMLElement tBody in aChild)
                    {
                        if (index == 0) { index++; continue; }

                        IHTMLElementCollection aChild1 = tBody.children as IHTMLElementCollection;

                        foreach (IHTMLElement tr in aChild1)
                        {
                            IHTMLElementCollection aChild2 = tr.children as IHTMLElementCollection;

                            int index1 = 0;

                            tweetToReportItem item = new tweetToReportItem();

                            foreach (IHTMLElement td in aChild2)
                            {
                                int i9876 = 9;
                                if (index1 == 0) { item.dateTime = td.innerText; index1 = 1; }
                                else if (index1 == 1) { item.userName = td.innerText; index1 = 2; }
                                else if (index1 == 2) { item.screenName = td.innerText; index1 = 3; }
                                else if (index1 == 3)
                                {
                                    item.tweet = /**Convert.ToDouble(*/td.innerText/*)*/;
                                    index1 = 4;
                                }
                                else if (index1 == 4) 
                                {
                                    item.v = Convert.ToDouble(td.innerText);
                                    index1 = 5; 
                                }
                                else if (index1 == 5) 
                                {
                                    item.a = Convert.ToDouble(td.innerText); index1 = 6; 
                                }
                                //else if (index1 == 4) { item.a = Convert.ToDouble(td.innerText); index1 = 5; }
                                //else if (index1 == 5) { item.tweet = td.innerText; index1 = 6; }
                                //int i = 0;
                            }//inner foreach...

                            if (item.tweet!=null&&item.tweet.Trim().Length>0/**item.v != 0.0f*/)
                                tweetsToReportList.Add(item);

                        }//inner foreach...

                    }//foreach...

                }

            }//if (wb!=null&&wB.Document != null)...

            if (tweetsToReportList != null && tweetsToReportList.Count > 0)
            {
                insertSentimentAnalysisInfoIntoDocumentUsingDocX(tweetsToReportList);

                //**MessageBox.Show("Added All Tweets to report successfully !");

            }//if (tweetsToReportList != null&&tweetsToReportList.Count>0)...

        }//func...

        private void DisplayActivitiesInADoc_ClickWithoutDb(object sender, RoutedEventArgs e)
        {
            if (osintGoogleBased[tabDynamicGoogle.SelectedIndex].lastSearch1 == null || osintGoogleBased[tabDynamicGoogle.SelectedIndex].lastSearch1.Trim().Length == 0)
            {
                MessageBox.Show("Please search first!");
                return;
            }//if (tweetsToReportList.Count == 0) ...

            Button but = sender as Button;

            StackPanel sp = but.Parent as StackPanel;

            Border bbr = sp.Parent as Border;

            DockPanel dpp = bbr.Parent as DockPanel;

            WebBrowser wB = dpp.FindName("browse") as WebBrowser;

            if (wB != null && wB.Document != null && (tweetsToReportList == null || tweetsToReportList.Count == 0))
            {
                HTMLDocument doc = (HTMLDocument)wB.Document;
                IHTMLElement theElement = doc.getElementById("tweet-tbl");

                if (theElement != null && theElement.children != null/*&&theElement.children[1].childNodes!=null&&theElement.children[1].childNodes.length>0*/)
                {
                    //theElement.children[1].childNodes.length
                    IHTMLElementCollection aChild = theElement.children as IHTMLElementCollection;

                    int index = 0;

                    foreach (IHTMLElement tBody in aChild)
                    {
                        if (index == 0) { index++; continue; }

                        IHTMLElementCollection aChild1 = tBody.children as IHTMLElementCollection;

                        foreach (IHTMLElement tr in aChild1)
                        {
                            IHTMLElementCollection aChild2 = tr.children as IHTMLElementCollection;

                            int index1 = 0;

                            tweetToReportItem item = new tweetToReportItem();

                            foreach (IHTMLElement td in aChild2)
                            {
                                int i9876 = 9;
                                if (index1 == 0) { item.dateTime = td.innerText; index1 = 1; }
                                else if (index1 == 1) { item.userName = td.innerText; index1 = 2; }
                                else if (index1 == 2) { item.screenName = td.innerText; index1 = 3; }
                                else if (index1 == 3)
                                {
                                    item.tweet = /**Convert.ToDouble(*/td.innerText/*)*/;
                                    index1 = 4;
                                }
                                else if (index1 == 4)
                                {
                                    item.v = Convert.ToDouble(td.innerText);
                                    index1 = 5;
                                }
                                else if (index1 == 5)
                                {
                                    item.a = Convert.ToDouble(td.innerText); index1 = 6;
                                }
                                //else if (index1 == 4) { item.a = Convert.ToDouble(td.innerText); index1 = 5; }
                                //else if (index1 == 5) { item.tweet = td.innerText; index1 = 6; }
                                //int i = 0;
                            }//inner foreach...

                            if (item.tweet != null && item.tweet.Trim().Length > 0/**item.v != 0.0f*/)
                                tweetsToReportList.Add(item);

                        }//inner foreach...

                    }//foreach...

                }

            }//if (wb!=null&&wB.Document != null)...

            if (tweetsToReportList != null && tweetsToReportList.Count > 0)
            {
                insertGoogleSentimentAnalysisInfoIntoDocumentUsingDocX(tweetsToReportList);

                //**MessageBox.Show("Added All Tweets to report successfully !");

            }//if (tweetsToReportList != null&&tweetsToReportList.Count>0)...

        }//func...

        private void DisplayfbPostsInADoc_ClickWithoutDb(object sender, RoutedEventArgs e)
        {
            if (osintfbBased[tabDynamicfb.SelectedIndex].lastSearch1 == null || osintfbBased[tabDynamicfb.SelectedIndex].lastSearch1.Trim().Length == 0)
            {
                MessageBox.Show("Please search first!");
                return;
            }//if (tweetsToReportList.Count == 0) ...

            Button but = sender as Button;

            StackPanel sp = but.Parent as StackPanel;

            Border bbr = sp.Parent as Border;

            DockPanel dpp = bbr.Parent as DockPanel;

            WebBrowser wB = dpp.FindName("browse") as WebBrowser;

            if (wB != null && wB.Document != null && (tweetsToReportList == null || tweetsToReportList.Count == 0))
            {
                HTMLDocument doc = (HTMLDocument)wB.Document;
                IHTMLElement theElement = doc.getElementById("tweet-tbl");

                if (theElement != null && theElement.children != null/*&&theElement.children[1].childNodes!=null&&theElement.children[1].childNodes.length>0*/)
                {
                    //theElement.children[1].childNodes.length
                    IHTMLElementCollection aChild = theElement.children as IHTMLElementCollection;

                    int index = 0;

                    foreach (IHTMLElement tBody in aChild)
                    {
                        if (index == 0) { index++; continue; }

                        IHTMLElementCollection aChild1 = tBody.children as IHTMLElementCollection;

                        foreach (IHTMLElement tr in aChild1)
                        {
                            IHTMLElementCollection aChild2 = tr.children as IHTMLElementCollection;

                            int index1 = 0;

                            tweetToReportItem item = new tweetToReportItem();

                            foreach (IHTMLElement td in aChild2)
                            {
                                int i9876 = 9;
                                if (index1 == 0) { item.dateTime = td.innerText; index1 = 1; }
                                else if (index1 == 1) { item.userName = td.innerText; index1 = 2; }
                                else if (index1 == 2) { item.screenName = td.innerText; index1 = 3; }
                                else if (index1 == 3)
                                {
                                    item.tweet = /**Convert.ToDouble(*/td.innerText/*)*/;
                                    index1 = 4;
                                }
                                else if (index1 == 4)
                                {
                                    item.v = Convert.ToDouble(td.innerText);
                                    index1 = 5;
                                }
                                else if (index1 == 5)
                                {
                                    item.a = Convert.ToDouble(td.innerText); index1 = 6;
                                }
                                //else if (index1 == 4) { item.a = Convert.ToDouble(td.innerText); index1 = 5; }
                                //else if (index1 == 5) { item.tweet = td.innerText; index1 = 6; }
                                //int i = 0;
                            }//inner foreach...

                            if (item.tweet != null && item.tweet.Trim().Length > 0/**item.v != 0.0f*/)
                                tweetsToReportList.Add(item);

                        }//inner foreach...

                    }//foreach...

                }

            }//if (wb!=null&&wB.Document != null)...

            if (tweetsToReportList != null && tweetsToReportList.Count > 0)
            {
                insertfbPostsSentimentAnalysisInfoIntoDocumentUsingDocX(tweetsToReportList);

                //**MessageBox.Show("Added All Tweets to report successfully !");

            }//if (tweetsToReportList != null&&tweetsToReportList.Count>0)...

        }//func...

        private void DisplayTweetsInADoc_Click(object sender, RoutedEventArgs e)
        {
            if (tweetsToReportList == null || (tweetsToReportList != null && tweetsToReportList.Count == 0)||
                (tweetsToReportList != null && tweetsToReportList.Count == 1 &&tweetsToReportList[0].v==0.0f))
            {
                tweetsToReportList = new ObservableCollection<tweetToReportItem>();

                MessageBox.Show("Nothing to show in Sentiment Analysis Report");
            }//if (tweetsToReportList.Count == 0) ...
            else
            {
                var temp= tweetsToReportList.Where(p => p.v > 5.0f && p.v < 6.0f);

                IEnumerable<tweetToReportItem> coll = temp as IEnumerable<tweetToReportItem>;
                
                tweetsToReportListHappy = new ObservableCollection<tweetToReportItem>(coll);
             /*   
                foreach (tweetToReportItem objTest in col)
                {
                    int i = 0;
                    int j = 0;
                }//foreach...
*/
                int r = 0;
                int hhhh = 0;

            }//else...

        }//func...

        private void searchIconOsint_Click(object sender, RoutedEventArgs e)
        {

            if (osintBased[tabDynamic1.SelectedIndex].mySearch1 == null || osintBased[tabDynamic1.SelectedIndex].mySearch1.Trim().Length == 0)
            {
                MessageBox.Show("Nothing to search for!");
                return;
            }//if (searchTextBoxText == null || searchTextBoxText.Trim().Length == 0) ...

            Image but = sender as Image;
            Grid gr = but.Parent as Grid;
            StackPanel sp = gr.Parent as StackPanel;
            Border bor = sp.Parent as Border;
            DockPanel dockP = bor.Parent as DockPanel;

            WebBrowser wB = dockP.FindName("browse") as WebBrowser;

            if (wB != null && wB.Document != null)
            {
                if (tweetsToReportList != null) tweetsToReportList.Clear();
                else tweetsToReportList = new ObservableCollection<tweetToReportItem>();

                searchTextBoxText = osintBased[tabDynamic1.SelectedIndex].mySearch1;

                osintBased[tabDynamic1.SelectedIndex].Header = searchTextBoxText;
                osintBased[tabDynamic1.SelectedIndex].lastSearch1 = searchTextBoxText;

                HTMLDocument doc = (HTMLDocument)wB.Document;

                if (doc != null)
                {
                    IHTMLElement theElement = doc.getElementById("query-inp");

                    if (theElement != null)
                    {
                        theElement.innerText = searchTextBoxText;

                        theElement = doc.getElementById("query-btn");
                        theElement.click();
                    }//theElement!=null...

                }//if doc!=null...
            }

        }//func...

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {

        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {

        }//end of func...

        public void commentIconClick_previewMouseButtonDownStatic()
        {
            Button tb = buttonSender as Button;

            searchTextBoxText = tb.Tag.ToString();
            searchTextBoxText1 = "commentCount";

            outerListCollections.Clear();

            //if (tb.Text.Equals("0")) return;

            outerImageLoading.Visibility = Visibility.Visible;

            likesOrCommentsLabel.Content = "Comments";

            Thread likeCountInParallelThread = new Thread(getCommentsInParallel);

            likeCountInParallelThread.Start();

        }//end of func...

        private void commentIconClick_previewMouseButtonDown(object sender, RoutedEventArgs e)
        {
            Image tb = sender as Image;

            searchTextBoxText = tb.Tag.ToString();
            searchTextBoxText1 = "commentCount";

            outerListCollections.Clear();

            //if (tb.Text.Equals("0")) return;

            outerImageLoading.Visibility = Visibility.Visible;

            likesOrCommentsLabel.Content = "Comments";

            Thread likeCountInParallelThread = new Thread(getCommentsInParallel);

            likeCountInParallelThread.Start();

        }//end of func...

        public void getCommentsInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                fs.GetPageComments(searchTextBoxText,"50","0");

                JsonArray data = null;

                if (fs.FacebookData != null)
                {
                    data = ((JsonArray)((JsonObject)fs.FacebookData)["data"]);

                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        foreach (JsonObject status in data)
                        {
                            string id = "", name = "",commentorId="", likesCount = "0", createdTime = "", comment = "";

                            if (status.ContainsKey("id")) { id = ((string)status["id"]); }
                            if (status.ContainsKey("like_count")) { likesCount = Convert.ToString(((long)status["like_count"])); }
                            if (status.ContainsKey("created_time")) { createdTime = ((string)status["created_time"]); }
                            if (status.ContainsKey("message")) { comment = ((string)status["message"]); }
                            if (status.ContainsKey("from")) { 
                            JsonObject from=(JsonObject)status["from"];
                                name = ((string)from["name"]);
                                commentorId = ((string)from["id"]); 
                            }

                            outerListCollections.Add(new fbUserListItems()
                            {

                                GridViewColumnName_ID = id,
                                GridViewColumnName_ImageSource = "https://graph.facebook.com/" + commentorId + "/picture?type=large",
                                GridViewColumnName_LabelContent = name,
                                GridViewColumnLocation = "Likes: " + likesCount,
                                GridViewColumn_createdTime = "created:"+createdTime,
                                GridViewColumnTags = comment,
                                GridViewColumn_commentorId = commentorId

                            });
                        }//foreach...

                        outer_div_for_likes_etc_display.Visibility = Visibility.Visible;

                        outerList.ItemsSource = outerListCollections;

                        outerImageLoading.Visibility = Visibility.Collapsed;


                    }));

                }//if fs.facebookdata!=null...

            }//else...

        }

        private void likeIconClick_previewMouseButtonDownStatic()
        {
            Button tb = buttonSender as Button;

            searchTextBoxText = tb.Tag.ToString();
            searchTextBoxText1 = "likeCount";

            outerListCollections.Clear();

            //if (tb.Text.Equals("0")) return;

            outerImageLoading.Visibility = Visibility.Visible;

            likesOrCommentsLabel.Content = "Likes";

            Thread likeCountInParallelThread = new Thread(getlikesInParallel);

            likeCountInParallelThread.Start();

        }//end of func...

        private void likeIconClick_previewMouseButtonDown(object sender, RoutedEventArgs e)
        {
            Image tb = sender as Image;

            searchTextBoxText = tb.Tag.ToString();
            searchTextBoxText1 = "likeCount";

            outerListCollections.Clear();

            //if (tb.Text.Equals("0")) return;

            outerImageLoading.Visibility = Visibility.Visible;

            likesOrCommentsLabel.Content = "Likes";

            Thread likeCountInParallelThread = new Thread(getlikesInParallel);

            likeCountInParallelThread.Start();


        }//end of func...

        public void getlikesInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                fs.GetPageLikedPages(searchTextBoxText,"50","0");

                JsonArray data = null;

                if (fs.FacebookData != null)
                {
                    data = ((JsonArray)((JsonObject)fs.FacebookData)["data"]);

                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {

                    foreach (JsonObject status in data)
                    {
                        string id="",name="";
                        
                        if (status.ContainsKey("id")) { id = ((string)status["id"]); }
                        if (status.ContainsKey("name")) { name = ((string)status["name"]); }

                        outerListCollections.Add(new fbUserListItems() { 
                        
                            GridViewColumnName_ID=id,
                            GridViewColumnName_ImageSource="https://graph.facebook.com/"+id+"/picture?type=large",
                             GridViewColumnName_LabelContent=name,
                        });
                    }//foreach...

                       outer_div_for_likes_etc_display.Visibility = Visibility.Visible;

                       outerList.ItemsSource = outerListCollections;

                       outerImageLoading.Visibility = Visibility.Collapsed;
                       
                   }));

                }//if fs.facebookdata!=null...

            }//else...

        }

        private void plusOneIconClick_previewMouseButtonDown(object sender, RoutedEventArgs e)
        {
            Image tb = sender as Image;

            searchTextBoxText = tb.Tag.ToString();
            searchTextBoxText1 = "plusOneCount";

            outerListCollections.Clear();

            //if (tb.Text.Equals("0")) return;

            outerImageLoading.Visibility = Visibility.Visible;

            likesOrCommentsLabel.Content = "PlusOne's";

            Thread plusOneCountInParallelThread = new Thread(getPlusOnesInParallel);

            plusOneCountInParallelThread.Start();

        }//end of func...

        public void getPlusOnesInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                /**
                GS

                JsonArray data = null;

                if (fs.FacebookData != null)
                {
                    data = ((JsonArray)((JsonObject)fs.FacebookData)["data"]);

                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {

                        foreach (JsonObject status in data)
                        {
                            string id = "", name = "";

                            if (status.ContainsKey("id")) { id = ((string)status["id"]); }
                            if (status.ContainsKey("name")) { name = ((string)status["name"]); }

                            outerListCollections.Add(new fbUserListItems()
                            {

                                GridViewColumnName_ID = id,
                                GridViewColumnName_ImageSource = "https://graph.facebook.com/" + id + "/picture?type=large",
                                GridViewColumnName_LabelContent = name,
                            });
                        }//foreach...


                        outer_div_for_likes_etc_display.Visibility = Visibility.Visible;

                        outerList.ItemsSource = outerListCollections;

                        outerImageLoading.Visibility = Visibility.Collapsed;

                    }));

                }//if fs.facebookdata!=null...
                */
            }//else...

        }

        private void repliesIconClick_previewMouseButtonDown(object sender, RoutedEventArgs e)
        {
            Image tb = sender as Image;

            searchTextBoxText = tb.Tag.ToString();
            searchTextBoxText1 = "repliesCount";

            outerListCollections.Clear();

            //if (tb.Text.Equals("0")) return;

            outerImageLoading.Visibility = Visibility.Visible;

            likesOrCommentsLabel.Content = "Replies";

            Thread plusOneCountInParallelThread = new Thread(getRepliesInParallel);

            plusOneCountInParallelThread.Start();

        }//end of func...

        public void getRepliesInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                /**
                GS

                JsonArray data = null;

                if (fs.FacebookData != null)
                {
                    data = ((JsonArray)((JsonObject)fs.FacebookData)["data"]);

                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {

                        foreach (JsonObject status in data)
                        {
                            string id = "", name = "";

                            if (status.ContainsKey("id")) { id = ((string)status["id"]); }
                            if (status.ContainsKey("name")) { name = ((string)status["name"]); }

                            outerListCollections.Add(new fbUserListItems()
                            {

                                GridViewColumnName_ID = id,
                                GridViewColumnName_ImageSource = "https://graph.facebook.com/" + id + "/picture?type=large",
                                GridViewColumnName_LabelContent = name,
                            });
                        }//foreach...


                        outer_div_for_likes_etc_display.Visibility = Visibility.Visible;

                        outerList.ItemsSource = outerListCollections;

                        outerImageLoading.Visibility = Visibility.Collapsed;

                    }));

                }//if fs.facebookdata!=null...
                */
            }//else...

        }//func...

        private void resharersIconClick_previewMouseButtonDown(object sender, RoutedEventArgs e)
        {
            Image tb = sender as Image;

            searchTextBoxText = tb.Tag.ToString();
            searchTextBoxText1 = "sharesCount";

            outerListCollections.Clear();

            //if (tb.Text.Equals("0")) return;

            outerImageLoading.Visibility = Visibility.Visible;

            likesOrCommentsLabel.Content = "Shares";

            Thread plusOneCountInParallelThread = new Thread(getReSharersInParallel);

            plusOneCountInParallelThread.Start();

        }//end of func...

        public void getReSharersInParallel()
        {
            if (searchTextBoxText.Length == 0) { }
            else
            {
                /**
                GS

                JsonArray data = null;

                if (fs.FacebookData != null)
                {
                    data = ((JsonArray)((JsonObject)fs.FacebookData)["data"]);

                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {

                        foreach (JsonObject status in data)
                        {
                            string id = "", name = "";

                            if (status.ContainsKey("id")) { id = ((string)status["id"]); }
                            if (status.ContainsKey("name")) { name = ((string)status["name"]); }

                            outerListCollections.Add(new fbUserListItems()
                            {

                                GridViewColumnName_ID = id,
                                GridViewColumnName_ImageSource = "https://graph.facebook.com/" + id + "/picture?type=large",
                                GridViewColumnName_LabelContent = name,
                            });
                        }//foreach...


                        outer_div_for_likes_etc_display.Visibility = Visibility.Visible;

                        outerList.ItemsSource = outerListCollections;

                        outerImageLoading.Visibility = Visibility.Collapsed;

                    }));

                }//if fs.facebookdata!=null...
                */
            }//else...

        }//func...

        /**

        private void PersonalityAnalysisTwitterWiz(System.Windows.Forms.WebBrowser Wb, String Url, String AliasName)
        {
            //<button id="query-btn" type="submit" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false"><span class="ui-button-text">Query</span></button>
            //<input id="query-inp" size="50" type="text" data-hasqtip="true" oldtitle="Keywords:" title="" aria-describedby="qtip-0">
            
            wBrowser.Navigate(Url);
            
            //wait for complete load
            while (wBrowser.ReadyState != WebBrowserReadyState.Complete)
            {
                System.Windows.Forms.Application.DoEvents();
            }

            //hiding extra blocks
            HtmlElement HEC = Wb.Document.Body;
            
            HEC.Children[0].Style = "display:none;";
            HEC.Children[2].Style = "display:none;";
            HEC.Children[5].Style = "display:none;";
            HEC.Children[8].Style = "display:none;";
            HEC.Children[11].Style = "display:none;";
            HEC.Children[14].Style = "display:none;";
            HEC.Children[17].Style = "display:none;";
            HEC.Children[19].Style = "display:none;";

            //applying query
            if (Wb.Document != null)
            {
                HtmlElement elem = Wb.Document.GetElementById("query-inp");
                elem.InnerText = AliasName;
                elem = Wb.Document.GetElementById("query-btn");
                elem.InvokeMember("click");
            }
        }

        */

        private void backBlack_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            outerListCollections.Clear();
            outer_div_for_likes_etc_display.Visibility = Visibility.Collapsed;
            outerImageLoading.Visibility = Visibility.Collapsed;
        }//end of func... 

        private void backBlackForIndividualGooglePost_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fbSessionContainer.Visibility = Visibility.Collapsed;
            backBlackForFbSession.Visibility = Visibility.Collapsed;
            individualPostData.Visibility = Visibility.Collapsed;
            backBlackForIndividualGooglePost.Visibility = Visibility.Collapsed;
        }//end of func... 

        private void backBlackForFbSession_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            individualPostData.Visibility = Visibility.Collapsed;
            backBlackForIndividualGooglePost.Visibility = Visibility.Collapsed;
            backBlackForFbSession.Visibility = Visibility.Collapsed;
            fbSessionContainer.Visibility = Visibility.Collapsed;
        }//end of func... 

        private void sA_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Countries[tabDynamic.SelectedIndex].mySearch.Trim().Length == 0) 
            {
                MessageBox.Show("Please search first...");
                return; 
            }

            //outerImageLoadingForAnyOne.Visibility = Visibility.Visible;

            aTabItem plusOne = new aTabItem() { Header = Countries[tabDynamic.SelectedIndex].mySearch };
            plusOne.mySearch = "";
            plusOne.type = "sA";
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = false;
            plusOne.twitterTweetListDivVisbility = false;
            plusOne.headerCloseIconVisibility = true;
            plusOne.sADivVisbility = true;

            plusOne.sourceCodeSenti = @"http://localhost:80/TwitterWizLocal/";//**@"http://www.csc.ncsu.edu/faculty/healey/tweet_viz/tweet_app/";

            plusOne.twitterUserProfileFollowersListVisbility = false;
            plusOne.twitterUserProfileFollowingListVisbility = false;
            plusOne.twitterUserProfileTweetListVisbility = false;

            searchTextBoxText = Countries[tabDynamic.SelectedIndex].mySearch.Trim();

            Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

            tabDynamic.SelectedItem = plusOne;

        }//end of func... 

        private void sA1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (Countries[tabDynamic.SelectedIndex].mySearch.Trim().Length == 0) return;

            //outerImageLoadingForAnyOne.Visibility = Visibility.Visible;

            aTabItem plusOne = new aTabItem() { Header = "Analysis" };
            plusOne.mySearch = "";
            plusOne.type = "sA1";
            plusOne.fbUserListCollections.Clear();
            plusOne.twitterUserListCollections.Clear();
            plusOne.tab_number = tabDynamic.Items.Count.ToString();
            plusOne.searchUserDivVisbility = false;
            plusOne.twitterUserDivVisbility = false;
            plusOne.twitterTweetListDivVisbility = false;
            plusOne.headerCloseIconVisibility = true;
            plusOne.sADivVisbility = true;

            //string curDir = Directory.GetCurrentDirectory();
            //string iii = curDir + @"\work\index.html";

            plusOne.sourceCodeSenti = @"http://localhost:80/analysis/";//iii;//@".\work\index.html";

            plusOne.twitterUserProfileFollowersListVisbility = false;
            plusOne.twitterUserProfileFollowingListVisbility = false;
            plusOne.twitterUserProfileTweetListVisbility = false;

            searchTextBoxText = Countries[tabDynamic.SelectedIndex].mySearch.Trim();

            Countries.Insert(tabDynamic.Items.Count - 1, plusOne);

            tabDynamic.SelectedItem = plusOne;
        }//end of func... 

        void wb_LoadCompleted(object sender, NavigationEventArgs e)
        {
           // wb.LoadCompleted -= wb_LoadCompleted; //REMOVE THE OLD EVENT METHOD BINDING
           // wb.LoadCompleted += wb_LoadCompleted2; //BIND TO A NEW METHOD FOR THE EVENT                     
        }

        void wb_LoadCompleted2(object sender, NavigationEventArgs e)
        {
            WebBrowser wB = sender as WebBrowser;

            mshtml.IHTMLElementCollection c = ((mshtml.HTMLDocumentClass)(wB.Document)).getElementsByTagName("div");
            foreach (IHTMLElement div in c)
            {
                //IHTMLElement child = null;
                //child.className = "ui-widget";

                if (div.className == "ui-widget")
                {
                    //div.setAttribute("display", "none", 1);
                    //**div.style.display = "none";
                }
                if (div.id == "control-div")
                {
                    //div.setAttribute("display", "none", 1);
                    div.style.display = "none";
                }
            }//foreach...
            
            /**
            if (wB.Document != null)
            {
                HTMLDocument doc = (HTMLDocument)wB.Document;
                IHTMLElement theElement = doc.getElementById("query-inp");
                theElement.innerText = searchTextBoxText;

                theElement = doc.getElementById("query-btn");
                theElement.click();
            }
            */
        }//func...

        private void getFbSessionId_Click(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void saveFbSessionId_Click(object sender, RoutedEventArgs e)
        {
            fbSessionContainer.Visibility = Visibility.Collapsed;
            backBlackForFbSession.Visibility = Visibility.Collapsed;

            if (fbSessionIdTextBox.Text.Trim().Length>0)
            fbSessionId = fbSessionIdTextBox.Text.Trim();

            if (fbSessionId.Length == 0) return;

            StreamWriter sW = new System.IO.StreamWriter("fbSessionId.txt");
            sW.Write(fbSessionId);

            sW.Close();

            fs = new FacebookSearch(fbSessionId);
            fs1 = new FacebookSearch(fbSessionId);
            fs2 = new FacebookSearch(fbSessionId);
        }

        private void openFbSessionId_Click(object sender, RoutedEventArgs e)
        {
            parentTab.SelectedIndex = 0;

            fbSessionContainer.Visibility = Visibility.Visible;
            backBlackForFbSession.Visibility = Visibility.Visible;

            if (fbSessionId.Equals("abc") == false)
                fbSessionIdTextBox.Text = fbSessionId;
            else
                fbSessionIdTextBox.Text = "";

            if (fbSessionId.Length == 0) return;

            /**fs = new FacebookSearch(fbSessionId);
            fs1 = new FacebookSearch(fbSessionId);
            fs1 = new FacebookSearch(fbSessionId);*/
        }

        private void openReportingFileDialog_Click(object sender, RoutedEventArgs e)
        {
            // Initialize an OpenFileDialog 
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter and RestoreDirectory 
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Word documents(*.doc;*.docx)|*.doc;*.docx";

            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                if (openFileDialog.FileName.Length > 0)
                {
                    reportFileLocation = openFileDialog.FileName;

                    try 
                    {
                    StreamWriter sW1 = new System.IO.StreamWriter("reportingFileLocation.txt");
                    sW1.Write(reportFileLocation);

                    sW1.Close();

                    }//try...
                    catch { }
                }
            }

        }

        private void crossIconOsintfbSearch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            osintfbBased[tabDynamicfb.SelectedIndex].mySearch1 = "";
        }//func...

        private void crossIconOsintGoogleSearch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            osintGoogleBased[tabDynamicGoogle.SelectedIndex].mySearch1 = "";
        }//func...


        private void crossIconOsintSearch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            osintBased[tabDynamic1.SelectedIndex].mySearch1 = "";
        }//func...

        private void crossIconSimpleSearch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Countries[tabDynamic.SelectedIndex].mySearch = "";
        }

        #region DocumentReporting
        //Create document method
        private void insertTweetIntoDocument1(String userName, String ScreeName, String desc, String createdTime, String profileImgUrl)
        {
            try
            {
                #region Novacode.DocX Section
               /** String strFileName = System.IO.Directory.GetCurrentDirectory() + "\\TestReport" + DateTime.Now.Ticks + ".docx";
                AddContentsToReport(String.Empty, strFileName, userName, ScreeName, desc, createdTime, tweetId);
                Process.Start(strFileName);*/
                #endregion

                #region InterOp Method
                /**
                using (Novacode.DocX doc = Novacode.DocX.Create(reportFileLocation))
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"hussein.png");

                        double xScale = 1;
                        double yScale = 1;

                        double maxWidthInches = 6.1; // Max width to fit on a page
                        double maxHeightInches = 8.66; // Max height to fit on a page

                        // Normalise the Horizontal and Vertical scale for different resolutions
                        double hScale = ((double)96 / myImg.HorizontalResolution);
                        double vScale = ((double)96 / myImg.VerticalResolution);

                        // Scaling required to fit in x direction
                        double imageWidthInches = myImg.Width / myImg.HorizontalResolution; // in inches using DPI
                        if (imageWidthInches > maxWidthInches)
                            xScale = maxWidthInches / imageWidthInches * hScale;

                        // Scaling required to fit in y direction
                        double imageHeightInches = myImg.Height / myImg.VerticalResolution;
                        if (imageHeightInches > maxHeightInches)
                            yScale = maxHeightInches / imageHeightInches * vScale;

                        double finalScale = Math.Min(xScale, yScale); // Use the smallest of the two scales to ensure the picture will fit in both directions

                        myImg.Save(ms, myImg.RawFormat); // Save your picture in a memory stream.
                        ms.Seek(0, SeekOrigin.Begin);

                        Novacode.Image img = doc.AddImage(ms); // Create image.
                        Novacode.Paragraph p = doc.InsertParagraph();
                        Novacode.Picture pic = img.CreatePicture(); // Create picture.

                        //Apply final scale to height & width
                        double width = Math.Round((double)myImg.Width * finalScale);
                        double height = Math.Round((double)myImg.Height * finalScale);

                        pic.Width = (int)(width);
                        pic.Height = (int)(height);

                        p.InsertPicture(pic, 0);//.InsertPicture(pic); // Insert picture into paragraph.
                    }
                }

                */

                #region loading and saving image...

                string imageUrl = profileImgUrl;
                string saveLocation = @"dp.jpg";

                byte[] imageBytes;
                HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
                WebResponse imageResponse = imageRequest.GetResponse();

                Stream responseStream = imageResponse.GetResponseStream();

                using (BinaryReader br = new BinaryReader(responseStream))
                {
                    imageBytes = br.ReadBytes(500000);
                    br.Close();
                }
                responseStream.Close();
                imageResponse.Close();

                FileStream fs = new FileStream(saveLocation, FileMode.Create,FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                try
                {
                    bw.Write(imageBytes);
                }
                finally
                {
                    fs.Close();
                    fs.Dispose();
                    bw.Dispose();
                    bw.Close();
                }
            #endregion

                using (Novacode.DocX doc = Novacode.DocX.Load(reportFileLocation))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"dp.jpg");

                        myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                        ms.Seek(0, SeekOrigin.Begin);

                        Novacode.Image img = doc.AddImage(ms); // Create image.

                        Novacode.Paragraph p = doc.InsertParagraph(userName + " " + ScreeName);

                        p.Bold(); p.Color(System.Drawing.Color.FromArgb(64, 153, 255));
                        p.FontSize(15.0f);


                        Novacode.Picture pic1 = img.CreatePicture();     // Create picture.
                        pic1.SetPictureShape(Novacode.RectangleShapes.rect); // Set picture shape (if needed)

                        p.InsertPicture(pic1, 0); // Insert picture into paragraph.
                        //Novacode.Formatting for112=new Novacode.Formatting();

                        //p.Alignment = Novacode.Alignment.center;

                        //Novacode.Paragraph p1 = doc.InsertParagraph(userName + " " + ScreeName ,false);
                        
                        Novacode.Paragraph p2 = doc.InsertParagraph("\n"+desc+"\n", false);

                        Novacode.Paragraph p3 = doc.InsertParagraph(createdTime + "\n", false);

                        //p3.Alignment = Novacode.Alignment.center;

                        Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);

                        p4.Alignment = Novacode.Alignment.center;


                        ms.Close();

                        doc.Save();
                    }
                }


                /**
                //Create an instance for word app
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                //winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                winword.Visible = false;

                //Create a missing variable for missing value
                object missing = System.Reflection.Missing.Value;

                //Create a new document
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Open(reportFileLocation, missing, ref missing, ref missing, ref missing);

                //adding text to document
                //document.Content.SetRange(0, 0);
                //document.Content.Text = document.Content.Text + "";//userName + " "+ScreeName+" "+ "     "+createdTime+ Environment.NewLine;

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                object styleHeading1 = "Heading 1";
                para1.Range.set_Style(ref styleHeading1);
                para1.Range.Text = userName + " " + ScreeName + " " + "     " + createdTime;//"Para 1 text";
                para1.Range.InsertParagraphAfter();

                office.Range rng1 = para1.Range;
                rng1.ParagraphFormat.Alignment = office.WdParagraphAlignment.wdAlignParagraphLeft;

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                //para2.Range.set_Style(ref styleHeading1);
                
                para2.Range.Text = desc;//"Para 1 text";
                para2.Range.InsertParagraphAfter();

                office.Range rng = para2.Range;
                rng.ParagraphFormat.Alignment = office.WdParagraphAlignment.wdAlignParagraphCenter;

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para3 = document.Content.Paragraphs.Add(ref missing);
                para3.Range.Text = "--------------------------------------------------" + Environment.NewLine;//"Para 1 text";

                para3.Range.InsertParagraphAfter();

                office.Range rng2 = para1.Range;
                rng2.ParagraphFormat.Alignment = office.WdParagraphAlignment.wdAlignParagraphLeft;

                //para3.Range.ParagraphFormat.Alignment = office.WdParagraphAlignment.wdAlignParagraphCenter;
                //para3.Alignment = office.WdParagraphAlignment.wdAlignParagraphCenter;
                //para3.Format.Alignment = office.WdParagraphAlignment.wdAlignParagraphCenter;
                
                String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Save the document
                object filename = reportFileLocation;//path + @"\Report.docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);

                //winword.ActiveDocument.Close();

                winword = null;*/
                 MessageBox.Show("Added to report successfully !");
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//func...

        //Create document method
        private void insertTweetIntoDocument(String userName, String ScreeName, String desc, String createdTime, String tweetId)
        {
            try
            {
                #region Novacode.DocX Section
                /** String strFileName = System.IO.Directory.GetCurrentDirectory() + "\\TestReport" + DateTime.Now.Ticks + ".docx";
                AddContentsToReport(String.Empty, strFileName, userName, ScreeName, desc, createdTime, tweetId);
                Process.Start(strFileName);*/
                #endregion

                #region InterOp Method
                /**
                using (Novacode.DocX doc = Novacode.DocX.Create(reportFileLocation))
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"hussein.png");

                        double xScale = 1;
                        double yScale = 1;

                        double maxWidthInches = 6.1; // Max width to fit on a page
                        double maxHeightInches = 8.66; // Max height to fit on a page

                        // Normalise the Horizontal and Vertical scale for different resolutions
                        double hScale = ((double)96 / myImg.HorizontalResolution);
                        double vScale = ((double)96 / myImg.VerticalResolution);

                        // Scaling required to fit in x direction
                        double imageWidthInches = myImg.Width / myImg.HorizontalResolution; // in inches using DPI
                        if (imageWidthInches > maxWidthInches)
                            xScale = maxWidthInches / imageWidthInches * hScale;

                        // Scaling required to fit in y direction
                        double imageHeightInches = myImg.Height / myImg.VerticalResolution;
                        if (imageHeightInches > maxHeightInches)
                            yScale = maxHeightInches / imageHeightInches * vScale;

                        double finalScale = Math.Min(xScale, yScale); // Use the smallest of the two scales to ensure the picture will fit in both directions

                        myImg.Save(ms, myImg.RawFormat); // Save your picture in a memory stream.
                        ms.Seek(0, SeekOrigin.Begin);

                        Novacode.Image img = doc.AddImage(ms); // Create image.
                        Novacode.Paragraph p = doc.InsertParagraph();
                        Novacode.Picture pic = img.CreatePicture(); // Create picture.

                        //Apply final scale to height & width
                        double width = Math.Round((double)myImg.Width * finalScale);
                        double height = Math.Round((double)myImg.Height * finalScale);

                        pic.Width = (int)(width);
                        pic.Height = (int)(height);

                        p.InsertPicture(pic, 0);//.InsertPicture(pic); // Insert picture into paragraph.
                    }
                }

                */

                #region loading and saving image...

                /*string imageUrl = profileImgUrl;
                string saveLocation = @"dp.jpg";

                byte[] imageBytes;
                HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
                WebResponse imageResponse = imageRequest.GetResponse();

                Stream responseStream = imageResponse.GetResponseStream();

                using (BinaryReader br = new BinaryReader(responseStream))
                {
                    imageBytes = br.ReadBytes(500000);
                    br.Close();
                }
                responseStream.Close();
                imageResponse.Close();

                FileStream fs = new FileStream(saveLocation, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                try
                {
                    bw.Write(imageBytes);
                }
                finally
                {
                    fs.Close();
                    fs.Dispose();
                    bw.Dispose();
                    bw.Close();
                }*/
                #endregion

                using (Novacode.DocX doc = Novacode.DocX.Load(reportFileLocation))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"dp.jpg");

                        //myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                        //ms.Seek(0, SeekOrigin.Begin);

                        //Novacode.Image img = doc.AddImage(ms); // Create image.

                        Novacode.Paragraph p = doc.InsertParagraph(userName + " " + ScreeName);

                        p.Bold(); p.Color(System.Drawing.Color.FromArgb(64, 153, 255));
                        p.FontSize(15.0f);

                        /*

                        Novacode.Picture pic1 = img.CreatePicture();     // Create picture.
                        pic1.SetPictureShape(Novacode.RectangleShapes.rect); // Set picture shape (if needed)

                        p.InsertPicture(pic1, 0); // Insert picture into paragraph.
                        //Novacode.Formatting for112=new Novacode.Formatting();

                        */

                        //p.Alignment = Novacode.Alignment.center;

                        //Novacode.Paragraph p1 = doc.InsertParagraph(userName + " " + ScreeName ,false);

                        Novacode.Paragraph p2 = doc.InsertParagraph("\n" + desc + "\n", false);

                        Novacode.Paragraph p3 = doc.InsertParagraph(createdTime + "\n", false);

                        //p3.Alignment = Novacode.Alignment.center;

                        Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);

                        p4.Alignment = Novacode.Alignment.center;


                        ms.Close();

                        doc.Save();
                    }//inner using...
                }//outer using...


                /**
                //Create an instance for word app
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                //winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                winword.Visible = false;

                //Create a missing variable for missing value
                object missing = System.Reflection.Missing.Value;

                //Create a new document
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Open(reportFileLocation, missing, ref missing, ref missing, ref missing);

                //adding text to document
                //document.Content.SetRange(0, 0);
                //document.Content.Text = document.Content.Text + "";//userName + " "+ScreeName+" "+ "     "+createdTime+ Environment.NewLine;

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                object styleHeading1 = "Heading 1";
                para1.Range.set_Style(ref styleHeading1);
                para1.Range.Text = userName + " " + ScreeName + " " + "     " + createdTime;//"Para 1 text";
                para1.Range.InsertParagraphAfter();

                office.Range rng1 = para1.Range;
                rng1.ParagraphFormat.Alignment = office.WdParagraphAlignment.wdAlignParagraphLeft;

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                //para2.Range.set_Style(ref styleHeading1);
                
                para2.Range.Text = desc;//"Para 1 text";
                para2.Range.InsertParagraphAfter();

                office.Range rng = para2.Range;
                rng.ParagraphFormat.Alignment = office.WdParagraphAlignment.wdAlignParagraphCenter;

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para3 = document.Content.Paragraphs.Add(ref missing);
                para3.Range.Text = "--------------------------------------------------" + Environment.NewLine;//"Para 1 text";

                para3.Range.InsertParagraphAfter();

                office.Range rng2 = para1.Range;
                rng2.ParagraphFormat.Alignment = office.WdParagraphAlignment.wdAlignParagraphLeft;

                //para3.Range.ParagraphFormat.Alignment = office.WdParagraphAlignment.wdAlignParagraphCenter;
                //para3.Alignment = office.WdParagraphAlignment.wdAlignParagraphCenter;
                //para3.Format.Alignment = office.WdParagraphAlignment.wdAlignParagraphCenter;
                
                String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Save the document
                object filename = reportFileLocation;//path + @"\Report.docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);

                //winword.ActiveDocument.Close();

                winword = null;*/
                MessageBox.Show("Added to report successfully !");
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//func...

        //Create document method
        private void insertTweetIntoDocumentWithoutDocX(String userName, String ScreeName, String desc, String createdTime, String tweetId)
        {
            try
            {
                #region Novacode.DocX Section
                /** String strFileName = System.IO.Directory.GetCurrentDirectory() + "\\TestReport" + DateTime.Now.Ticks + ".docx";
                AddContentsToReport(String.Empty, strFileName, userName, ScreeName, desc, createdTime, tweetId);
                Process.Start(strFileName);*/
                #endregion

                #region InterOp Method
                //Create an instance for word app
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                //winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                winword.Visible = false;

                //Create a missing variable for missing value
                object missing = System.Reflection.Missing.Value;


                using (Novacode.DocX doc = Novacode.DocX.Load(reportFileLocation))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebClient wc = new WebClient();
                        byte[] bytes = wc.DownloadData("http://isaw.nyu.edu/exhibitions/aesthetics/images/facebooklikeicon.png");

                        MemoryStream ms1 = new MemoryStream(bytes);
                        //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                        System.Drawing.Image myImg = System.Drawing.Image.FromStream(ms1);//System.Drawing.Image.FromFile(@"https://graph.facebook.com/1099261681/picture");

                        ms1.Close();

                        myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                        ms.Seek(0, SeekOrigin.Begin);

                        Novacode.Image img = doc.AddImage(ms); // Create image.

                        //Paragraph p = doc.InsertParagraph("Hello", false);
                        Novacode.Paragraph p = doc.InsertParagraph("");
                        Novacode.Picture pic1 = img.CreatePicture();     // Create picture.
                        pic1.SetPictureShape(Novacode.BasicShapes.cube); // Set picture shape (if needed)

                        p.InsertPicture(pic1, 0); // Insert picture into paragraph.

                        doc.Save();
                    }
                }

                //Create a new document
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Open(reportFileLocation, missing, ref missing, ref missing, ref missing);

                //adding text to document
                //document.Content.SetRange(0, 0);
                //document.Content.Text = document.Content.Text + "";//userName + " "+ScreeName+" "+ "     "+createdTime+ Environment.NewLine;

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                object styleHeading1 = "Heading 1";
                para1.Range.set_Style(ref styleHeading1);
                para1.Range.Text = userName + " " + ScreeName + " " + "     " + createdTime /*+ Environment.NewLine*/;//"Para 1 text";
                para1.Range.InsertParagraphAfter();

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                para2.Range.set_Style(ref styleHeading1);
                para2.Range.Text = desc + Environment.NewLine;//"Para 1 text";
                para2.Range.InsertParagraphAfter();

                String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Save the document
                object filename = reportFileLocation;//path + @"\Report.docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);

                //winword.ActiveDocument.Close();

                winword = null;
                MessageBox.Show("Added to report successfully !");
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//func...

        #region Tahir Report Section
        private bool AddContentsToReport(String strReport2Modify, String strReport2Create, params String[] p)
        {
            try
            {
                using (Novacode.DocX docx = Novacode.DocX.Load(@".\Tweeter_Tweet.docx"))
                {
                    String strDirPath = (strReport2Create.Length > 0) ? System.IO.Path.GetDirectoryName(strReport2Create) : System.IO.Path.GetDirectoryName(strReport2Modify);
                    if (!System.IO.Directory.Exists(strDirPath))
                        System.IO.Directory.CreateDirectory(strDirPath);

                    FillContentsInTemplate(docx, strDirPath, p);
                    if (strReport2Modify == null || strReport2Modify.Length < 1)
                    {
                        docx.AddProtection(Novacode.EditRestrictions.readOnly);
                        docx.SaveAs(strReport2Create);
                    }
                    else
                    {
                        #region Document Merging Test
                        Exception ex = null;
                        using (Novacode.DocX docx2 = Novacode.DocX.Load(strReport2Modify))
                        {
                            docx2.RemoveProtection();
                            try
                            {
                                docx2.Paragraphs[docx2.Paragraphs.Count - 1].InsertPageBreakAfterSelf();
                                docx2.InsertDocument(docx);
                            }
                            catch (Exception e) { ex = e; }
                            finally
                            {
                                docx2.AddProtection(Novacode.EditRestrictions.readOnly);
                                docx2.Save();
                            }

                            if (ex != null) { throw ex; }
                        }
                        #endregion
                    }
                }
            }
            catch { return false; }

            return true;
        }

        private void FillContentsInTemplate(Novacode.DocX docx, String strRptDir, params String[] p)
        {
            Novacode.Image img = null;
            Novacode.Hyperlink link = null;

            //if (!String.IsNullOrEmpty(crc.ImageFilePath))
            //{
            //    img = docx.AddImage(crc.ImageFilePath);
            //    docx.Tables[0].Rows[0].Cells[0].Paragraphs[0].AppendPicture(img.CreatePicture(70, 70));
            //}

            //if (!String.IsNullOrEmpty(crc.WebURL))
            //{
            //    link = docx.AddHyperlink(crc.Title, new Uri(crc.WebURL));
            //    docx.Tables[0].Rows[0].Cells[1].Paragraphs[0].AppendHyperlink(link).Color(Color.Blue).UnderlineStyle(Novacode.UnderlineStyle.singleLine).Bold().AppendLine();
            //}
            //else
            //    docx.Tables[0].Rows[0].Cells[1].Paragraphs[0].Append(crc.Title);

            docx.Tables[0].Rows[0].Cells[1].Paragraphs[0].Append(p[0] + p[1]);
            docx.Tables[0].Rows[0].Cells[2].Paragraphs[0].Append(p[3]);
            docx.Tables[0].Rows[1].Cells[1].Paragraphs[0].Append(p[2]);
        }
        #endregion

        //Create document method
        private void insertAllTweetsIntoDocument(ObservableCollection<twitterUserTweetListItems> twitterUserTweetListCollections)
        {
            try
            {
                //Create an instance for word app
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                //winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                winword.Visible = false;

                //Create a missing variable for missing value
                object missing = System.Reflection.Missing.Value;

                //Create a new document
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Open(reportFileLocation, missing, ref missing, ref missing, ref missing);

                //adding text to document
                //document.Content.SetRange(0, 0);
                //document.Content.Text = document.Content.Text + "";//userName + " "+ScreeName+" "+ "     "+createdTime+ Environment.NewLine;

                int length= twitterUserTweetListCollections.Count;

                for (int i = 0; i < length; i++)
                {

                    //Add paragraph with Heading 1 style
                    Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                    object styleHeading1 = "Heading 1";
                    para1.Range.set_Style(ref styleHeading1);
                    para1.Range.Text = twitterUserTweetListCollections[i].twitterUserTweetListItems_name + " " + twitterUserTweetListCollections[i].twitterUserTweetListItems_screenName + " "
                        + "     " + twitterUserTweetListCollections[i].twitterUserTweetListItems_dateTime /*+ Environment.NewLine*/;//"Para 1 text";
                    para1.Range.InsertParagraphAfter();

                    //Add paragraph with Heading 1 style
                    Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                    //para2.Range.set_Style(ref styleHeading1);
                    para2.Range.Text = twitterUserTweetListCollections[i].twitterUserTweetListItems_desc + Environment.NewLine;//"Para 1 text";
                    para2.Range.InsertParagraphAfter();

                }//for loop...

                String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Save the document
                object filename = path + @"\Report.docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);

                //winword.ActiveDocument.Close();

                winword = null;
                 MessageBox.Show("Added All Tweets to report successfully !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//func...

        //Create document method
        private void insertAllTweetsIntoDocumentWithDocX(ObservableCollection<twitterUserTweetListItems> twitterUserTweetListCollections)
        {

            try{

            using (Novacode.DocX doc = Novacode.DocX.Load(reportFileLocation))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    //System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"dp.jpg");

                    //myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                    //ms.Seek(0, SeekOrigin.Begin);

                    //Novacode.Image img = doc.AddImage(ms); // Create image.

                    int length = twitterUserTweetListCollections.Count;

                    for (int i = 0; i < length; i++)
                    {
                        Novacode.Paragraph p = doc.InsertParagraph(twitterUserTweetListCollections[i].twitterUserTweetListItems_name + " " + twitterUserTweetListCollections[i].twitterUserTweetListItems_screenName);

                        p.Bold(); p.Color(System.Drawing.Color.FromArgb(64, 153, 255));
                        p.FontSize(15.0f);

                        /*

                        Novacode.Picture pic1 = img.CreatePicture();     // Create picture.
                        pic1.SetPictureShape(Novacode.RectangleShapes.rect); // Set picture shape (if needed)

                        p.InsertPicture(pic1, 0); // Insert picture into paragraph.
                        //Novacode.Formatting for112=new Novacode.Formatting();

                        */

                        //p.Alignment = Novacode.Alignment.center;

                        //Novacode.Paragraph p1 = doc.InsertParagraph(userName + " " + ScreeName ,false);

                        Novacode.Paragraph p2 = doc.InsertParagraph("\n" + twitterUserTweetListCollections[i].twitterUserTweetListItems_desc + "\n", false);

                        Novacode.Paragraph p3 = doc.InsertParagraph(twitterUserTweetListCollections[i].twitterUserTweetListItems_dateTime + "\n", false);

                        //p3.Alignment = Novacode.Alignment.center;

                        Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);

                        p4.Alignment = Novacode.Alignment.center;

                        ms.Close();

                        doc.Save();
                    
                    }//for loop...

                    MessageBox.Show("Added All Tweets To Report Successfully");

                }//inner using...
            }//outer using...

            }  catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//func...

        //Create document method
        private void insertTwitterUserProfileInfoIntoDocument(String twitterProfileUserTweetCount, String twitterProfileUserName, String twitterProfileUserId,
            String twitterProfileUserFollowingCount, String twitterProfileUserFollowerCount,String twitterProfileUserDp,String twitterProfileUserCityCountry,
            String twitterProfileUserAge, String twitterProfileUserAboutMe, String twitterProfileUserUrl)
        {

            try
            {
                //Create an instance for word app
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                //winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                winword.Visible = false;

                //Create a missing variable for missing value
                object missing = System.Reflection.Missing.Value;

                //Create a new document
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Open(reportFileLocation, missing, ref missing, ref missing, ref missing);

                //adding text to document
                //document.Content.SetRange(0, 0);
                //document.Content.Text = document.Content.Text + "";//userName + " "+ScreeName+" "+ "     "+createdTime+ Environment.NewLine;

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                object styleHeading1 = "Heading 1";
                para1.Range.set_Style(ref styleHeading1);
                para1.Range.Text = twitterProfileUserName + "  (" + twitterProfileUserAge + ")   id: " +twitterProfileUserId /*+ Environment.NewLine*/;//"Para 1 text";
                para1.Range.InsertParagraphAfter();

                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                //para2.Range.set_Style(ref styleHeading1);
                para2.Range.Text = "Location: "+twitterProfileUserCityCountry + Environment.NewLine+
                    "Followers Count: "+twitterProfileUserFollowerCount+Environment.NewLine+
                    "Following Count: "+twitterProfileUserFollowingCount+Environment.NewLine+
                    "Retweet Count: "+twitterProfileUserTweetCount + Environment.NewLine+
                    "Profile Url: " + twitterProfileUserUrl+Environment.NewLine;
                para2.Range.InsertParagraphAfter();

                String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Save the document
                object filename = path + @"\Report.docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);

                //winword.ActiveDocument.Close();

                winword = null;
                 MessageBox.Show("Added User Profile to report successfully !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Create document method
        private void insertTwitterUserProfileInfoIntoDocumentUsingDocX(String twitterProfileUserTweetCount, String twitterProfileUserName, String twitterProfileUserId,
            String twitterProfileUserFollowingCount, String twitterProfileUserFollowerCount, String twitterProfileUserDp, String twitterProfileUserCityCountry,
            String twitterProfileUserAge, String twitterProfileUserAboutMe, String twitterProfileUserUrl)
        {

            try
            {

                using (Novacode.DocX doc = Novacode.DocX.Load(reportFileLocation))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"dp.jpg");

                        //myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                        //ms.Seek(0, SeekOrigin.Begin);

                        //Novacode.Image img = doc.AddImage(ms); // Create image.

                        Novacode.Paragraph pp = doc.InsertParagraph("Twitter User Profile Info\n");

                        pp.Bold(); pp.Color(System.Drawing.Color.Black);
                        pp.FontSize(15.0f);
                        pp.Alignment = Novacode.Alignment.center;


                        Novacode.Paragraph p = doc.InsertParagraph(twitterProfileUserName + "  (" + twitterProfileUserAge + ")   id: " + twitterProfileUserId);

                        p.Bold(); p.Color(System.Drawing.Color.FromArgb(64, 153, 255));
                        p.FontSize(15.0f);

                        /*

                        Novacode.Picture pic1 = img.CreatePicture();     // Create picture.
                        pic1.SetPictureShape(Novacode.RectangleShapes.rect); // Set picture shape (if needed)

                        p.InsertPicture(pic1, 0); // Insert picture into paragraph.
                        //Novacode.Formatting for112=new Novacode.Formatting();

                        */

                        //p.Alignment = Novacode.Alignment.center;

                        //Novacode.Paragraph p1 = doc.InsertParagraph(userName + " " + ScreeName ,false);

                        Novacode.Paragraph p2 = doc.InsertParagraph("\n" + "Location: " + twitterProfileUserCityCountry + Environment.NewLine +
                        "Followers Count: " + twitterProfileUserFollowerCount + Environment.NewLine +
                        "Following Count: " + twitterProfileUserFollowingCount + Environment.NewLine +
                        "Retweet Count: " + twitterProfileUserTweetCount + Environment.NewLine +
                        "Profile Url: " + twitterProfileUserUrl + Environment.NewLine, false);

                        //Novacode.Paragraph p3 = doc.InsertParagraph(createdTime + "\n", false);

                        //p3.Alignment = Novacode.Alignment.center;

                        Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);

                        p4.Alignment = Novacode.Alignment.center;


                        ms.Close();

                        doc.Save();

                        MessageBox.Show("Added User Profile to report successfully !");
                    }//inner using...
                }//outer using....

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//func...

        //Create document method
        private void insertfbPageProfileInfoIntoDocumentUsingDocX(String fbPageTabInfo_LabelContent,String fbPageTabInfo_userName, String fbPageTabInfo_ID, String fbPageTabInfo_Category,
            String fbPageTabInfo_location, String fbPageTabInfo_about, String fbPageTabInfo_awards, String fbPageTabInfo_canPost,
            String fbPageTabInfo_checkIns, String fbPageTabInfo_coverSource, String fbPageTabInfo_description,
            String fbPageTabInfo_hasAddedApp,String fbPageTabInfo_isCommunityPage,String fbPageTabInfo_isPublished,
            String fbPageTabInfo_link,String fbPageTabInfo_website,String fbPageTabInfo_wereHere)
        {

            try
            {

                using (Novacode.DocX doc = Novacode.DocX.Load(reportFileLocation))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"dp.jpg");

                        //myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                        //ms.Seek(0, SeekOrigin.Begin);

                        //Novacode.Image img = doc.AddImage(ms); // Create image.

                        Novacode.Paragraph pp = doc.InsertParagraph("Facebook Page Info\n");

                        pp.Bold(); pp.Color(System.Drawing.Color.Black);
                        pp.FontSize(15.0f);
                        pp.Alignment = Novacode.Alignment.center;


                        Novacode.Paragraph p = doc.InsertParagraph(fbPageTabInfo_LabelContent);

                        p.Bold(); p.Color(System.Drawing.Color.FromArgb(59, 89, 152));
                        p.FontSize(15.0f);

                        /*

                        Novacode.Picture pic1 = img.CreatePicture();     // Create picture.
                        pic1.SetPictureShape(Novacode.RectangleShapes.rect); // Set picture shape (if needed)

                        p.InsertPicture(pic1, 0); // Insert picture into paragraph.
                        //Novacode.Formatting for112=new Novacode.Formatting();

                        */

                        //p.Alignment = Novacode.Alignment.center;

                        //Novacode.Paragraph p1 = doc.InsertParagraph(userName + " " + ScreeName ,false);

                        Novacode.Paragraph p2 = doc.InsertParagraph("\n" +
                            fbPageTabInfo_userName+ "\n"+
                            fbPageTabInfo_ID + "\n" +
                            fbPageTabInfo_Category+ "\n"+
                            fbPageTabInfo_location+ "\n"+
                            fbPageTabInfo_about+ "\n"+
                            fbPageTabInfo_awards+ "\n"+
                            fbPageTabInfo_checkIns+ "\n"+
                            fbPageTabInfo_coverSource+ "\n"+
                            fbPageTabInfo_description+ "\n"+
                            fbPageTabInfo_hasAddedApp+ "\n"+
                            fbPageTabInfo_isCommunityPage+ "\n"+
                            fbPageTabInfo_isPublished+ "\n"+
                            fbPageTabInfo_link + "\n" +
                            fbPageTabInfo_website+ "\n"+
                            fbPageTabInfo_wereHere+ "\n"
                            , false);
                        
                        //Novacode.Paragraph p3 = doc.InsertParagraph(createdTime + "\n", false);

                        //p3.Alignment = Novacode.Alignment.center;

                        Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);

                        p4.Alignment = Novacode.Alignment.center;


                        ms.Close();

                        doc.Save();

                        MessageBox.Show("Added Facebook Page Info to report successfully !");
                    }//inner using...
                }//outer using....

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//func...

        //Create document method
        private void insertfbGroupProfileInfoIntoDocumentUsingDocX(String fbPageTabInfo_LabelContent, String fbPageTabInfo_userName, String fbPageTabInfo_ID, String fbPageTabInfo_Category,
            String fbPageTabInfo_location, String fbPageTabInfo_awards,
            String fbPageTabInfo_checkIns, String fbPageTabInfo_description,
            String fbPageTabInfo_link)
        {

            try
            {

                using (Novacode.DocX doc = Novacode.DocX.Load(reportFileLocation))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"dp.jpg");

                        //myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                        //ms.Seek(0, SeekOrigin.Begin);

                        //Novacode.Image img = doc.AddImage(ms); // Create image.

                        Novacode.Paragraph pp = doc.InsertParagraph("Facebook Group Info\n");

                        pp.Bold(); pp.Color(System.Drawing.Color.Black);
                        pp.FontSize(15.0f);
                        pp.Alignment = Novacode.Alignment.center;


                        Novacode.Paragraph p = doc.InsertParagraph(fbPageTabInfo_LabelContent);

                        p.Bold(); p.Color(System.Drawing.Color.FromArgb(59, 89, 152));
                        p.FontSize(15.0f);

                        /*

                        Novacode.Picture pic1 = img.CreatePicture();     // Create picture.
                        pic1.SetPictureShape(Novacode.RectangleShapes.rect); // Set picture shape (if needed)

                        p.InsertPicture(pic1, 0); // Insert picture into paragraph.
                        //Novacode.Formatting for112=new Novacode.Formatting();

                        */

                        //p.Alignment = Novacode.Alignment.center;

                        //Novacode.Paragraph p1 = doc.InsertParagraph(userName + " " + ScreeName ,false);

                        Novacode.Paragraph p2 = doc.InsertParagraph("\n" +
                            fbPageTabInfo_userName + "\n" +
                            fbPageTabInfo_ID + "\n" +
                            fbPageTabInfo_Category + "\n" +
                            fbPageTabInfo_location + "\n" +
                            //fbPageTabInfo_about + "\n" +
                            fbPageTabInfo_awards + "\n" +
                            fbPageTabInfo_checkIns + "\n" +
                            //fbPageTabInfo_coverSource + "\n" +
                            fbPageTabInfo_description + "\n" +
                            //fbPageTabInfo_hasAddedApp + "\n" +
                            //fbPageTabInfo_isCommunityPage + "\n" +
                            //fbPageTabInfo_isPublished + "\n" +
                            fbPageTabInfo_link + "\n" 
                            //fbPageTabInfo_website + "\n" +
                            //fbPageTabInfo_wereHere + "\n"
                            , false);

                        //Novacode.Paragraph p3 = doc.InsertParagraph(createdTime + "\n", false);

                        //p3.Alignment = Novacode.Alignment.center;

                        Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);

                        p4.Alignment = Novacode.Alignment.center;


                        ms.Close();

                        doc.Save();

                        MessageBox.Show("Added Facebook Group Info to report successfully !");
                    }//inner using...
                }//outer using....

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//func...

        //Create document method
        private void insertfbUserProfileInfoIntoDocumentUsingDocX(String fbPageTabInfo_LabelContent, String fbPageTabInfo_ID, String fbPageTabInfo_Category,
            String fbPageTabInfo_location, String fbPageTabInfo_description)
        {

            try
            {

                using (Novacode.DocX doc = Novacode.DocX.Load(reportFileLocation))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"dp.jpg");

                        //myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                        //ms.Seek(0, SeekOrigin.Begin);

                        //Novacode.Image img = doc.AddImage(ms); // Create image.

                        Novacode.Paragraph pp = doc.InsertParagraph("Facebook User Info\n");

                        pp.Bold(); pp.Color(System.Drawing.Color.Black);
                        pp.FontSize(15.0f);
                        pp.Alignment = Novacode.Alignment.center;


                        Novacode.Paragraph p = doc.InsertParagraph(fbPageTabInfo_LabelContent);

                        p.Bold(); p.Color(System.Drawing.Color.FromArgb(59, 89, 152));
                        p.FontSize(15.0f);

                        /*

                        Novacode.Picture pic1 = img.CreatePicture();     // Create picture.
                        pic1.SetPictureShape(Novacode.RectangleShapes.rect); // Set picture shape (if needed)

                        p.InsertPicture(pic1, 0); // Insert picture into paragraph.
                        //Novacode.Formatting for112=new Novacode.Formatting();

                        */

                        //p.Alignment = Novacode.Alignment.center;

                        //Novacode.Paragraph p1 = doc.InsertParagraph(userName + " " + ScreeName ,false);

                        Novacode.Paragraph p2 = doc.InsertParagraph("\n" +
                            "Facebook User Id: "+fbPageTabInfo_ID + "\n" +
                            fbPageTabInfo_Category + "\n" +
                            fbPageTabInfo_location + "\n" +
                            //fbPageTabInfo_checkIns + "\n" +
                            //fbPageTabInfo_coverSource + "\n" +
                            fbPageTabInfo_description + "\n"
                            //fbPageTabInfo_hasAddedApp + "\n" +
                            //fbPageTabInfo_isCommunityPage + "\n" +
                            //fbPageTabInfo_isPublished + "\n" +
                            //fbPageTabInfo_link + "\n" +
                            //fbPageTabInfo_website + "\n" +
                            //fbPageTabInfo_wereHere + "\n"
                            , false);

                        //Novacode.Paragraph p3 = doc.InsertParagraph(createdTime + "\n", false);

                        //p3.Alignment = Novacode.Alignment.center;

                        Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);

                        p4.Alignment = Novacode.Alignment.center;


                        ms.Close();

                        doc.Save();

                        MessageBox.Show("Added Facebook User Info to report successfully !");
                    }//inner using...
                }//outer using....

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//func...

        //Create document method
        private void insertSentimentAnalysisInfoIntoDocumentUsingDocX(ObservableCollection<tweetToReportItem> tweetsToReportListFunc)
        {
            try
            {
                String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Save the document
                 String pathi= path + @"\sentiment analysis.docx";

                 #region getting lists first using LINQ...

                 var temp = tweetsToReportList.Where(p => p.v >= 0.0f && p.v < 2.0f && p.a < 5.0f);
                 IEnumerable<tweetToReportItem> coll = temp as IEnumerable<tweetToReportItem>;
                if(coll!=null)
                 tweetsToReportListSad = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 0.0f && p.v < 2.0f && p.a >= 5.0f);
                temp = tweetsToReportList.Where(p => p.v >= 0.0f && p.v < 5.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListUpset = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 2.0f && p.v < 3.0f && p.a < 5.0f);
                temp = tweetsToReportList.Where(p => p.v < 5.0f && p.v >= 0.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListUnHappy = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 2.0f && p.v < 3.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListStressed = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 3.0f && p.v < 4.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListDepressed = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 3.0f && p.v < 4.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListNervous = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 4.0f && p.v < 5.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListSubdued = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 4.0f && p.v < 5.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListActive = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 5.0f && p.v < 6.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListCalm = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 5.0f && p.v < 6.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListAlert = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 6.0f && p.v < 7.0f && p.a < 5.0f);
                temp = tweetsToReportList.Where(p => p.v >= 5.0f/* && p.v < 7.0f*/ && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListRelaxed = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 6.0f && p.v < 7.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListExcited = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 7.0f && p.v < 8.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListElated = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 7.0f && p.v < 8.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListSerene = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 8.0f && p.v < 9.0f && p.a < 5.0f);
                temp = tweetsToReportList.Where(p => p.v >= 5.0f /*&& p.v < 9.0f*/ && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListHappy = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 8.0f && p.v < 9.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListContented = new ObservableCollection<tweetToReportItem>(coll);

                 #endregion

                 #region now saving and opening word file...

                 using (Novacode.DocX doc = Novacode.DocX.Create(pathi))
                {
                    Novacode.Paragraph pp = doc.InsertParagraph("\nSentiment Analysis Summary\n");
                    
                    pp.Bold(); pp.Color(System.Drawing.Color.Black);
                    pp.FontSize(20.0f);
                    pp.Alignment = Novacode.Alignment.center;

                    pp = doc.InsertParagraph(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\n");

                    pp.Bold(); pp.Color(System.Drawing.Color.Black);
                    pp.FontSize(12.0f);
                    pp.Alignment = Novacode.Alignment.center;

                    if (osintBased[tabDynamic1.SelectedIndex] != null)
                    {
                        pp = doc.InsertParagraph("Search Keyword: "+osintBased[tabDynamic1.SelectedIndex].lastSearch1 + "\n");

                        pp.Color(System.Drawing.Color.Black);
                        pp.FontSize(15.0f);
                        pp.Alignment = Novacode.Alignment.center;
                    }
                   
                     /**
                    Novacode.Paragraph p4 = doc.InsertParagraph("Sad: "+tweetsToReportListSad.Count.ToString()+"\n\n"+
                        "Upset: " + tweetsToReportListUpset.Count.ToString() + "\n\n"+
                        "UnHappy: " + tweetsToReportListUnHappy.Count.ToString() + "\n\n"+
                        "Stressed: " + tweetsToReportListStressed.Count.ToString() + "\n\n"+
                        "Depressed: " + tweetsToReportListDepressed.Count.ToString() + "\n\n"+
                        "Nervous: " + tweetsToReportListNervous.Count.ToString() + "\n\n"+
                        "Subdued: " + tweetsToReportListSubdued.Count.ToString() + "\n\n"+
                        "Active: " + tweetsToReportListActive.Count.ToString() + "\n\n"+
                        "Calm: " + tweetsToReportListCalm.Count.ToString() + "\n\n"+
                        "Alert: " + tweetsToReportListAlert.Count.ToString() + "\n\n"+
                        "Relaxed: " + tweetsToReportListRelaxed.Count.ToString() + "\n\n"+
                        "Excited: " + tweetsToReportListExcited.Count.ToString() + "\n\n"+
                        "Elated: " + tweetsToReportListElated.Count.ToString() + "\n\n"+
                        "Serene: " + tweetsToReportListSerene.Count.ToString() + "\n\n"+
                        "Happy: " + tweetsToReportListHappy.Count.ToString() + "\n\n"+
                        "Contented: " + tweetsToReportListContented.Count.ToString() + "\n\n",
                        false);
                     */

                    Novacode.Table t; int r = 1;

                    t = doc.AddTable(1 + 1, 4);
                    // Specify some properties for this Table.
                    t.Alignment = Novacode.Alignment.center;
                    t.Design = Novacode.TableDesign.LightShadingAccent5;

//                    t.AutoFit = Novacode.AutoFit.Contents;
                     

                    // Add content to this Table.
                    t.Rows[0].Cells[0].Paragraphs.First().Append("Happy");
                    t.Rows[0].Cells[1].Paragraphs.First().Append("Relaxed");
                    t.Rows[0].Cells[2].Paragraphs.First().Append("UnHappy");
                    t.Rows[0].Cells[3].Paragraphs.First().Append("Upset");
                    /*t.Rows[0].Cells[4].Paragraphs.First().Append("Depressed");
                    t.Rows[0].Cells[5].Paragraphs.First().Append("Nervous");
                    t.Rows[0].Cells[6].Paragraphs.First().Append("Subdued");
                    t.Rows[0].Cells[7].Paragraphs.First().Append("Active");*/

                    t.Rows[1].Cells[0].Paragraphs.First().Append(tweetsToReportListHappy.Count.ToString());
                    t.Rows[1].Cells[1].Paragraphs.First().Append(tweetsToReportListRelaxed.Count.ToString());
                    t.Rows[1].Cells[2].Paragraphs.First().Append(tweetsToReportListUnHappy.Count.ToString());
                    t.Rows[1].Cells[3].Paragraphs.First().Append(tweetsToReportListUpset.Count.ToString());
                    /*t.Rows[1].Cells[4].Paragraphs.First().Append(tweetsToReportListDepressed.Count.ToString());
                    t.Rows[1].Cells[5].Paragraphs.First().Append(tweetsToReportListNervous.Count.ToString());
                    t.Rows[1].Cells[6].Paragraphs.First().Append(tweetsToReportListSubdued.Count.ToString());
                    t.Rows[1].Cells[7].Paragraphs.First().Append(tweetsToReportListActive.Count.ToString());*/
                   
                    doc.InsertTable(t);

                    Novacode.Paragraph p42 = doc.InsertParagraph("\n\n", false);

/**
                    t = doc.AddTable(1 + 1, 8);
                    // Specify some properties for this Table.
                    t.Alignment = Novacode.Alignment.center;
                    t.Design = Novacode.TableDesign.LightShadingAccent5;

                    // Add content to this Table.
                    t.Rows[0].Cells[0].Paragraphs.First().Append("Calm");
                    t.Rows[0].Cells[1].Paragraphs.First().Append("Alert");
                    t.Rows[0].Cells[2].Paragraphs.First().Append("Relaxed");
                    t.Rows[0].Cells[3].Paragraphs.First().Append("Excited");
                    t.Rows[0].Cells[4].Paragraphs.First().Append("Elated");
                    t.Rows[0].Cells[5].Paragraphs.First().Append("Serene");
                    t.Rows[0].Cells[6].Paragraphs.First().Append("Happy");
                    t.Rows[0].Cells[7].Paragraphs.First().Append("Contented");

                    t.Rows[1].Cells[0].Paragraphs.First().Append(tweetsToReportListCalm.Count.ToString());
                    t.Rows[1].Cells[1].Paragraphs.First().Append(tweetsToReportListAlert.Count.ToString());
                    t.Rows[1].Cells[2].Paragraphs.First().Append(tweetsToReportListRelaxed.Count.ToString());
                    t.Rows[1].Cells[3].Paragraphs.First().Append(tweetsToReportListExcited.Count.ToString());
                    t.Rows[1].Cells[4].Paragraphs.First().Append(tweetsToReportListElated.Count.ToString());
                    t.Rows[1].Cells[5].Paragraphs.First().Append(tweetsToReportListSerene.Count.ToString());
                    t.Rows[1].Cells[6].Paragraphs.First().Append(tweetsToReportListHappy.Count.ToString());
                    t.Rows[1].Cells[7].Paragraphs.First().Append(tweetsToReportListContented.Count.ToString());

                    doc.InsertTable(t);

                    Novacode.Paragraph p44 = doc.InsertParagraph("\n\n", false);
*/

                    #region Happy

                    if (tweetsToReportListHappy.Count > 0)
                    {
                        Novacode.Paragraph p8 = doc.InsertParagraph("\nHappy Tweets Details (" + tweetsToReportListHappy.Count.ToString()+ ")\n", false);
                        p8.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListHappy.Count + 1, 4);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Tweet");

                        t.Rows[0].Cells[0].Width = 200;
                        t.Rows[0].Cells[1].Width = 80;
                        t.Rows[0].Cells[2].Width = 80;
                        t.Rows[0].Cells[3].Width = 700;

                        t.Rows[0].Cells[0].Paragraphs.First().FontSize(12.0f);
                        t.Rows[0].Cells[1].Paragraphs.First().FontSize(12.0f);
                        t.Rows[0].Cells[2].Paragraphs.First().FontSize(12.0f);
                        t.Rows[0].Cells[3].Paragraphs.First().FontSize(12.0f);

                        //t

                        t.AutoFit = Novacode.AutoFit.Contents;

                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListHappy)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            t.Rows[r].Cells[0].Width = 200;
                            t.Rows[r].Cells[1].Width = 80;
                            t.Rows[r].Cells[2].Width = 80;
                            t.Rows[r].Cells[3].Width = 700;

                            t.Rows[r].Cells[0].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[1].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[2].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[3].Paragraphs.First().FontSize(12.0f);

                            //t.

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion

                    #region Relaxed

                    if (tweetsToReportListRelaxed.Count > 0)
                    {
                        Novacode.Paragraph p8 = doc.InsertParagraph("\nRelaxed Tweets Details (" + tweetsToReportListRelaxed.Count.ToString() + ")\n", false);
                        p8.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListRelaxed.Count + 1, 4);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Tweet");
                        t.AutoFit = Novacode.AutoFit.Contents;
                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListRelaxed)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            t.Rows[r].Cells[0].Width = 200;
                            t.Rows[r].Cells[1].Width = 80;
                            t.Rows[r].Cells[2].Width = 80;
                            t.Rows[r].Cells[3].Width = 700;

                            t.Rows[r].Cells[0].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[1].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[2].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[3].Paragraphs.First().FontSize(12.0f);


                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion

                    #region Unhappy

                    if (tweetsToReportListUnHappy.Count > 0)
                    {
                        Novacode.Paragraph p7 = doc.InsertParagraph("\nUnHappy Tweets Details (" + tweetsToReportListUnHappy.Count.ToString() + ")\n", false);
                        p7.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListUnHappy.Count + 1, 4);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Tweet");

                        t.AutoFit = Novacode.AutoFit.Contents;
                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListUnHappy)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            t.Rows[r].Cells[0].Width = 200;
                            t.Rows[r].Cells[1].Width = 80;
                            t.Rows[r].Cells[2].Width = 80;
                            t.Rows[r].Cells[3].Width = 700;

                            t.Rows[r].Cells[0].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[1].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[2].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[3].Paragraphs.First().FontSize(12.0f);

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion
  
                    #region Upset

                        if (tweetsToReportListUpset.Count > 0)
                        {
                            Novacode.Paragraph p6 = doc.InsertParagraph("\nUpset Tweets Details (" + tweetsToReportListUpset.Count.ToString() + ")\n", false);
                            p6.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListUpset.Count + 1, 4);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[3].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;

                            foreach (tweetToReportItem item in tweetsToReportListUpset)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                                t.Rows[r].Cells[0].Width = 200;
                                t.Rows[r].Cells[1].Width = 80;
                                t.Rows[r].Cells[2].Width = 80;
                                t.Rows[r].Cells[3].Width = 700;

                                t.Rows[r].Cells[0].Paragraphs.First().FontSize(12.0f);
                                t.Rows[r].Cells[1].Paragraphs.First().FontSize(12.0f);
                                t.Rows[r].Cells[2].Paragraphs.First().FontSize(12.0f);
                                t.Rows[r].Cells[3].Paragraphs.First().FontSize(12.0f);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        
                        #region lower ones...
                        /**
                       #region sad

                    if (tweetsToReportListSad.Count > 0)
                    {
                        Novacode.Paragraph p5 = doc.InsertParagraph("\nSad Tweets Details\n", false);
                        p5.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListSad.Count + 1, 3);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                        t.AutoFit = Novacode.AutoFit.Contents;

                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListSad)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion

                       #region Stressed

                        if (tweetsToReportListStressed.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nStressed Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListStressed.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListStressed)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Depressed

                        if (tweetsToReportListDepressed.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nDepressed Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListDepressed.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListDepressed)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Nervous

                        if (tweetsToReportListNervous.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nNervous Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListNervous.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListNervous)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Subdued

                        if (tweetsToReportListSubdued.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nSubdued Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListSubdued.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListSubdued)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Active

                        if (tweetsToReportListActive.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nActive Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListActive.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListActive)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                      * 
                      * 
                      * #region Calm

                        if (tweetsToReportListCalm.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nCalm Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListCalm.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;

                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListCalm)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Alert

                        if (tweetsToReportListAlert.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nAlert Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListAlert.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListAlert)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Active

                        if (tweetsToReportListExcited.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nExcied Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListExcited.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListExcited)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Elated

                        if (tweetsToReportListElated.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nElated Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListElated.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListElated)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Serene

                        if (tweetsToReportListSerene.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nSerene Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListSerene.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListSerene)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Contented

                        if (tweetsToReportListContented.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nContented Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListContented.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListContented)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion
                     */
                 #endregion

                        Novacode.Paragraph p41 = doc.InsertParagraph("-----------------------------------------------------------\n", false);
                        p41.Alignment = Novacode.Alignment.center;

                     doc.Save();
                }//outer using....

                // opening word file...
                if(pathi!=null&&path.Length>0)
                 Process.Start(pathi);

                 #endregion

                // Insert the Table into the document.
            }
           catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
    
        }//func...

        //Create document method
        private void insertGoogleSentimentAnalysisInfoIntoDocumentUsingDocX(ObservableCollection<tweetToReportItem> tweetsToReportListFunc)
        {
            try
            {
                String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Save the document
                String pathi = path + @"\sentiment analysis.docx";

                #region getting lists first using LINQ...

                var temp = tweetsToReportList.Where(p => p.v >= 0.0f && p.v < 2.0f && p.a < 5.0f);
                IEnumerable<tweetToReportItem> coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListSad = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 0.0f && p.v < 2.0f && p.a >= 5.0f);
                temp = tweetsToReportList.Where(p => p.v >= 0.0f && p.v < 5.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListUpset = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 2.0f && p.v < 3.0f && p.a < 5.0f);
                temp = tweetsToReportList.Where(p => p.v < 5.0f && p.v >= 0.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListUnHappy = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 2.0f && p.v < 3.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListStressed = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 3.0f && p.v < 4.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListDepressed = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 3.0f && p.v < 4.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListNervous = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 4.0f && p.v < 5.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListSubdued = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 4.0f && p.v < 5.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListActive = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 5.0f && p.v < 6.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListCalm = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 5.0f && p.v < 6.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListAlert = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 6.0f && p.v < 7.0f && p.a < 5.0f);
                temp = tweetsToReportList.Where(p => p.v >= 5.0f/* && p.v < 7.0f*/ && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListRelaxed = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 6.0f && p.v < 7.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListExcited = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 7.0f && p.v < 8.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListElated = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 7.0f && p.v < 8.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListSerene = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 8.0f && p.v < 9.0f && p.a < 5.0f);
                temp = tweetsToReportList.Where(p => p.v >= 5.0f /*&& p.v < 9.0f*/ && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListHappy = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 8.0f && p.v < 9.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListContented = new ObservableCollection<tweetToReportItem>(coll);

                #endregion

                #region now saving and opening word file...

                using (Novacode.DocX doc = Novacode.DocX.Create(pathi))
                {
                    Novacode.Paragraph pp = doc.InsertParagraph("\nGooglePlus Sentiment Analysis Summary\n");

                    pp.Bold(); pp.Color(System.Drawing.Color.Black);
                    pp.FontSize(20.0f);
                    pp.Alignment = Novacode.Alignment.center;

                    pp = doc.InsertParagraph(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\n");

                    pp.Bold(); pp.Color(System.Drawing.Color.Black);
                    pp.FontSize(12.0f);
                    pp.Alignment = Novacode.Alignment.center;

                    if (osintGoogleBased[tabDynamicGoogle.SelectedIndex] != null)
                    {
                        pp = doc.InsertParagraph("Search Keyword: " + osintGoogleBased[tabDynamicGoogle.SelectedIndex].lastSearch1 + "\n");

                        pp.Color(System.Drawing.Color.Black);
                        pp.FontSize(15.0f);
                        pp.Alignment = Novacode.Alignment.center;
                    }

                    /**
                   Novacode.Paragraph p4 = doc.InsertParagraph("Sad: "+tweetsToReportListSad.Count.ToString()+"\n\n"+
                       "Upset: " + tweetsToReportListUpset.Count.ToString() + "\n\n"+
                       "UnHappy: " + tweetsToReportListUnHappy.Count.ToString() + "\n\n"+
                       "Stressed: " + tweetsToReportListStressed.Count.ToString() + "\n\n"+
                       "Depressed: " + tweetsToReportListDepressed.Count.ToString() + "\n\n"+
                       "Nervous: " + tweetsToReportListNervous.Count.ToString() + "\n\n"+
                       "Subdued: " + tweetsToReportListSubdued.Count.ToString() + "\n\n"+
                       "Active: " + tweetsToReportListActive.Count.ToString() + "\n\n"+
                       "Calm: " + tweetsToReportListCalm.Count.ToString() + "\n\n"+
                       "Alert: " + tweetsToReportListAlert.Count.ToString() + "\n\n"+
                       "Relaxed: " + tweetsToReportListRelaxed.Count.ToString() + "\n\n"+
                       "Excited: " + tweetsToReportListExcited.Count.ToString() + "\n\n"+
                       "Elated: " + tweetsToReportListElated.Count.ToString() + "\n\n"+
                       "Serene: " + tweetsToReportListSerene.Count.ToString() + "\n\n"+
                       "Happy: " + tweetsToReportListHappy.Count.ToString() + "\n\n"+
                       "Contented: " + tweetsToReportListContented.Count.ToString() + "\n\n",
                       false);
                    */

                    Novacode.Table t; int r = 1;

                    t = doc.AddTable(1 + 1, 4);
                    // Specify some properties for this Table.
                    t.Alignment = Novacode.Alignment.center;
                    t.Design = Novacode.TableDesign.LightShadingAccent5;

                    //                    t.AutoFit = Novacode.AutoFit.Contents;


                    // Add content to this Table.
                    t.Rows[0].Cells[0].Paragraphs.First().Append("Happy");
                    t.Rows[0].Cells[1].Paragraphs.First().Append("Relaxed");
                    t.Rows[0].Cells[2].Paragraphs.First().Append("UnHappy");
                    t.Rows[0].Cells[3].Paragraphs.First().Append("Upset");
                    /*t.Rows[0].Cells[4].Paragraphs.First().Append("Depressed");
                    t.Rows[0].Cells[5].Paragraphs.First().Append("Nervous");
                    t.Rows[0].Cells[6].Paragraphs.First().Append("Subdued");
                    t.Rows[0].Cells[7].Paragraphs.First().Append("Active");*/

                    t.Rows[1].Cells[0].Paragraphs.First().Append(tweetsToReportListHappy.Count.ToString());
                    t.Rows[1].Cells[1].Paragraphs.First().Append(tweetsToReportListRelaxed.Count.ToString());
                    t.Rows[1].Cells[2].Paragraphs.First().Append(tweetsToReportListUnHappy.Count.ToString());
                    t.Rows[1].Cells[3].Paragraphs.First().Append(tweetsToReportListUpset.Count.ToString());
                    /*t.Rows[1].Cells[4].Paragraphs.First().Append(tweetsToReportListDepressed.Count.ToString());
                    t.Rows[1].Cells[5].Paragraphs.First().Append(tweetsToReportListNervous.Count.ToString());
                    t.Rows[1].Cells[6].Paragraphs.First().Append(tweetsToReportListSubdued.Count.ToString());
                    t.Rows[1].Cells[7].Paragraphs.First().Append(tweetsToReportListActive.Count.ToString());*/

                    doc.InsertTable(t);

                    Novacode.Paragraph p42 = doc.InsertParagraph("\n\n", false);

                    /**
                                        t = doc.AddTable(1 + 1, 8);
                                        // Specify some properties for this Table.
                                        t.Alignment = Novacode.Alignment.center;
                                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                                        // Add content to this Table.
                                        t.Rows[0].Cells[0].Paragraphs.First().Append("Calm");
                                        t.Rows[0].Cells[1].Paragraphs.First().Append("Alert");
                                        t.Rows[0].Cells[2].Paragraphs.First().Append("Relaxed");
                                        t.Rows[0].Cells[3].Paragraphs.First().Append("Excited");
                                        t.Rows[0].Cells[4].Paragraphs.First().Append("Elated");
                                        t.Rows[0].Cells[5].Paragraphs.First().Append("Serene");
                                        t.Rows[0].Cells[6].Paragraphs.First().Append("Happy");
                                        t.Rows[0].Cells[7].Paragraphs.First().Append("Contented");

                                        t.Rows[1].Cells[0].Paragraphs.First().Append(tweetsToReportListCalm.Count.ToString());
                                        t.Rows[1].Cells[1].Paragraphs.First().Append(tweetsToReportListAlert.Count.ToString());
                                        t.Rows[1].Cells[2].Paragraphs.First().Append(tweetsToReportListRelaxed.Count.ToString());
                                        t.Rows[1].Cells[3].Paragraphs.First().Append(tweetsToReportListExcited.Count.ToString());
                                        t.Rows[1].Cells[4].Paragraphs.First().Append(tweetsToReportListElated.Count.ToString());
                                        t.Rows[1].Cells[5].Paragraphs.First().Append(tweetsToReportListSerene.Count.ToString());
                                        t.Rows[1].Cells[6].Paragraphs.First().Append(tweetsToReportListHappy.Count.ToString());
                                        t.Rows[1].Cells[7].Paragraphs.First().Append(tweetsToReportListContented.Count.ToString());

                                        doc.InsertTable(t);

                                        Novacode.Paragraph p44 = doc.InsertParagraph("\n\n", false);
                    */

                    #region Happy

                    if (tweetsToReportListHappy.Count > 0)
                    {
                        Novacode.Paragraph p8 = doc.InsertParagraph("\nHappy Posts Details (" + tweetsToReportListHappy.Count.ToString() + ")\n", false);
                        p8.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListHappy.Count + 1, 4);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Post");

                        t.Rows[0].Cells[0].Width = 200;
                        t.Rows[0].Cells[1].Width = 80;
                        t.Rows[0].Cells[2].Width = 80;
                        t.Rows[0].Cells[3].Width = 700;

                        t.Rows[0].Cells[0].Paragraphs.First().FontSize(12.0f);
                        t.Rows[0].Cells[1].Paragraphs.First().FontSize(12.0f);
                        t.Rows[0].Cells[2].Paragraphs.First().FontSize(12.0f);
                        t.Rows[0].Cells[3].Paragraphs.First().FontSize(12.0f);

                        //t

                        t.AutoFit = Novacode.AutoFit.Contents;

                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListHappy)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            t.Rows[r].Cells[0].Width = 200;
                            t.Rows[r].Cells[1].Width = 80;
                            t.Rows[r].Cells[2].Width = 80;
                            t.Rows[r].Cells[3].Width = 700;

                            t.Rows[r].Cells[0].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[1].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[2].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[3].Paragraphs.First().FontSize(12.0f);

                            //t.

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion

                    #region Relaxed

                    if (tweetsToReportListRelaxed.Count > 0)
                    {
                        Novacode.Paragraph p8 = doc.InsertParagraph("\nRelaxed Posts Details (" + tweetsToReportListRelaxed.Count.ToString() + ")\n", false);
                        p8.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListRelaxed.Count + 1, 4);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Post");
                        t.AutoFit = Novacode.AutoFit.Contents;
                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListRelaxed)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion

                    #region Unhappy

                    if (tweetsToReportListUnHappy.Count > 0)
                    {
                        Novacode.Paragraph p7 = doc.InsertParagraph("\nUnHappy Posts Details (" + tweetsToReportListUnHappy.Count.ToString() + ")\n", false);
                        p7.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListUnHappy.Count + 1, 4);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Post");

                        t.AutoFit = Novacode.AutoFit.Contents;
                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListUnHappy)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion

                    #region Upset

                    if (tweetsToReportListUpset.Count > 0)
                    {
                        Novacode.Paragraph p6 = doc.InsertParagraph("\nUpset Posts Details (" + tweetsToReportListUpset.Count.ToString() + ")\n", false);
                        p6.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListUpset.Count + 1, 4);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Post");

                        t.AutoFit = Novacode.AutoFit.Contents;
                        r = 1;

                        foreach (tweetToReportItem item in tweetsToReportListUpset)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion


                    #region lower ones...
                    /**
                       #region sad

                    if (tweetsToReportListSad.Count > 0)
                    {
                        Novacode.Paragraph p5 = doc.InsertParagraph("\nSad Tweets Details\n", false);
                        p5.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListSad.Count + 1, 3);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                        t.AutoFit = Novacode.AutoFit.Contents;

                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListSad)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion

                       #region Stressed

                        if (tweetsToReportListStressed.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nStressed Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListStressed.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListStressed)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Depressed

                        if (tweetsToReportListDepressed.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nDepressed Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListDepressed.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListDepressed)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Nervous

                        if (tweetsToReportListNervous.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nNervous Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListNervous.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListNervous)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Subdued

                        if (tweetsToReportListSubdued.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nSubdued Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListSubdued.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListSubdued)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Active

                        if (tweetsToReportListActive.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nActive Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListActive.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListActive)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                      * 
                      * 
                      * #region Calm

                        if (tweetsToReportListCalm.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nCalm Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListCalm.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;

                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListCalm)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Alert

                        if (tweetsToReportListAlert.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nAlert Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListAlert.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListAlert)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Active

                        if (tweetsToReportListExcited.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nExcied Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListExcited.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListExcited)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Elated

                        if (tweetsToReportListElated.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nElated Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListElated.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListElated)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Serene

                        if (tweetsToReportListSerene.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nSerene Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListSerene.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListSerene)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Contented

                        if (tweetsToReportListContented.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nContented Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListContented.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListContented)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion
                     */
                #endregion

                    Novacode.Paragraph p41 = doc.InsertParagraph("-----------------------------------------------------------\n", false);
                    p41.Alignment = Novacode.Alignment.center;

                    doc.Save();
                }//outer using....

                // opening word file...
                if (pathi != null && path.Length > 0)
                    Process.Start(pathi);

                #endregion

                // Insert the Table into the document.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//func...

        //Create document method
        private void insertfbPostsSentimentAnalysisInfoIntoDocumentUsingDocX(ObservableCollection<tweetToReportItem> tweetsToReportListFunc)
        {
            try
            {
                String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                //Save the document
                String pathi = path + @"\sentiment analysis.docx";

                #region getting lists first using LINQ...

                var temp = tweetsToReportList.Where(p => p.v >= 0.0f && p.v < 2.0f && p.a < 5.0f);
                IEnumerable<tweetToReportItem> coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListSad = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 0.0f && p.v < 2.0f && p.a >= 5.0f);
                temp = tweetsToReportList.Where(p => p.v >= 0.0f && p.v < 5.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListUpset = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 2.0f && p.v < 3.0f && p.a < 5.0f);
                temp = tweetsToReportList.Where(p => p.v < 5.0f && p.v >= 0.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListUnHappy = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 2.0f && p.v < 3.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListStressed = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 3.0f && p.v < 4.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListDepressed = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 3.0f && p.v < 4.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListNervous = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 4.0f && p.v < 5.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListSubdued = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 4.0f && p.v < 5.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListActive = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 5.0f && p.v < 6.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListCalm = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 5.0f && p.v < 6.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListAlert = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 6.0f && p.v < 7.0f && p.a < 5.0f);
                temp = tweetsToReportList.Where(p => p.v >= 5.0f/* && p.v < 7.0f*/ && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListRelaxed = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 6.0f && p.v < 7.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListExcited = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 7.0f && p.v < 8.0f && p.a < 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListElated = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 7.0f && p.v < 8.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListSerene = new ObservableCollection<tweetToReportItem>(coll);

                //temp = tweetsToReportList.Where(p => p.v >= 8.0f && p.v < 9.0f && p.a < 5.0f);
                temp = tweetsToReportList.Where(p => p.v >= 5.0f /*&& p.v < 9.0f*/ && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListHappy = new ObservableCollection<tweetToReportItem>(coll);

                temp = tweetsToReportList.Where(p => p.v >= 8.0f && p.v < 9.0f && p.a >= 5.0f);
                coll = temp as IEnumerable<tweetToReportItem>;
                if (coll != null)
                    tweetsToReportListContented = new ObservableCollection<tweetToReportItem>(coll);

                #endregion

                #region now saving and opening word file...

                using (Novacode.DocX doc = Novacode.DocX.Create(pathi))
                {
                    Novacode.Paragraph pp = doc.InsertParagraph("\nFacebook Sentiment Analysis Summary\n");

                    pp.Bold(); pp.Color(System.Drawing.Color.Black);
                    pp.FontSize(20.0f);
                    pp.Alignment = Novacode.Alignment.center;

                    pp = doc.InsertParagraph(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\n");

                    pp.Bold(); pp.Color(System.Drawing.Color.Black);
                    pp.FontSize(12.0f);
                    pp.Alignment = Novacode.Alignment.center;

                    if (osintGoogleBased[tabDynamicGoogle.SelectedIndex] != null)
                    {
                        pp = doc.InsertParagraph("Search Keyword: " + osintfbBased[tabDynamicfb.SelectedIndex].lastSearch1 + "\n");

                        pp.Color(System.Drawing.Color.Black);
                        pp.FontSize(15.0f);
                        pp.Alignment = Novacode.Alignment.center;
                    }

                    /**
                   Novacode.Paragraph p4 = doc.InsertParagraph("Sad: "+tweetsToReportListSad.Count.ToString()+"\n\n"+
                       "Upset: " + tweetsToReportListUpset.Count.ToString() + "\n\n"+
                       "UnHappy: " + tweetsToReportListUnHappy.Count.ToString() + "\n\n"+
                       "Stressed: " + tweetsToReportListStressed.Count.ToString() + "\n\n"+
                       "Depressed: " + tweetsToReportListDepressed.Count.ToString() + "\n\n"+
                       "Nervous: " + tweetsToReportListNervous.Count.ToString() + "\n\n"+
                       "Subdued: " + tweetsToReportListSubdued.Count.ToString() + "\n\n"+
                       "Active: " + tweetsToReportListActive.Count.ToString() + "\n\n"+
                       "Calm: " + tweetsToReportListCalm.Count.ToString() + "\n\n"+
                       "Alert: " + tweetsToReportListAlert.Count.ToString() + "\n\n"+
                       "Relaxed: " + tweetsToReportListRelaxed.Count.ToString() + "\n\n"+
                       "Excited: " + tweetsToReportListExcited.Count.ToString() + "\n\n"+
                       "Elated: " + tweetsToReportListElated.Count.ToString() + "\n\n"+
                       "Serene: " + tweetsToReportListSerene.Count.ToString() + "\n\n"+
                       "Happy: " + tweetsToReportListHappy.Count.ToString() + "\n\n"+
                       "Contented: " + tweetsToReportListContented.Count.ToString() + "\n\n",
                       false);
                    */

                    Novacode.Table t; int r = 1;

                    t = doc.AddTable(1 + 1, 4);
                    // Specify some properties for this Table.
                    t.Alignment = Novacode.Alignment.center;
                    t.Design = Novacode.TableDesign.LightShadingAccent5;

                    //                    t.AutoFit = Novacode.AutoFit.Contents;


                    // Add content to this Table.
                    t.Rows[0].Cells[0].Paragraphs.First().Append("Happy");
                    t.Rows[0].Cells[1].Paragraphs.First().Append("Relaxed");
                    t.Rows[0].Cells[2].Paragraphs.First().Append("UnHappy");
                    t.Rows[0].Cells[3].Paragraphs.First().Append("Upset");
                    /*t.Rows[0].Cells[4].Paragraphs.First().Append("Depressed");
                    t.Rows[0].Cells[5].Paragraphs.First().Append("Nervous");
                    t.Rows[0].Cells[6].Paragraphs.First().Append("Subdued");
                    t.Rows[0].Cells[7].Paragraphs.First().Append("Active");*/

                    t.Rows[1].Cells[0].Paragraphs.First().Append(tweetsToReportListHappy.Count.ToString());
                    t.Rows[1].Cells[1].Paragraphs.First().Append(tweetsToReportListRelaxed.Count.ToString());
                    t.Rows[1].Cells[2].Paragraphs.First().Append(tweetsToReportListUnHappy.Count.ToString());
                    t.Rows[1].Cells[3].Paragraphs.First().Append(tweetsToReportListUpset.Count.ToString());
                    /*t.Rows[1].Cells[4].Paragraphs.First().Append(tweetsToReportListDepressed.Count.ToString());
                    t.Rows[1].Cells[5].Paragraphs.First().Append(tweetsToReportListNervous.Count.ToString());
                    t.Rows[1].Cells[6].Paragraphs.First().Append(tweetsToReportListSubdued.Count.ToString());
                    t.Rows[1].Cells[7].Paragraphs.First().Append(tweetsToReportListActive.Count.ToString());*/

                    doc.InsertTable(t);

                    Novacode.Paragraph p42 = doc.InsertParagraph("\n\n", false);

                    /**
                                        t = doc.AddTable(1 + 1, 8);
                                        // Specify some properties for this Table.
                                        t.Alignment = Novacode.Alignment.center;
                                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                                        // Add content to this Table.
                                        t.Rows[0].Cells[0].Paragraphs.First().Append("Calm");
                                        t.Rows[0].Cells[1].Paragraphs.First().Append("Alert");
                                        t.Rows[0].Cells[2].Paragraphs.First().Append("Relaxed");
                                        t.Rows[0].Cells[3].Paragraphs.First().Append("Excited");
                                        t.Rows[0].Cells[4].Paragraphs.First().Append("Elated");
                                        t.Rows[0].Cells[5].Paragraphs.First().Append("Serene");
                                        t.Rows[0].Cells[6].Paragraphs.First().Append("Happy");
                                        t.Rows[0].Cells[7].Paragraphs.First().Append("Contented");

                                        t.Rows[1].Cells[0].Paragraphs.First().Append(tweetsToReportListCalm.Count.ToString());
                                        t.Rows[1].Cells[1].Paragraphs.First().Append(tweetsToReportListAlert.Count.ToString());
                                        t.Rows[1].Cells[2].Paragraphs.First().Append(tweetsToReportListRelaxed.Count.ToString());
                                        t.Rows[1].Cells[3].Paragraphs.First().Append(tweetsToReportListExcited.Count.ToString());
                                        t.Rows[1].Cells[4].Paragraphs.First().Append(tweetsToReportListElated.Count.ToString());
                                        t.Rows[1].Cells[5].Paragraphs.First().Append(tweetsToReportListSerene.Count.ToString());
                                        t.Rows[1].Cells[6].Paragraphs.First().Append(tweetsToReportListHappy.Count.ToString());
                                        t.Rows[1].Cells[7].Paragraphs.First().Append(tweetsToReportListContented.Count.ToString());

                                        doc.InsertTable(t);

                                        Novacode.Paragraph p44 = doc.InsertParagraph("\n\n", false);
                    */

                    #region Happy

                    if (tweetsToReportListHappy.Count > 0)
                    {
                        Novacode.Paragraph p8 = doc.InsertParagraph("\nHappy Posts Details (" + tweetsToReportListHappy.Count.ToString() + ")\n", false);
                        p8.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListHappy.Count + 1, 4);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Post");

                        t.Rows[0].Cells[0].Width = 200;
                        t.Rows[0].Cells[1].Width = 80;
                        t.Rows[0].Cells[2].Width = 80;
                        t.Rows[0].Cells[3].Width = 700;

                        t.Rows[0].Cells[0].Paragraphs.First().FontSize(12.0f);
                        t.Rows[0].Cells[1].Paragraphs.First().FontSize(12.0f);
                        t.Rows[0].Cells[2].Paragraphs.First().FontSize(12.0f);
                        t.Rows[0].Cells[3].Paragraphs.First().FontSize(12.0f);

                        //t

                        t.AutoFit = Novacode.AutoFit.Contents;

                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListHappy)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            t.Rows[r].Cells[0].Width = 200;
                            t.Rows[r].Cells[1].Width = 80;
                            t.Rows[r].Cells[2].Width = 80;
                            t.Rows[r].Cells[3].Width = 700;

                            t.Rows[r].Cells[0].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[1].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[2].Paragraphs.First().FontSize(12.0f);
                            t.Rows[r].Cells[3].Paragraphs.First().FontSize(12.0f);

                            //t.

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion

                    #region Relaxed

                    if (tweetsToReportListRelaxed.Count > 0)
                    {
                        Novacode.Paragraph p8 = doc.InsertParagraph("\nRelaxed Posts Details (" + tweetsToReportListRelaxed.Count.ToString() + ")\n", false);
                        p8.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListRelaxed.Count + 1, 4);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Post");
                        t.AutoFit = Novacode.AutoFit.Contents;
                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListRelaxed)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion

                    #region Unhappy

                    if (tweetsToReportListUnHappy.Count > 0)
                    {
                        Novacode.Paragraph p7 = doc.InsertParagraph("\nUnHappy Posts Details (" + tweetsToReportListUnHappy.Count.ToString() + ")\n", false);
                        p7.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListUnHappy.Count + 1, 4);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Post");

                        t.AutoFit = Novacode.AutoFit.Contents;
                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListUnHappy)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion

                    #region Upset

                    if (tweetsToReportListUpset.Count > 0)
                    {
                        Novacode.Paragraph p6 = doc.InsertParagraph("\nUpset Posts Details (" + tweetsToReportListUpset.Count.ToString() + ")\n", false);
                        p6.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListUpset.Count + 1, 4);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[3].Paragraphs.First().Append("Post");

                        t.AutoFit = Novacode.AutoFit.Contents;
                        r = 1;

                        foreach (tweetToReportItem item in tweetsToReportListUpset)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion


                    #region lower ones...
                    /**
                       #region sad

                    if (tweetsToReportListSad.Count > 0)
                    {
                        Novacode.Paragraph p5 = doc.InsertParagraph("\nSad Tweets Details\n", false);
                        p5.Alignment = Novacode.Alignment.center;

                        t = doc.AddTable(tweetsToReportListSad.Count + 1, 3);
                        // Specify some properties for this Table.
                        t.Alignment = Novacode.Alignment.center;
                        t.Design = Novacode.TableDesign.LightShadingAccent5;

                        // Add content to this Table.
                        t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                        t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                        //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                        //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                        t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                        t.AutoFit = Novacode.AutoFit.Contents;

                        r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListSad)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                            r++;

                        }//foreach...

                        doc.InsertTable(t);
                    }
                    #endregion

                       #region Stressed

                        if (tweetsToReportListStressed.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nStressed Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListStressed.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListStressed)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Depressed

                        if (tweetsToReportListDepressed.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nDepressed Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListDepressed.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListDepressed)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Nervous

                        if (tweetsToReportListNervous.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nNervous Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListNervous.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListNervous)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Subdued

                        if (tweetsToReportListSubdued.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nSubdued Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListSubdued.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListSubdued)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Active

                        if (tweetsToReportListActive.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nActive Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListActive.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListActive)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                      * 
                      * 
                      * #region Calm

                        if (tweetsToReportListCalm.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nCalm Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListCalm.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;

                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListCalm)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Alert

                        if (tweetsToReportListAlert.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nAlert Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListAlert.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListAlert)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Active

                        if (tweetsToReportListExcited.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nExcied Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListExcited.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListExcited)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Elated

                        if (tweetsToReportListElated.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nElated Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListElated.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListElated)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Serene

                        if (tweetsToReportListSerene.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nSerene Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListSerene.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListSerene)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion

                        #region Contented

                        if (tweetsToReportListContented.Count > 0)
                        {
                            Novacode.Paragraph p8 = doc.InsertParagraph("\nContented Tweets Details\n", false);
                            p8.Alignment = Novacode.Alignment.center;

                            t = doc.AddTable(tweetsToReportListContented.Count + 1, 3);
                            // Specify some properties for this Table.
                            t.Alignment = Novacode.Alignment.center;
                            t.Design = Novacode.TableDesign.LightShadingAccent5;

                            // Add content to this Table.
                            t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                            t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                            //t.Rows[0].Cells[2].Paragraphs.First().Append("v");
                            //t.Rows[0].Cells[3].Paragraphs.First().Append("a");
                            t.Rows[0].Cells[2].Paragraphs.First().Append("Tweet");

                            t.AutoFit = Novacode.AutoFit.Contents;
                            r = 1;
                            foreach (tweetToReportItem item in tweetsToReportListContented)
                            {
                                t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                                t.Rows[r].Cells[1].Paragraphs.First().Append(item.userName);
                                //t.Rows[r].Cells[2].Paragraphs.First().Append(item.v.ToString());
                                //t.Rows[r].Cells[3].Paragraphs.First().Append(item.a.ToString());
                                t.Rows[r].Cells[2].Paragraphs.First().Append(item.tweet);

                                r++;

                            }//foreach...

                            doc.InsertTable(t);
                        }
                        #endregion
                     */
                #endregion

                    Novacode.Paragraph p41 = doc.InsertParagraph("-----------------------------------------------------------\n", false);
                    p41.Alignment = Novacode.Alignment.center;

                    doc.Save();
                }//outer using....

                // opening word file...
                if (pathi != null && path.Length > 0)
                    Process.Start(pathi);

                #endregion

                // Insert the Table into the document.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//func...

        //Create document method
        private void insertAllTweetsInfoIntoDocumentUsingDocX(ObservableCollection<tweetToReportItem> tweetsToReportListFunc)
        {
            
            try
            {

                using (Novacode.DocX doc = Novacode.DocX.Load(reportFileLocation))
                {

                    Novacode.Paragraph pp = doc.InsertParagraph("\nAll Tweets\n");

                    pp.Bold(); pp.Color(System.Drawing.Color.Black);
                    pp.FontSize(15.0f);
                    pp.Alignment = Novacode.Alignment.center;

                    Novacode.Table t = doc.AddTable(tweetsToReportList.Count + 1, 4);
                    // Specify some properties for this Table.
                    t.Alignment = Novacode.Alignment.center;
                    t.Design = Novacode.TableDesign.LightShadingAccent5;

                    t.AutoFit = Novacode.AutoFit.Contents;

                    // Add content to this Table.
                    //t.Rows[1].Cells[2].Paragraphs.First().Append("F");
                    t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                    t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                    t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                    //t.Rows[0].Cells[3].Paragraphs.First().Append("v");
                    //t.Rows[0].Cells[4].Paragraphs.First().Append("a");
                    t.Rows[0].Cells[3].Paragraphs.First().Append("Tweet");

                    using (MemoryStream ms = new MemoryStream())
                    {
                        //Novacode.Paragraph p = doc.InsertParagraph("\n" +
                        //        "dateTime" + "\t" +
                        //        "userName" + "\t" +
                        //        "v" + "\t" +
                        //        "a" + "\t" +
                        //        "Tweet" + "\t");

                        //p.Bold(); p.Color(System.Drawing.Color.FromArgb(59, 89, 152));
                        //p.FontSize(15.0f);

                        int r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListFunc) 
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[4].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            r++;

                            //Novacode.Paragraph p2 = doc.InsertParagraph("\n" +
                            //    item.dateTime + "\t" +
                            //    item.userName + "\t" +
                            //    item.v + "\t" +
                            //    item.a + "\t"+
                            //    item.tweet + "\t"
                            //    , false);

                        }//foreach...

                        //Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);

                        //p4.Alignment = Novacode.Alignment.center;

                        //ms.Close();

                        //doc.Save();

                       // MessageBox.Show("Added All Tweets to report successfully !");

                        doc.InsertTable(t);

                        Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);
                        p4.Alignment = Novacode.Alignment.center;
                        
                        doc.Save();
                    }//inner using...

                }//outer using....

                // Insert the Table into the document.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//func...

        //Create document method
        private void insertAllActivitiesInfoIntoDocumentUsingDocX(ObservableCollection<tweetToReportItem> tweetsToReportListFunc)
        {

            try
            {

                using (Novacode.DocX doc = Novacode.DocX.Load(reportFileLocation))
                {

                    Novacode.Paragraph pp = doc.InsertParagraph("\nAll GooglePlus Posts\n");

                    pp.Bold(); pp.Color(System.Drawing.Color.Black);
                    pp.FontSize(15.0f);
                    pp.Alignment = Novacode.Alignment.center;

                    Novacode.Table t = doc.AddTable(tweetsToReportList.Count + 1, 4);
                    // Specify some properties for this Table.
                    t.Alignment = Novacode.Alignment.center;
                    t.Design = Novacode.TableDesign.LightShadingAccent5;

                    t.AutoFit = Novacode.AutoFit.Window;

                    // Add content to this Table.
                    //t.Rows[1].Cells[2].Paragraphs.First().Append("F");
                    t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                    t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                    t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                    //t.Rows[0].Cells[3].Paragraphs.First().Append("v");
                    //t.Rows[0].Cells[4].Paragraphs.First().Append("a");
                    t.Rows[0].Cells[3].Paragraphs.First().Append("Post");

                    using (MemoryStream ms = new MemoryStream())
                    {
                        //Novacode.Paragraph p = doc.InsertParagraph("\n" +
                        //        "dateTime" + "\t" +
                        //        "userName" + "\t" +
                        //        "v" + "\t" +
                        //        "a" + "\t" +
                        //        "Tweet" + "\t");

                        //p.Bold(); p.Color(System.Drawing.Color.FromArgb(59, 89, 152));
                        //p.FontSize(15.0f);

                        int r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListFunc)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[4].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            r++;

                            //Novacode.Paragraph p2 = doc.InsertParagraph("\n" +
                            //    item.dateTime + "\t" +
                            //    item.userName + "\t" +
                            //    item.v + "\t" +
                            //    item.a + "\t"+
                            //    item.tweet + "\t"
                            //    , false);

                        }//foreach...

                        //Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);

                        //p4.Alignment = Novacode.Alignment.center;

                        //ms.Close();

                        //doc.Save();

                        // MessageBox.Show("Added All Tweets to report successfully !");

                        doc.InsertTable(t);

                        Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);
                        p4.Alignment = Novacode.Alignment.center;

                        doc.Save();
                    }//inner using...

                }//outer using....

                // Insert the Table into the document.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//func...

        //Create document method
        private void insertAllFbPostsInfoIntoDocumentUsingDocX(ObservableCollection<tweetToReportItem> tweetsToReportListFunc)
        {

            try
            {

                using (Novacode.DocX doc = Novacode.DocX.Load(reportFileLocation))
                {

                    Novacode.Paragraph pp = doc.InsertParagraph("\nAll Facebook Posts\n");

                    pp.Bold(); pp.Color(System.Drawing.Color.Black);
                    pp.FontSize(15.0f);
                    pp.Alignment = Novacode.Alignment.center;

                    Novacode.Table t = doc.AddTable(tweetsToReportList.Count + 1, 4);
                    // Specify some properties for this Table.
                    t.Alignment = Novacode.Alignment.center;
                    t.Design = Novacode.TableDesign.LightShadingAccent5;

                    t.AutoFit = Novacode.AutoFit.Window;

                    // Add content to this Table.
                    //t.Rows[1].Cells[2].Paragraphs.First().Append("F");
                    t.Rows[0].Cells[0].Paragraphs.First().Append("DateTime");
                    t.Rows[0].Cells[1].Paragraphs.First().Append("UserName");
                    t.Rows[0].Cells[2].Paragraphs.First().Append("ScreenName");
                    //t.Rows[0].Cells[3].Paragraphs.First().Append("v");
                    //t.Rows[0].Cells[4].Paragraphs.First().Append("a");
                    t.Rows[0].Cells[3].Paragraphs.First().Append("Post");

                    using (MemoryStream ms = new MemoryStream())
                    {
                        //Novacode.Paragraph p = doc.InsertParagraph("\n" +
                        //        "dateTime" + "\t" +
                        //        "userName" + "\t" +
                        //        "v" + "\t" +
                        //        "a" + "\t" +
                        //        "Tweet" + "\t");

                        //p.Bold(); p.Color(System.Drawing.Color.FromArgb(59, 89, 152));
                        //p.FontSize(15.0f);

                        int r = 1;
                        foreach (tweetToReportItem item in tweetsToReportListFunc)
                        {
                            t.Rows[r].Cells[0].Paragraphs.First().Append(item.dateTime);
                            t.Rows[r].Cells[1].Paragraphs.First().Append(item.screenName);
                            t.Rows[r].Cells[2].Paragraphs.First().Append(item.userName);
                            //t.Rows[r].Cells[3].Paragraphs.First().Append(item.v.ToString());
                            //t.Rows[r].Cells[4].Paragraphs.First().Append(item.a.ToString());
                            t.Rows[r].Cells[3].Paragraphs.First().Append(item.tweet);

                            r++;

                            //Novacode.Paragraph p2 = doc.InsertParagraph("\n" +
                            //    item.dateTime + "\t" +
                            //    item.userName + "\t" +
                            //    item.v + "\t" +
                            //    item.a + "\t"+
                            //    item.tweet + "\t"
                            //    , false);

                        }//foreach...

                        //Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);

                        //p4.Alignment = Novacode.Alignment.center;

                        //ms.Close();

                        //doc.Save();

                        // MessageBox.Show("Added All Tweets to report successfully !");

                        doc.InsertTable(t);

                        Novacode.Paragraph p4 = doc.InsertParagraph("-----------------------------------------------------------\n", false);
                        p4.Alignment = Novacode.Alignment.center;

                        doc.Save();
                    }//inner using...

                }//outer using....

                // Insert the Table into the document.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }//func...




        private void getLogin_Click(object sender, RoutedEventArgs e)
        {
            string uId = userNameTextBox.Text.Trim();
            string pass = passwordTextBox.Password;

            /*MessageBox.Show(
                "userId=" + uId + "\n" +
                "password=" + pass + "\n" +
                "ip=" + ipId + "\n" +
            "macId=" + macId + "\n" +
            "sId=" + sId);*/

            if ((uId.Equals("samanager") || uId.Equals("saop")) && pass.Equals("123456") /*&&
                ipId == 3232236112*//*3232236111 3232237673*/ && macId == 154880080045081ul /**&& macId == 154880080108673ul *//*154880080045081ul 110337783785927ul*/ /**&& sId.Equals("S-1-5-21-2974535589-2240299097-3769635268-1001"
                "S-1-5-32-544"*/
                /*"S-1-5-21-3000391741-3201552926-2021448925-1001"*/
                )
            {
                backBlackForLogin.Visibility = Visibility.Collapsed;
                loginContainer.Visibility = Visibility.Collapsed;
            }//if uid.equals...
            else
                MessageBox.Show("Invalid User Id/ Password");
        }

        private void getLogin_ClickEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string uId = userNameTextBox.Text.Trim();
                string pass = passwordTextBox.Password;

                /*MessageBox.Show(
                    "userId="+uId+"\n"+
                    "password="+pass+"\n"+
                    "ip=" + ipId + "\n" +
                "macId=" + macId + "\n" +
                "sId=" + sId);*/
                
                if ((uId.Equals("samanager") || uId.Equals("saop")) && pass.Equals("123456") /*&&
                ipId == 3232236112*//*3232236111 3232237673*/ && macId == 154880080045081ul /** && macId == 154880080108673ul*/ /*154880080045081ul 110337783785927ul*/ /**&& sId.Equals("S-1-5-21-2974535589-2240299097-3769635268-1001"
                "S-1-5-32-544"*/
                    /*"S-1-5-21-3000391741-3201552926-2021448925-1001"*/
                )
            
                {
                    backBlackForLogin.Visibility = Visibility.Collapsed;
                    loginContainer.Visibility = Visibility.Collapsed;
                }//if uid.equals...
                else
                    MessageBox.Show("Invalid User Id/ Password");
            }//enter...
        }//func...

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string copyright = "\u00a9 2015 C@RE Pvt Ltd, all rights reserved.";
            MessageBox.Show("Care OSInt Based Social Media Analyzer is a copyright product of C@RE Pvt Ltd. It cannot be used or distributed commercially, personally or for business purposes without prior permisison from the Authority. \n\n" + copyright);
        }//func...

        void ProcessStart(Process Apache, Process mySQL)
        {
            try{
            ProcessStartInfo startApache = new ProcessStartInfo();
            ProcessStartInfo startSQL = new ProcessStartInfo();
            startApache.FileName = @"C:\xampp\apache_start.bat";
            startSQL.FileName = @"C:\xampp\mysql_start.bat";
            startApache.WindowStyle = ProcessWindowStyle.Hidden;
            startSQL.WindowStyle = ProcessWindowStyle.Hidden;                     
            Apache = Process.Start(startApache);
            mySQL = Process.Start(startSQL);
            p1 = Apache;
            p2 = mySQL;
            }//try
            catch(Exception ex)
            {

            }//catch...

        }//func...

        void ProcessStop(Process Apache, Process mySQL)
        {

            if (!Apache.HasExited)
            {

                try
                {
                    foreach (Process proc in Process.GetProcessesByName("httpd"))
                    {
                        proc.Kill();
                        proc.CloseMainWindow();
                        proc.Dispose();

                    }
                }
                catch (Exception ex)
                {
                    
                }
               
               
            }

            if (!mySQL.HasExited)
            {
                try
                {
                    foreach (Process proc in Process.GetProcessesByName("mysqld"))
                    {
                        proc.Kill();
                        proc.CloseMainWindow();
                        proc.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
              
            }
        }

        public void urlOpener_MouseLeftButtonUpStatic()
        {
            object sender = buttonSender;

            if (sender == null) return;

            //Button but = buttonSender as Button;
            //string ssss=but.Tag.ToString();
            // Save command execution logic...
            Button tB = sender as Button;

            string tbText = tB.Content.ToString();

            string urlText = "";

            int index = tbText.IndexOf(": ");

            if (index < 0)
                urlText = tbText;
            else
                urlText = tbText.Substring(index + 1);

            urlText = urlText.Trim();

            if (urlText.Length > 7)
            {
                Process.Start(new ProcessStartInfo(urlText));
                //e.Handled = true;
            }

        }//urlOpener_MouseLeftButtonUpsaveobject...

        private void urlOpener_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBox tB = sender as TextBox;

            string tbText = tB.Text;

            string urlText="";

            int index=tbText.IndexOf(": ");

            if (index < 0)
                urlText = tbText;
            else
                urlText = tbText.Substring(index + 1);

            urlText = urlText.Trim();

            if (urlText.Length > 7)
            {
                Process.Start(new ProcessStartInfo(urlText));
                e.Handled = true;
            }
            
        }//func...
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////
        // Encryption...

        #region LoginEncryption
        private static void EncryptConfigurationFile()
        {
            String executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            /*System.Configuration.*/Configuration config = /*System.Configuration.*/ConfigurationManager.OpenExeConfiguration(executablePath);
            
            System.Configuration.AppSettingsSection section = config.GetSection("appSettings") as System.Configuration.AppSettingsSection;
            if (!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
            }
        }

        private static void FetchMachineIpAndMacAddress()
        {
            //***UInt32 ipCode = 0;
            //***UInt64 macCode = 0;

            System.Net.IPAddress ipAddr = null;
            String hostName = System.Net.Dns.GetHostName();
            System.Net.IPAddress[] myIPs = System.Net.Dns.GetHostByName(hostName).AddressList;

            foreach (System.Net.IPAddress ip in myIPs)
            {
                if (ip.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork) continue;

                ipAddr = ip;
                ipId = (uint)System.Net.IPAddress.NetworkToHostOrder((int)ip.Address);
                break;
            }

            if (ipId == 0)
            {
                //Console.WriteLine("Error locating valid IPAddress");
                return;
            }


            byte[] macAddrBytes = new byte[8];
            foreach (System.Net.NetworkInformation.NetworkInterface n in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (n.OperationalStatus != System.Net.NetworkInformation.OperationalStatus.Up) continue;

                foreach (System.Net.NetworkInformation.UnicastIPAddressInformation uai in n.GetIPProperties().UnicastAddresses)
                {
                    if (uai.Address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork) continue;

                    if (uai.Address.Address.Equals(ipAddr.Address))
                    {
                        byte[] arr = n.GetPhysicalAddress().GetAddressBytes();
                        Array.Copy(arr, 0, macAddrBytes, 2, arr.Length);
                        Array.Reverse(macAddrBytes);
                        macId = BitConverter.ToUInt64(macAddrBytes, 0);
                        break;
                    }
                }
            }

            //***Console.WriteLine("IP Value\t" + ipCode + "\nMAC Value\t" + macCode + "\n\n");
        }

        private static void FetchMachineSID()
        {
            IntPtr logonToken = LogonUser();
            IntPtrConstructor(logonToken);
            IntPtrStringConstructor(logonToken);
            IntPtrStringTypeConstructor(logonToken);
            IntPrtStringTypeBoolConstructor(logonToken);
            UseProperties(logonToken);
        }

        private static void IntPtrConstructor(IntPtr logonToken) 
        {
            WindowsIdentity windowsIdentity = new WindowsIdentity(logonToken); 
        }

        private static void IntPtrStringConstructor(IntPtr logonToken)
        {
            String authenticationType = "WindowsAuthentication";
            WindowsIdentity windowsIdentity = new WindowsIdentity(logonToken, authenticationType);
        }

        private static void IntPtrStringTypeConstructor(IntPtr logonToken)
        {
            string authenticationType = "WindowsAuthentication";
            WindowsAccountType guestAccount = WindowsAccountType.Guest;
            WindowsIdentity windowsIdentity = new WindowsIdentity(logonToken, authenticationType, guestAccount);
        }

        private static void IntPrtStringTypeBoolConstructor(IntPtr logonToken)
        {
            String authenticationType = "WindowsAuthentication";
            WindowsAccountType guestAccount = WindowsAccountType.Guest;
            bool isAuthenticated = true;
            WindowsIdentity windowsIdentity = new WindowsIdentity(logonToken, authenticationType, guestAccount, isAuthenticated);
        }

        private static void UseProperties(IntPtr logonToken)
        {
            WindowsIdentity windowsIdentity = new WindowsIdentity(logonToken);
            string propertyDescription = "The Windows identity named ";

            // Retrieve the Windows logon name from the Windows identity object.
            propertyDescription += windowsIdentity.Name;

            // Verify that the user account is not considered to be an Anonymous 
            // account by the system. 
            if (!windowsIdentity.IsAnonymous)
            {
                propertyDescription += " is not an Anonymous account";
            }

            // Verify that the user account has been authenticated by Windows. 
            if (windowsIdentity.IsAuthenticated)
            {
                propertyDescription += ", is authenticated";
            }

            // Verify that the user account is considered to be a System account 
            // by the system. 
            if (windowsIdentity.IsSystem)
            {
                propertyDescription += ", is a System account";
            }
            // Verify that the user account is considered to be a Guest account 
            // by the system. 
            if (windowsIdentity.IsGuest)
            {
                propertyDescription += ", is a Guest account";
            }

            // Retrieve the authentication type for the 
            String authenticationType = windowsIdentity.AuthenticationType;

            // Append the authenication type to the output message. 
            if (authenticationType != null)
            {
                propertyDescription += (" and uses " + authenticationType);
                propertyDescription += (" authentication type.");
            }

            //Console.WriteLine(propertyDescription);

            // Display the SID for the owner.
            //Console.Write("\n\nThe SID for the owner is : ");
            SecurityIdentifier si = windowsIdentity.Owner;
            
            sId = si.ToString();

            //Console.WriteLine(si.ToString());
            //// Display the SIDs for the groups the current user belongs to.
            //Console.WriteLine("Display the SIDs for the groups the current user belongs to.");
            //IdentityReferenceCollection irc = windowsIdentity.Groups;
            //foreach (IdentityReference ir in irc)
            //    Console.WriteLine(ir.Value);
            //TokenImpersonationLevel token = windowsIdentity.ImpersonationLevel;
            //Console.WriteLine("The impersonation level for the current user is : " + token.ToString());
        }

        // Retrieve the account token from the current WindowsIdentity object 
        // instead of calling the unmanaged LogonUser method in the advapi32.dll. 
        private static IntPtr LogonUser()
        {
            IntPtr accountToken = WindowsIdentity.GetCurrent().Token;
           // Console.WriteLine("Token number is: " + accountToken.ToString());

            return accountToken;
        }

        // Get the WindowsIdentity object for an Anonymous user. 
        private static void GetAnonymousUser()
        {
            // Retrieve a WindowsIdentity object that represents an anonymous 
            // Windows user.
            WindowsIdentity windowsIdentity = WindowsIdentity.GetAnonymous();
        }

        // Impersonate a Windows identity. 
        private static void ImpersonateIdentity(IntPtr logonToken)
        {
            // Retrieve the Windows identity using the specified token.
            WindowsIdentity windowsIdentity = new WindowsIdentity(logonToken);

            // Create a WindowsImpersonationContext object by impersonating the 
            // Windows identity.
            WindowsImpersonationContext impersonationContext =
                windowsIdentity.Impersonate();

            //Console.WriteLine("Name of the identity after impersonation: "
             //   + WindowsIdentity.GetCurrent().Name + ".");
           // Console.WriteLine(windowsIdentity.ImpersonationLevel);
            // Stop impersonating the user.
            impersonationContext.Undo();

            // Check the identity name.
            //Console.Write("Name of the identity after performing an Undo on the");
            //Console.WriteLine(" impersonation: " +
            //    WindowsIdentity.GetCurrent().Name);
        }//func..
        #endregion
        ///////////////////////////////////////////////////////////////////////////////////////////

        #region Bookmark
        public bool alreadyBookmarked(String idInput, String typeInput, String socialMediaInput) 
        {
            bool result = false;

            String line = "";

            using (StreamReader sr = new StreamReader("bookmarks.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Trim().Length <= 10)
                        continue;

                    String name = "", type = "", socialMedia = "", id = "";

                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        name = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }

                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        type = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }
                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        socialMedia = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }
                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        id = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }

                    if (type == typeInput && socialMedia == socialMediaInput && id == idInput)
                    {
                        result = true; 
                        break;
                    }//if (type == typeInput && socialMedia == socialMediaInput && id == idInput)...
                    
                }//while...
            }//using...

            return result;

        }//func...

        public int removeBookmark(String idInput, String socialMediaInput) 
        {
            int result = -1;

            List<String> bookMarkedEntries = new List<string>();

            int counterrr = 0; String line = "";

            using (StreamReader sr = new StreamReader("bookmarks.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    //System.Console.WriteLine(line);
                    if (line.Trim().Length > 10)
                        counterrr++;
                    else
                        continue;

                    String name = "", type = "", socialMedia = "", id = "",line1=line;

                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        name = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }

                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        type = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }
                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        socialMedia = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }
                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        id = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }

                    if (id == idInput && socialMedia == socialMediaInput)
                    { result = counterrr-1; }
                    else  bookMarkedEntries.Add(line1);

                }//while...

            }//using...

            StreamWriter sW = new System.IO.StreamWriter("bookmarks.txt");
            
            foreach (String aBookmark in bookMarkedEntries) 
            {
                if (sW != null && aBookmark != null)
                    sW.WriteLine(aBookmark);
            }//foreach...

            if (sW != null)
                sW.Close();

            return result;
        }//func...

        public int getBookmarkIndex(String idInput, String socialMediaInput)
        {
            int result = -1;

            List<String> bookMarkedEntries = new List<string>();

            int counterrr = 0; String line = "";

            using (StreamReader sr = new StreamReader("bookmarks.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    //System.Console.WriteLine(line);
                    if (line.Trim().Length > 10)
                        counterrr++;
                    else
                        continue;

                    String name = "", type = "", socialMedia = "", id = "", line1 = line;

                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        name = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }

                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        type = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }
                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        socialMedia = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }
                    if (line.IndexOf(",.,.,.,.,.") >= 0)
                    {
                        id = line.Substring(0, line.IndexOf(",.,.,.,.,."));
                        line = line.Substring(line.IndexOf(",.,.,.,.,.") + 10);
                    }

                    if (id == idInput && socialMedia == socialMediaInput)
                    { result = counterrr-1; break; }
                    else continue;

                }//while...

            }//using...

            return result;
        }//func...

        #endregion

    }//end of class...


    public class CanvasShape : INotifyPropertyChanged
    {
        //is is all that the interface requires
        public event PropertyChangedEventHandler PropertyChanged;

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public double x1 { get; set; }
        public double y1 { get; set; }
        public double x2 { get; set; }
        public double y2 { get; set; }

        private String _type;
        public String type
        {
            get { return _type; }
            set
            {
                _type = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("type"));
            }
        }

        private String _textIfAny;
        public String textIfAny
        {
            get { return _textIfAny; }
            set
            {
                _textIfAny = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("textIfAny"));
            }
        }

        private String _ImageSource;
        public String ImageSource
        {
            get { return _ImageSource; }
            set
            {
                _ImageSource = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ImageSource"));
            }
        }

        private bool _imageVisibility;
        public bool imageVisibility
        {
            get { return _imageVisibility; }
            set
            {
                _imageVisibility = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("imageVisibility"));
            }
        }

        private bool _lineVisibility;
        public bool lineVisibility
        {
            get { return _lineVisibility; }
            set
            {
                _lineVisibility = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("lineVisibility"));
            }
        }

        private String _lineColor;
        public String lineColor
        {
            get { return _lineColor; }
            set
            {
                _lineColor = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("lineColor"));
            }
        }

        private String _textColor;
        public String textColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("textColor"));
            }
        }

        //public Geometry PathData { get; set; }//TODO: add change notification

    }//class...

}//end of namespace...