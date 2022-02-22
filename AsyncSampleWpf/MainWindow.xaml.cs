using System.Collections.Generic;
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
            Debug.WriteLine($"Button Click Start / ID：{ Thread.CurrentThread.ManagedThreadId }");

            //疑似的な重い処理を同期的に実行する
            HeavyDuty();

            SetStatus(StatusType.Done, "同期");
            Debug.WriteLine($"Button Click End / ID：{ Thread.CurrentThread.ManagedThreadId }");
        }

        /// <summary>
        /// 非同期処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AsyncStart_Click(object sender, RoutedEventArgs e)
        {
            SetStatus(StatusType.Start, "非同期");
            Debug.WriteLine($"Button Click Start / ID：{ Thread.CurrentThread.ManagedThreadId }");

            //疑似的な重い処理を非同期的に実行する
            await Task.Run(() => HeavyDuty());

            Debug.WriteLine($"Button Click End / ID：{ Thread.CurrentThread.ManagedThreadId }");
            SetStatus(StatusType.Done, "非同期");
        }

        private async void AsyncStart2_Click(object sender, RoutedEventArgs e)
        {
            SetStatus(StatusType.Start, "非同期２");
            Debug.WriteLine($"Button Click Start / ID：{ Thread.CurrentThread.ManagedThreadId }");

            //疑似的な重い処理を非同期的に実行する
            await DoDuties();

            Debug.WriteLine($"Button Click End / ID：{ Thread.CurrentThread.ManagedThreadId }");
            SetStatus(StatusType.Done, "非同期２");
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
            Debug.WriteLine($"{nameof(HeavyDuty)} / ID：{ Thread.CurrentThread.ManagedThreadId }");
            Thread.Sleep(5000);
        }

        /// <summary>
        /// 疑似的な重い処理
        /// </summary>
        private void HeavyDuty2()
        {
            Debug.WriteLine($"{nameof(HeavyDuty2)} / ID：{ Thread.CurrentThread.ManagedThreadId }");
            Thread.Sleep(7000);
        }

        /// <summary>
        /// 複数の疑似的な重い処理を並列で実行
        /// </summary>
        /// <returns></returns>
        private async Task DoDuties()
        {
            Debug.WriteLine($"{nameof(DoDuties)} / ID：{ Thread.CurrentThread.ManagedThreadId }");

            var tasks = new List<Task>();
            for (var i = 1; i <= 3; i++)
            {
                var x = i;
                tasks.Add(Task.Run(() => HeavyDuty()));
            }
            await Task.WhenAll(tasks);
            Debug.WriteLine($"{nameof(DoDuties)}が完了しました。");
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
