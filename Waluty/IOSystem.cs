namespace Waluty
{
    public class IOSystem
    {
        private string _file = "update.dat";

        public void Save(int year, int month)
        {
            try
            {
                string content = string.Format("{0}\n{1}", year, month);
                File.WriteAllText(_file, content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DateTime Read()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(_file))
                {
                    int year = int.Parse(streamReader.ReadLine());
                    int month = int.Parse(streamReader.ReadLine());
                    return new DateTime(year, month, 20);
                }
            }
            catch (Exception ex)
            {
                return DateTime.MaxValue;
            }
        }
    }
}
