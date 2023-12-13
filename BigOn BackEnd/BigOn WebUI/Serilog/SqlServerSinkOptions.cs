using Serilog.Sinks.MSSqlServer;

namespace BigOn_WebUI.Serilog
{
    public class SqlServerSinkOptions : MSSqlServerSinkOptions
    {
        public SqlServerSinkOptions()
        {
            AutoCreateSqlTable = true;
            SchemaName = "Serilog";
            TableName = "Logs";
        }
    }
}
