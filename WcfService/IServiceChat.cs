using System.ServiceModel;

namespace WcfService
{
    [ServiceContract(CallbackContract = typeof(IServiceChatCallBack))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name); // Метод подключения к WCF сервису, когда пользователь присоединяется к чату

        [OperationContract]
        void Disconnect(int id); // Метод отключения от WCF сервиса, когда пользователь покидает чата

        [OperationContract(IsOneWay = true)] // Метод SendMessage не дожидается ответа от WCF сервера
        void SendMessage(string message, int id); // Метод отправки сообщения клиента в чате
    }

    
    [ServiceContract]
    public interface IServiceChatCallBack
    {
        [OperationContract(IsOneWay = true)] // Метод MessageCallBack не дожидается ответа от WCF сервера
        void MessageCallBack(string message);
    }
}
