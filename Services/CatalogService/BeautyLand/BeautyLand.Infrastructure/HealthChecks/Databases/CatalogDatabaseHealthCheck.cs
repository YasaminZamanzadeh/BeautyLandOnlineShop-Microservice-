using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeautyLand.Infrastructure.HealthChecks.Databases
{
    public class CatalogDatabaseHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var connection = new SqlConnection("Data Source =.; Initial Catalog = CatalogBeautyLand; Integrated Security = True;"))
            {
                try
                {
                   await connection.OpenAsync(cancellationToken);
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT TOP 1";
                    await  command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {

                    return new HealthCheckResult(
                        status: context.Registration.FailureStatus,
                        exception: ex
                        );
                }
            }
            return await Task.FromResult(HealthCheckResult.Healthy());
                
        }
    }
}
