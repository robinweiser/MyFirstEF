namespace MyFirstEF
{
    public interface IMyProductService
    {
        void addData();
        void AddLogging();
        void createDatabase();
        void FilterEntries();
        void ShowAllEntries();
        void dropDatabase();
        void UpdateData(string param);
    }
}