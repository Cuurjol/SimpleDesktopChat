using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private readonly List<ServerUser> _serverUsers = new List<ServerUser>();
        private int _nextUserId = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser
            {
                Id = _nextUserId,
                Name = name,
                OperationContext = OperationContext.Current
            };

            _nextUserId++;

            SendMessage(user.Name + " подключился к чату.", 0);
            _serverUsers.Add(user);

            return user.Id;
        }

        public void Disconnect(int id)
        {
            ServerUser user = _serverUsers.Find(c => c.Id == id);
            if (user != null)
            {
                _serverUsers.Remove(user);
                SendMessage(user.Name + " покинул чат.", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach (ServerUser serverUser in _serverUsers)
            {
                string answer = DateTime.Now.ToShortTimeString();

                ServerUser user = _serverUsers.Find(c => c.Id == id);
                if (user != null)
                {
                    answer = user.Name + $" ({answer}): " + message;
                    serverUser.OperationContext.GetCallbackChannel<IServiceChatCallBack>().MessageCallBack(answer);
                }
                else
                {
                    answer +=": " + message;
                    serverUser.OperationContext.GetCallbackChannel<IServiceChatCallBack>().MessageCallBack(answer);
                }
            }
        }
    }
}
