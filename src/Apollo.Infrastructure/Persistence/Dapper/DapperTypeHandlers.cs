using System.Data;
using Dapper;

namespace Apollo.Infrastructure.Persistence.Dapper;

internal static class DapperTypeHandlers
{
    public static void Register()
    {
        SqlMapper.AddTypeHandler(new GuidHandler());
        SqlMapper.AddTypeHandler(new DecimalHandler());
        SqlMapper.AddTypeHandler(new BooleanHandler());
        SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
    }

    private sealed class GuidHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value) => value switch
        {
            Guid guid => guid,
            byte[] bytes => new Guid(bytes),
            _ => Guid.Parse(value.ToString()!)
        };

        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.DbType = DbType.String;
            parameter.Value = value.ToString();
        }
    }

    private sealed class DecimalHandler : SqlMapper.TypeHandler<decimal>
    {
        public override decimal Parse(object value) => Convert.ToDecimal(value);

        public override void SetValue(IDbDataParameter parameter, decimal value)
        {
            parameter.DbType = DbType.Decimal;
            parameter.Value = value;
        }
    }

    private sealed class BooleanHandler : SqlMapper.TypeHandler<bool>
    {
        public override bool Parse(object value) => Convert.ToInt64(value) != 0;

        public override void SetValue(IDbDataParameter parameter, bool value)
        {
            parameter.DbType = DbType.Int64;
            parameter.Value = value ? 1L : 0L;
        }
    }

    private sealed class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value) => value switch
        {
            DateTimeOffset offset => offset,
            DateTime dateTime => new DateTimeOffset(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)),
            _ => DateTimeOffset.Parse(value.ToString()!)
        };

        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
        {
            parameter.DbType = DbType.String;
            parameter.Value = value.ToString("O");
        }
    }
}
