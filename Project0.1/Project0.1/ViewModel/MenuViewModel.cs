using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Shapes;


namespace Project0._1.ViewModel
{
    

    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MenuViewModel : ViewModelBase
    {
        //定义端口设置窗口
        public static NewSettingsWindow newWinow;

        //窗口
        private MainWindow owner;


        //所有属性
        /// <summary>
        /// The <see cref="NewFileName" /> property's name.
        /// </summary>
        public const string NewFileNamePropertyName = "NewFileName";

        private string newFileName = "";

        /// <summary>
        /// Sets and gets the NewFileName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string NewFileName
        {
            get
            {
                return newFileName;
            }

            set
            {
                if (newFileName == value)
                {
                    return;
                }

                newFileName = value;
                RaisePropertyChanged(NewFileNamePropertyName);
            }
        }


        /// <summary>
        /// The <see cref="TxtContent" /> property's name.
        /// </summary>
        public const string TxtContentPropertyName = "TxtContent";

        private string txtContent = "";

        /// <summary>
        /// Sets and gets the TxtContent property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TxtContent
        {
            get
            {
                return txtContent;
            }

            set
            {
                if (txtContent == value)
                {
                    return;
                }

                txtContent = value;
                RaisePropertyChanged(TxtContentPropertyName);
            }
        }


        //所有命令
        private RelayCommand addNewFileCommand;

        /// <summary>
        /// Gets the AddNewFile.
        /// </summary>
        public RelayCommand AddNewFileCommand
        {
            get
            {
                return addNewFileCommand ?? (addNewFileCommand = new RelayCommand(
                    ExecuteAddNewFileCommand,
                    CanExecuteAddNewFileCommand));
            }
        }

        private void ExecuteAddNewFileCommand()
        {
            if (!AddNewFileCommand.CanExecute(null))
            {
                return;
            }
            //注意Mode 和 UpdateSourceTrigger
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Title = "新建";
            ofd.DefaultExt = ".txt";
            ofd.CheckFileExists = false;
            ofd.CheckPathExists = false;
            ofd.Filter = "文本文件|*.txt";

            Nullable<bool> ofdResult = ofd.ShowDialog(owner);
            string fileName = ofd.FileName;

            if (ofdResult == true && fileName != null)
            {

                File.Create(fileName);
                NewFileName = fileName;

            }
            else if (fileName == null)
            {
                MessageBox.Show("文件名不能为空");
            }

            newWinow = new NewSettingsWindow();
            newWinow.Show();
            


             
        }

        private bool CanExecuteAddNewFileCommand()
        {
            return true;
        }


        private RelayCommand saveCommand;

        /// <summary>
        /// Gets the SaveCommand.
        /// </summary>
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(
                    ExecuteSaveCommand,
                    CanExecuteSaveCommand));
            }
        }

        private void ExecuteSaveCommand()
        {
            if (this.NewFileName!=null)
            {

               

                FileStream filestream = new FileStream(NewFileName, FileMode.Create);
                byte[] bytedata;
                UnicodeEncoding txt = new UnicodeEncoding();
                bytedata = txt.GetBytes(this.TxtContent);

                filestream.Seek(0, SeekOrigin.Begin);
                filestream.Write(bytedata,0,bytedata.Length);

            }
   
            
        }

        private bool CanExecuteSaveCommand()
        {
            return true;
        }


        private RelayCommand saveAnotherPlaceCommand;

        /// <summary>
        /// Gets the SaveAnotherPlaceCommand.
        /// </summary>
        public RelayCommand SaveAnotherPlaceCommand
        {
            get
            {
                return saveAnotherPlaceCommand ?? (saveAnotherPlaceCommand = new RelayCommand(
                    ExecuteSaveAnotherPlaceCommand,
                    CanExecuteSaveAnotherPlaceCommand));
            }
        }

        private void ExecuteSaveAnotherPlaceCommand()
        {
            if (!SaveAnotherPlaceCommand.CanExecute(null))
            {
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文本文件|*.txt";
            sfd.ShowDialog(owner);
            string filename = sfd.FileName;
            
            if (filename != null)
            {
                FileStream filestream = new FileStream(filename, FileMode.CreateNew);
                byte[] bytedata;
                UnicodeEncoding txt = new UnicodeEncoding();
                bytedata = txt.GetBytes(this.TxtContent.ToString());

                filestream.Seek(0, SeekOrigin.Begin);
                filestream.Write(bytedata, 0, bytedata.Length);

            }

            
        }

        private bool CanExecuteSaveAnotherPlaceCommand()
        {
            return true;
        }



        private RelayCommand openFileCommand;

        /// <summary>
        /// Gets the OpenFileCommand.
        /// </summary>
        public RelayCommand OpenFileCommand
        {
            get
            {
                return openFileCommand ?? (openFileCommand = new RelayCommand(
                    ExecuteOpenFileCommand,
                    CanExecuteOpenFileCommand));
            }
        }

        private void ExecuteOpenFileCommand()
        {
            if (!OpenFileCommand.CanExecute(null))
            {
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.InitialDirectory = "C:\\";
            ofd.Multiselect = false;
            ofd.ShowDialog();
            
        }

        private bool CanExecuteOpenFileCommand()
        {
            return true;
        }

        
        private RelayCommand closeFileCommand;

        /// <summary>
        /// Gets the CloseFileCommand.
        /// </summary>
        public RelayCommand CloseFileCommand
        {
            get
            {
                return closeFileCommand ?? (closeFileCommand = new RelayCommand(
                    ExecuteCloseFileCommand,
                    CanExecuteCloseFileCommand));
            }
        }

        private void ExecuteCloseFileCommand()
        {
            if (!CloseFileCommand.CanExecute(null))
            {
                return;
            }
            MessageBox.Show(TxtContent);
            
        }

        private bool CanExecuteCloseFileCommand()
        {
            return true;
        }


       


        /// <summary>
        /// Initializes a new instance of the MenuViewModel class.
        /// </summary>
        public MenuViewModel()
        {
            owner = MainWindow.owner;
          
        }
    }
}