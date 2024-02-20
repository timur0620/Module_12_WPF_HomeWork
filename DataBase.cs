using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace Module_12_WPF_HomeWork
{
    class DataBase
    {
        private string GetPathClient()
        {
            return "C:\\Users\\User\\source\\repos\\Module_12_WPF_HomeWork\\DB\\clientBase.csv";
        }
        private string GetPathAccount()
        {
            return "C:\\Users\\User\\source\\repos\\Module_12_WPF_HomeWork\\DB\\accountBase.csv";
        }
        public void AddFakeClient(int countClients)
        {
            using (StreamWriter sw = new StreamWriter(GetPathClient()))
            {
                for (int i = 1; i < countClients; i++)
                {
                    var faker = new Faker();
                    Random rand = new Random();

                    sw.WriteLine($"{i}|" +
                                $"{faker.Person.LastName}|" +
                                $"{faker.Person.FirstName}|" +
                                $"{faker.Person.UserName}|" +
                                $"{0}|");
                }
            }
        }
        public void AddFakeAccount(int countAccount)
        {
            using (StreamWriter sw = new StreamWriter(GetPathAccount()))
            {
                for (int i = 1; i < countAccount; i++)
                {
                    var faker = new Faker();
                    Random rand = new Random();

                    sw.WriteLine($"{i}|" +
                                $"{rand.Next(100_000_000, 9_000_000_00)}|" +
                                $"{rand.Next(-10_000, 100_000)}|");
                }
            }
        }
        private List<string> GetAllInDB(string name)
        {
            string path;
            List<string> allData = new List<string>();
            if (name.Equals("accounts"))
            {
                path = GetPathAccount();
            }
            else if (name.Equals("clients"))
            {
                path = GetPathClient();
            }
            else
            {
                return allData;
            }
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";

                while ((s = sr.ReadLine()) != null)
                {

                    allData.Add(s);
                }
            }
            return allData;
        }
        public Client<uint, string, string, string, uint> GetOneClient(int idData)
        {
            List <Client<uint, string, string, string, uint>> clients = GetClients();
            foreach (Client<uint, string, string, string, uint> client in clients)
            {
                if (client.idClient == idData)
                {
                    return client;
                }
            }
            return clients[0];
        }
        public BankAccount<uint, long, uint> GetOneAccount(int idData)
        {
            List<BankAccount<uint, long, uint>> accounts = GetAccounts();
            foreach (BankAccount<uint, long, uint> account in accounts)
            {
                if (account.idAccount == idData)
                {
                    return account;
                }
            }
            return accounts[0];
        }
        public uint GetCurrentIdInDB(string name)
        {   
            List <string> db = GetAllInDB(name);
 
            HashSet<int> idHash = new HashSet<int>();

            uint idCurrent = (uint)db.Count;

            foreach (string client in db)
            {
                string[] tempCl = client.Split('|');

                idHash.Add(int.Parse(tempCl[0]));
            }
            while (true)
            {
                idCurrent++;

                if (!idHash.Contains((int)idCurrent))
                {
                    return idCurrent;
                }
                continue;
            }
        }
        private List<Client<uint, string, string, string, uint>> ClientTransformDB(List<string> allClientDB)
        {
            int index = 0;
            List<Client<uint, string, string, string, uint>> consList = new List<Client<uint, string, string, string, uint>>();

            foreach (string item in allClientDB)
            {
                Client<uint, string, string, string, uint> client = new Client<uint, string, string, string, uint>();
                string[] tempMassive = new string[item.Split('|').Length];
                tempMassive = item.Split('|');

                client.idClient = uint.Parse(tempMassive[0]);
                client.lastName = tempMassive[1];
                client.name = tempMassive[2];
                client.surname = tempMassive[3];
                client.bankAccountID = uint.Parse(tempMassive[4]);
                consList.Add(client);

                index++;
            }
            return consList;
        }
        private List<BankAccount<uint, long, uint>> AccountTransformDB(List<string> allAccount)
        {
            List<BankAccount<uint, long, uint>> consList = new List<BankAccount<uint, long, uint>>();

            foreach (string item in allAccount)
            {
                BankAccount<uint, long, uint> account = new BankAccount<uint, long, uint>();
                string[] tempMassive = new string[item.Split('|').Length];
                tempMassive = item.Split('|');

                account.idAccount = uint.Parse(tempMassive[0]);
                account.accountNumber = long.Parse(tempMassive[1]);
                account.totalSum = uint.Parse(tempMassive[2]);
                consList.Add(account);
            }
            return consList;
        }
        public List<Client<uint, string, string, string, uint>> GetClients()
        {
            return ClientTransformDB(GetAllInDB("clients"));
        }
        public List<BankAccount<uint, long, uint>> GetAccounts()
        {
            return AccountTransformDB(GetAllInDB("accounts"));
        }
        public void RecordFileClients(List<Client<uint, string, string, string, uint>> allClients)
        {
            using (StreamWriter sw = new StreamWriter(GetPathClient()))
            {
                for (int i = 0; i < allClients.Count; i++)
                {
                    sw.WriteLine($"{allClients[i].idClient}|" +
                                $"{allClients[i].lastName}|" +
                                $"{allClients[i].name}|" +
                                $"{allClients[i].surname}|" +
                                $"{allClients[i].bankAccountID}|");
                }
            }
        }
        public void AppendAccountDB(BankAccount<uint, long, uint> account)
        {
            using (StreamWriter sw = File.AppendText(GetPathAccount()))
            {
                sw.WriteLine($"{account.idAccount}|{account.accountNumber}|{account.totalSum}|");
            }
        }
        public void RecordFileAccounts(List<BankAccount<uint, long, uint>> allAccounts)
        {
            using (StreamWriter sw = new StreamWriter(GetPathAccount()))
            {
                for (int i = 0; i < allAccounts.Count; i++)
                {
                    sw.WriteLine($"{allAccounts[i].idAccount}|" +
                                 $"{allAccounts[i].accountNumber}|" +
                                 $"{allAccounts[i].totalSum}|");        
                }
            }
        }
    }
}
