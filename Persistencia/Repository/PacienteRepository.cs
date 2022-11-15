using Microsoft.EntityFrameworkCore;
using Persistencia.Context;
using Dominio.Paciente;
using Microsoft.Data.SqlClient;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Repository
{
//    public IGenericRepository<Paciente> _PacienteRepository { get; }
//    public AplicationDbContext _context;

//    public ParametroRepository(ApplicationDbContext context, IGenericRepository<Paciente> parametroDetalleRepository) : base(context)
//    {
//        this._parametroDetalleRepository = parametroDetalleRepository;
//        this._context = context;
//    }

//    public async Task<Boolean> ValidarExisteParametro(string codigoInterno)
//    {
//        return _context.Parametro.Any(p => p.VcCodigoInterno == codigoInterno);
//    }



//    public void insertMassiveData(ParametroRequest parametroRequest)
//    {
//        //insert to db
//        using (var connection = _context.Database.GetDbConnection())
//        {
//            connection.Open();


//            //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [ParametroDetalle] ON");
//            //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [Parametro] ON");



//            using (SqlTransaction transaction = (SqlTransaction)connection.BeginTransaction())
//            {
//                using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)connection, SqlBulkCopyOptions.Default, transaction))
//                {

//                    try
//                    {



//                        bulkCopy.DestinationTableName = "Parametro";
//                        bulkCopy.WriteToServer(parametroRequest.Parametros);

//                        bulkCopy.DestinationTableName = "ParametroDetalle";
//                        bulkCopy.WriteToServer(parametroRequest.ParametroDetalles);



//                        transaction.Commit();
//                    }
//                    catch (Exception ex)
//                    {
//                        transaction.Rollback();
//                        connection.Close();
//                        throw;
//                    }

//                }
//            }


//            //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [ParametroDetalle] OFF");
//            //_context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [Parametro] OFF");

//        }

    //}
}
