using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using QuizRapido2PM2.Models;

namespace QuizRapido2PM2.Controllers
{
    public class AutoresControllers
    {
        readonly SQLiteAsyncConnection _connection;

        public AutoresControllers()
        {
            SQLite.SQLiteOpenFlags extensiones = SQLite.SQLiteOpenFlags.ReadWrite |
            //SQLite.SQLiteOpenFlags.ReadOnly |
                SQLite.SQLiteOpenFlags.Create |
                SQLite.SQLiteOpenFlags.SharedCache;

            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBAutores.db3"), extensiones);
            _connection.CreateTableAsync<Models.Autores>();
        }

        // Crud Methods

        // Create  //Update
        public async Task<int> StoreAutor(Models.Autores autores)
        {
            if (autores.Id == 0)
            {
                return await _connection.InsertAsync(autores);
            }
            else
            {
                return await _connection.UpdateAsync(autores);
            }
        }

        //Read

        public async Task<List<Models.Autores>> GetListAutores()
        {
            return await _connection.Table<Models.Autores>().ToListAsync();
        }

        //

        public async Task<Models.Autores> GetAutor(int id)
        {
            return await _connection.Table<Models.Autores>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        //Delete
        public async Task<int> DeleteAutor(Models.Autores autor)
        {
            return await _connection.DeleteAsync(autor);
        }
    }
}
