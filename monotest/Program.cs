using ppatierno.AzureSBLite;
using ppatierno.AzureSBLite.Messaging;
using System;
using System.Threading;

namespace monotest
{
	class MainClass
	{
		static string eventHubName = "snappy";
		static string connectionString = "Endpoint=sb://demohub.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=BOLIBIGWLoIOk1rVy+DqhKS2mnAgx33qCQQuMz03KLg=";

		public static void Main (string[] args)
		{
			Console.WriteLine("Press Ctrl-C to stop the sender process");
			Console.WriteLine("Press Enter to start now");
			Console.ReadLine();
			SendingRandomMessages();
		}

		static void SendingRandomMessages()
		{
			MessagingFactory factory = MessagingFactory.CreateFromConnectionString(connectionString); 
			EventHubClient client = factory.CreateEventHubClient(eventHubName); 

			while (true)
			{
				try
				{
					var message = Guid.NewGuid().ToString();
					Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, message);
					client.Send(new EventData(System.Text.Encoding.UTF8.GetBytes(message)));
				}
				catch (Exception exception)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
					Console.ResetColor();
				}

				Thread.Sleep(200);
			}
		}
	}
}
