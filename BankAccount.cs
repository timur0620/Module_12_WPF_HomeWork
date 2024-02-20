using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Module_12_WPF_HomeWork
{
    class BankAccount <T1, T2, T3>: Client<uint, string, string, string, uint>
    {   
        public T1  idAccount { get; set; }
        public T2 accountNumber { get; set; }
        public T3 totalSum {  get; set; }
        public List<BankAccount<uint, long, uint>> acList { get; set; }
        public BankAccount<uint, long, uint> this[int index]
        {
            get { return this.acList[index]; }
        }
        public BankAccount(T1 id, T2 accountNumber, T3 totalSum)
        {
            this.idAccount = id;
            this.accountNumber = accountNumber;
            this.totalSum = totalSum;

        }

        public BankAccount()
        {
        }

        public BankAccount<uint, long, uint> CreateAccount(uint sum)
        {           
            DataBase database = new DataBase();

            Random rand = new Random();

            uint id = database.GetCurrentIdInDB("accounts");

            BankAccount<uint, long, uint> account = new BankAccount<uint, long,uint>(id, rand.Next(1_000_000, 9_000_000), sum);

            database.AppendAccountDB(account);

            return account;
        }
        public void DeleteAccount(string idClient)
        {
            List<Client<uint, string, string, string, uint>> allClients = GetClients();
            List<BankAccount<uint, long, uint>> allAccounts = GetAccounts();
            List<BankAccount<uint, long, uint>> tempAllAccount = new List<BankAccount<uint, long, uint>>();
            uint tempId = 0;

            for (int i = 0; i < allClients.Count; i++)
            {
                if (allClients[i].idClient == uint.Parse(idClient))
                {
                    tempId = allClients[i].bankAccountID;

                    allClients[i].bankAccountID = 0;

                    RecordFileClients(allClients);
                }
            }      
            for (int i = 0; i < allAccounts.Count; i++)
            {
                if (allAccounts[i].idAccount == tempId)
                { 
                   continue;
                }
                tempAllAccount.Add(allAccounts[i]);
            }
            RecordFileAccounts(tempAllAccount);
        }
        public void TransferMoney(string debitAccount, string creditAccount, string sum)
        {
            List<BankAccount<uint, long, uint>> allAccounts = GetAccounts();
            int controller = 0;

            for (int i = 0; i < allAccounts.Count; i++)
            {
                if (allAccounts[i].accountNumber == long.Parse(debitAccount))
                {
                    allAccounts[i].totalSum -= uint.Parse(sum);
                    controller++;
                }
                else if (allAccounts[i].accountNumber == long.Parse (creditAccount))
                {
                    allAccounts[i].totalSum += uint.Parse(sum);
                    controller++;
                }
            }
            if (controller == 2)
            {
                RecordFileAccounts(allAccounts);
                controller = 0;
            }
            
        }
        public override string ToString()
        {
            return $"{idAccount} {accountNumber} {totalSum}";
        }
    }
}

