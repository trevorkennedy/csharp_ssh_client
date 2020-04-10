using System;
using System.IO;
using Renci.SshNet;

namespace sshClient
{
    class Program
    {
        public static ConnectionInfo CreateConnectionInfo()
        {
            const string privateKeyFilePath = "../../../Lightsail.pem";
            ConnectionInfo connectionInfo;
            using (var stream = new FileStream(privateKeyFilePath, FileMode.Open, FileAccess.Read))
            {
                var privateKeyFile = new PrivateKeyFile(stream);
                AuthenticationMethod authenticationMethod = new PrivateKeyAuthenticationMethod("bitnami", privateKeyFile);
                connectionInfo = new ConnectionInfo("3.80.245.253", "bitnami", authenticationMethod);
            }

            return connectionInfo;
        }

        public static void Connect()
        {
            using (var ssh = new SshClient(CreateConnectionInfo()))
            {
                ssh.Connect();
                var command = ssh.CreateCommand("uptime");
                var result = command.Execute();
                Console.WriteLine(result);
                ssh.Disconnect();
            }
        }

        static void Main(string[] args)
        {
            Connect();
            Console.WriteLine("Done");
        }
    }
}
