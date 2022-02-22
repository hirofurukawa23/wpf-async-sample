using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncSampleWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 同期処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NormalStart_Click(object sender, RoutedEventArgs e)
        {
            SetStatus(StatusType.Start, "同期");
            Debug.WriteLine($"Button Click / ID：{ Thread.CurrentThread.ManagedThreadId }");

            //疑似的な重い処理を同期的に実行する
            HeavyDuty();

            SetStatus(StatusType.Done, "同期");
            Debug.WriteLine($"Button Click / ID：{ Thread.CurrentThread.ManagedThreadId }");
        }

        /// <summary>
        /// 非同期処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AsyncStart_Click(object sender, RoutedEventArgs e)
        {
            SetStatus(StatusType.Start, "非同期");
            Debug.WriteLine($"Button Click / ID：{ Thread.CurrentThread.ManagedThreadId }");

            //疑似的な重い処理を非同期的に実行する
            await Task.Run(() => HeavyDuty());

            Debug.WriteLine($"Button Click / ID：{ Thread.CurrentThread.ManagedThreadId }");
            SetStatus(StatusType.Done, "非同期");
        }

        /// <summary>
        /// ステータスクリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearStatus_Click(object sender, RoutedEventArgs e)
        {
            this.Status.Text = "";
        }

        /// <summary>
        /// 疑似的な重い処理
        /// </summary>
        private void HeavyDuty()
        {
            Debug.WriteLine($"HeavyDuty / ID：{ Thread.CurrentThread.ManagedThreadId }");
            Thread.Sleep(5000);
        }

        /// <summary>
        /// ステータスタイプ
        /// </summary>
        private enum StatusType
        {
            Start, Done
        }

        /// <summary>
        /// ステータスを設定する
        /// </summary>
        /// <param name="input"></param>
        /// <param name="type"></param>
        private void SetStatus(StatusType input, string type)
        {
            switch ((int)input)
            {
                case (int)StatusType.Start:
                    this.Status.Text = $"{type}処理を開始します。";
                    break;
                case (int)StatusType.Done:
                    this.Status.Text = $"{type}処理が完了しました。";
                    break;
                default: break;
            }
        }
    }
}
