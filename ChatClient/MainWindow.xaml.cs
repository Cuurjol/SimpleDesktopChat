using System.Windows;
using System.Windows.Input;
using System.ServiceModel;
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IServiceChatCallback
    {
        private bool _isConnected;
        private ServiceChatClient _chatClient;
        private int _userId;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void ConnectUser()
        {
            if (!_isConnected)
            {
                _chatClient = new ServiceChatClient(new InstanceContext(this));
                _userId = _chatClient.Connect(UserNameTextBox.Text);
                _isConnected = true;
                UserNameTextBox.IsEnabled = false;
                MessageTextBox.IsEnabled = true;
                ConnectOrDisconnectButton.Content = "Покинуть чат";
            }
        }

        private void DisconnectUser()
        {
            if (_isConnected)
            {
                _chatClient.Disconnect(_userId);
                _chatClient = null;
                _isConnected = false;
                UserNameTextBox.IsEnabled = true;
                UserNameTextBox.Text = "";
                MessageTextBox.IsEnabled = false;
                MessageTextBox.Text = "";
                ConnectOrDisconnectButton.Content = "Присоединиться к чату";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _chatClient?.SendMessage(MessageTextBox.Text, _userId);
                MessageTextBox.Text = string.Empty;
            }
        }

        public void MessageCallBack(string message)
        {
            ChatListBox.Items.Add(message);
            ChatListBox.ScrollIntoView(ChatListBox.Items[ChatListBox.Items.Count - 1]);
        }
    }
}
