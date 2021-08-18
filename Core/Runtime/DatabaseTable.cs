namespace AlessandroBrizio.Cabinet.Core
{
    public abstract class DatabaseTable
    {
        protected internal Database database { get; internal set; }

        protected abstract string tableName { get; }

        protected internal abstract void CreateTable();
    }
}
