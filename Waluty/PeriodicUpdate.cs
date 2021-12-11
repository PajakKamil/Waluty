using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waluty
{
    public class PeriodicUpdate : IOSystem
    {
        public PeriodicUpdate()
        {
            if (CheckIfNeedUpdate())
            {
                MessageBox.Show("Your data is up to date");
                SqlStuff update = new SqlStuff();
                update.ClickToUpdate();
            }
            Save(DateTime.Today.Year, DateTime.Today.Month);
        }


        private bool CheckIfNeedUpdate()
        {
            DateTime needUpdate = Read();
            if (DateTime.Today.Year > needUpdate.Year || DateTime.Today.Month > needUpdate.Month + 1)
                return true;
            else
                return false;
        }
    }
}
