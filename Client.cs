using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Module_12_WPF_HomeWork
{
    class Client <T1, T2, T3, T4, T5>: DataBase 
    {
        public T1 idClient { get; set; }
        public T2 lastName { get; set; }
        public T3 name { get; set; }
        public T4 surname { get; set; }
        public T5 bankAccountID {  get; set; }
        public List<Client<uint, string, string, string, uint>> clList { get; set; }
        public Client<uint, string, string, string, uint> this[int index]
        {
            get { return this.clList[index]; }
        }
        public Client()
        {

        }
        public Client(T1 id, T2 lastName, T3 name, T4 surname, T5 account)
        {
            this.idClient = id;
            this.lastName = lastName;
            this.name = name;
            this.surname = surname;
            this.bankAccountID = account;
        }
        public override string ToString()
        {
            return $"{idClient} {lastName} {name} {surname} {bankAccountID}";
        }
        public List<Client<uint, string, string, string, uint>> SearchClient(string lastNameClient)
        {
            List<Client<uint, string, string, string, uint>> clList = new List<Client<uint, string, string, string, uint>>();
            Client<uint, string, string, string, uint> cl = new Client<uint, string, string, string, uint>();

            foreach (Client<uint, string, string, string, uint> client in cl.GetClients())
            {
                if (client.lastName == lastNameClient)
                {
                    clList.Add(client);

                    return clList;
                }
            }
            return clList;
        }
        public void AddIdAccountClient(string idClient, BankAccount<uint, long, uint> newAccount)
        {
            List<Client<uint, string, string, string, uint>> clList = GetClients();

            for (int i = 0; i < clList.Count; i++)
            {
                if (clList[i].idClient == uint.Parse(idClient))
                {
                    clList[i].bankAccountID = newAccount.idAccount;

                    RecordFileClients(clList);

                    return;
                }
            }
        }
    }
}
