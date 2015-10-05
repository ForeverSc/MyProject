using System.Windows;
using Project0._1.ViewModel;
using System.Windows.Controls;
using System;
using System.Windows.Interop;

namespace Project0._1
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //通过全局变量的方法，将
        public static MainWindow owner;    
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        { 
           
            InitializeComponent();
            owner = this;
            Closing += (s, e) => ViewModelLocator.Cleanup();

           
        }

        
    }
}