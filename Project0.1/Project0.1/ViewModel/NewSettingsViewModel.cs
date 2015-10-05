using GalaSoft.MvvmLight;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Collections;
using System;
using MvvmLight1.Model;



namespace Project0._1.ViewModel
{
    
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class NewSettingsViewModel : ViewModelBase
    {

        //private MainWindow owner;
        private  List<List<Rectangle>> rectList;
        private List<TextBox> tboxList;
        private List<int> inputList;
        private List<List<int>> tagList;
        //private int one;
        //private int two;
        //private int three;
        //private int four;
        //private int five;
        //private int six;
        //private int seven;
        //private int eight;
       
        private int portNumbers;
        private int[,] tag;
        private double rectWidth;
        private double rectHeight;
        private double rectsTopSpace;
        private double rectsLeftSpace;
      

      


        //Combox中的选择项值
        public List<int> NewList { get; set; }
        /// <summary>
        /// The <see cref="ChoosedItem" /> property's name.
        /// </summary>
        public const string ChoosedItemPropertyName = "ChoosedItem";

        private string choosedItem = "";

        /// <summary>
        /// Sets and gets the ChoosedItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ChoosedItem
        {
            get
            {
                return choosedItem;
            }

            set
            {
                if (choosedItem == value)
                {
                    return;
                }

                choosedItem = value;
                RaisePropertyChanged(ChoosedItemPropertyName);
            }
        }
        

        public void CreateNewList()
        {
            NewList = new List<int>();

            for (int i = 1; i < 11; i++)
            {

                NewList.Add(i);
            }

        }

        //运行操作命令
        private RelayCommand runCommand;

        /// <summary>
        /// Gets the RunCommand.
        /// </summary>
        public RelayCommand RunCommand
        {
            get
            {
                return runCommand ?? (runCommand = new RelayCommand(
                    ExecuteRunCommand,
                    CanExecuteRunCommand));
            }
        }

        private void ExecuteRunCommand()
        {
            if (!RunCommand.CanExecute(null))
            {
                return;
            }

            GetInputBoxNumbers();
            CalculatorAndShowContactsCondition();

        }

        private bool CanExecuteRunCommand()
        {
            return true;
        }


        //撤销操作命令
        private RelayCommand revokeCommand;

        /// <summary>
        /// Gets the RevokeCommand.
        /// </summary>
        public RelayCommand RevokeCommand
        {
            get
            {
                return revokeCommand ?? (revokeCommand = new RelayCommand(
                    ExecuteRevokeCommand,
                    CanExecuteRevokeCommand));
            }
        }

        private void ExecuteRevokeCommand()
        {
            if (!RevokeCommand.CanExecute(null))
            {
                return;
            }

            ShowRectangles(portNumbers);
            ClearInputBox();
            SetOriginalContacts(tag);
        }

        private bool CanExecuteRevokeCommand()
        {
            return true;
        }




        //下一步操作命令
        private RelayCommand nextStep;

        /// <summary>
        /// Gets the NextStep.
        /// </summary>
        public RelayCommand NextStep
        {
            get
            {
                return nextStep
                    ?? (nextStep = new RelayCommand(ExecuteNextStep));
            }
        }
        
        private void ExecuteNextStep()
        {
            tag = new int[,] { { 0, 1, 2, 3, 4, 5, 6, 7 }, { 0, 2, 4, 6, 1, 3, 5, 7 },
                { 0, 2, 1, 3, 4, 6, 5, 7 }, { 0, 2, 1, 3, 4, 6, 5, 7 } };
            //tag = new int[,] { { 0,4,1,5,2,6,3,7}, { 0, 2, 1, 3, 4, 6, 5, 7 },
            //    { 0, 2, 1, 3, 4, 6, 5, 7 }, { 0,2,4,6,1,3,5,7} };
            string ci = ChoosedItem;
            int numbers = int.Parse(ci);
            portNumbers = numbers;
            ShowRectangles(numbers);
            ShowInputBox();
            SetOriginalContacts(tag);
            MenuViewModel.newWinow.Close();

        }


        //取消操作命令
        private RelayCommand cancel;

        /// <summary>
        /// Gets the Cancel.
        /// </summary>
        public RelayCommand Cancel
        {
            get
            {
                return cancel
                    ?? (cancel = new RelayCommand(ExecuteCancel));
            }
        }

        private void ExecuteCancel()
        {
            MenuViewModel.newWinow.Close();
        }





        
        //定义并显示所有输入Box
        private void ShowInputBox()
        {
            tboxList = new List<TextBox>();

            for (int i = 0; i < portNumbers*2; i++)
            {
                TextBox tbox = new TextBox();
                tbox.Width=rectWidth;
                tbox.Height=rectHeight/3;
                tbox.SetValue(Canvas.LeftProperty,(double)10);
                tbox.SetValue(Canvas.TopProperty, (double)((i + 2)*(rectsTopSpace/2)));

                tboxList.Add(tbox);
                MainWindow.owner.canvas.Children.Add(tbox);

            }

        }


        //获得输入值
        private void GetInputBoxNumbers()
        {
            inputList=new List<int> ();
            //或者通过设置一个 IEnumerator对象  IEnumerator IE= tboxList.GetEnumerator();
            //然后调用IEnumerator接口的方法函数，来依次访问每个对象
            for (int i = 0; i < tboxList.Count; i++)
            {
                inputList.Add(Convert.ToInt32(tboxList[i].Text));

            }
            

        }
        

        //清空所有Tbox
        private void ClearInputBox()
        {
            for (int i = 0; i < tboxList.Count; i++)
            {
                tboxList[i].Clear();
            }

        }
        


        //计算并显示不同的连接状况
        private void CalculatorAndShowContactsCondition()
        {
            //初始化tagList
            tagList=new List<List<int>>();
            for (int i = 0; i < portNumbers; i++)
            {
                List<int> tlist = new List<int>();
                for (int j = 0; j < portNumbers+1; j++)
                {
                    tlist.Add(new int());
                }
                tagList.Add(tlist);
            }


            TransportBean tr = new TransportBean(inputList[0], inputList[1], inputList[2],
                    inputList[3], inputList[4], inputList[5], inputList[6], inputList[7]);
            main ma = new main();
            Switch[,] sw = new Switch[portNumbers, portNumbers+1];
            int[] Export = new int[8];
            int[] outport = new int[8];

            for (int i = 0; i < portNumbers; i++)
                for (int j = 0; j < portNumbers+1; j++)
                {
                    sw[i, j] = new Switch();
                    sw[i, j].setFlag(-1);
                }

            outport[0] = tr.getOne();
            outport[1] = tr.getTwo();
            outport[2] = tr.getThree();
            outport[3] = tr.getFour();
            outport[4] = tr.getFive();
            outport[5] = tr.getSix();
            outport[6] = tr.getSeven();
            outport[7] = tr.getEight();

            ma.TestExport(outport);
            for (int i = 0; i < 8; i++)
            {
                Export[outport[i]] = i;
            }
            ma.SetSwitch(Export, outport, sw, 0, tag, portNumbers);

            //getFlag()用来获取是直通还是交叉
            for (int i = 0; i < portNumbers; i++)
            {
                for (int j = 0; j < portNumbers+1; j++)
                {
                    tagList[i][j]=sw[i, j].getFlag();
                }
            }

            for (int i = 0; i < portNumbers; i++)
            {
                for (int j = 0; j < portNumbers+1; j++)
                {
                    SetRectanglesStatus(tagList[i][j], rectList[i][j]);
                }
                
            }

        }
        


        //定义并显示所有矩形
        private void ShowRectangles(int numbers)
        {
            //定义List的二维数组
            rectList = new List<List<Rectangle>>();

            for (int i = 0; i < numbers; i++)
            {
                List<Rectangle> rect = new List<Rectangle>();
                for (int j = 0; j < numbers+1; j++)
                {
                    rect.Add(new Rectangle());
                }
                rectList.Add(rect);
            }

            for (int i = 0; i < numbers; i++)
            {
                for (int j = 0; j < numbers+1; j++)
                {
                    ShowRectanglesInCanvas(rectList[i][j], i, j);
                }
                
            }

        }

         //在画布上显示一个矩形
        private  void ShowRectanglesInCanvas(Rectangle rect, int i,int j)
        {
            rect.Width = rectWidth;
            rect.Height = rectHeight;
            rect.Fill = new SolidColorBrush(Colors.LightBlue);
            rect.SetValue(Canvas.TopProperty, (double)((i+1) * rectsTopSpace));//j
            rect.SetValue(Canvas.LeftProperty, (double)((j+1) * rectsLeftSpace));//i
            //后台设置style
            rect.SetResourceReference(Rectangle.StyleProperty, "myRectangle");
            MainWindow.owner.canvas.Children.Add(rect);

        }


        //设置初始Benes网络的连接
        private void SetOriginalContacts(int[,] tag )
        {
             
            //List<int> taglist = new List<int>();
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 8; j++)
            //    {
                  

            //       taglist.Add(tag[i, j]);

            //    }

            //    CreateContactsBetweenTwoFloors(taglist, i);
               
            //    taglist.Clear();
            //}

            int[] taglist=new int[8];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    taglist[j] = tag[i,j];
                    
                }
                CreateContactsBetweenTwoFloors(taglist, i);
              
            }

        }

        
        //每一层级间的端口线段连接
        private void CreateContactsBetweenTwoFloors(int[] tag,int floors)
        {

                for (int j = 0; j < 8; j++)
                {
                    Line line = new Line();
                    line.Stroke = new SolidColorBrush(Colors.Black);
                    line.SetValue(Canvas.TopProperty, (double)rectList[j/2][floors].GetValue(Canvas.TopProperty));
                    line.SetValue(Canvas.LeftProperty, (double)rectList[j/2][floors].GetValue(Canvas.LeftProperty));
                    line.X1 = rectList[j/2][floors].Width;
                    line.Y1 = ((j % 2 == 0) ? (rectList[j/2][floors].Height / 6) : (5 * rectList[j/2][floors].Height / 6));

                    //下一层 
                    line.X2 = (double)rectList[tag[j]/2][floors + 1].GetValue(Canvas.LeftProperty)
                        - (double)rectList[j/2][floors].GetValue(Canvas.LeftProperty);

                    line.Y2 = (double)rectList[tag[j]/2][floors + 1].GetValue(Canvas.TopProperty)
                        - (double)rectList[j/2][floors].GetValue(Canvas.TopProperty)
                        + ((tag[j] % 2 == 0) ? (rectList[tag[j]/2][floors + 1].Height / 6) : (5 * rectList[tag[j]/2][floors + 1].Height / 6));

                   MainWindow.owner.canvas.Children.Add(line);

                }


        }
        

        //设置直通和交换的rectangle中的不同显示
        private void SetRectanglesStatus(int flag, Rectangle rect)
        {

            Line line1 = new Line();
            line1.Stroke = new SolidColorBrush(Colors.Black);
            line1.SetValue(Canvas.TopProperty, (double)rect.GetValue(Canvas.TopProperty));
            line1.SetValue(Canvas.LeftProperty, (double)rect.GetValue(Canvas.LeftProperty));

            Line line2 = new Line();
            line2.Stroke = new SolidColorBrush(Colors.Black);
            line2.SetValue(Canvas.TopProperty, (double)rect.GetValue(Canvas.TopProperty));
            line2.SetValue(Canvas.LeftProperty, (double)rect.GetValue(Canvas.LeftProperty));

            //交换
            if (flag == 1)
            {
                line1.X1 = 0;
                line1.Y1 = rect.Height / 6;
                line1.X2 = rect.Width;
                line1.Y2 = (rect.Height * 5) / 6;

                line2.X1 = 0;
                line2.Y1 = (rect.Height * 5) / 6;
                line2.X2 = rect.Width;
                line2.Y2 = rect.Height / 6;

                MainWindow.owner.canvas.Children.Add(line1);
                MainWindow.owner.canvas.Children.Add(line2);  

            }
            //直通
            else if (flag == 0)
            {


                line1.X1 = 0;
                line1.Y1 = rect.Height / 6;
                line1.X2 = rect.Width;
                line1.Y2 = rect.Height / 6;

                line2.X1 = 0;
                line2.Y1 = (rect.Height * 5) / 6;
                line2.X2 = rect.Width;
                line2.Y2 = (rect.Height * 5) / 6;

                MainWindow.owner.canvas.Children.Add(line1);
                MainWindow.owner.canvas.Children.Add(line2);

            }
        }

        //设置矩形等等的长度，宽度，间距初始值

        private void InitializeSettings()
        {
            rectHeight = 60;
            rectWidth = 60;
            rectsLeftSpace = 80;
            rectsTopSpace = 80;

        }
        


     
        /// <summary>
        /// Initializes a new instance of the NewSettingsViewModel class.
        /// </summary>
        public NewSettingsViewModel()
        {
            
            CreateNewList();
            InitializeSettings();
           
            

        }




      
    }
}