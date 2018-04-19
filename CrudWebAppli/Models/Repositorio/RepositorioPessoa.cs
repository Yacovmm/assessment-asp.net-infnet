using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CrudWebAppli.Models.Repositorio
{
    public class RepositorioPessoa
    {
        private static string stringConnections =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Projetos\VisualStudio\CrudATWeb\CrudWebAppli\App_Data\DatabaseAmigos.mdf;Integrated Security=True";


        public abstract class AbstractRepository<TEntity, TKey>
         where TEntity : class
        {
            protected string StringConnection { get; } = stringConnections;
                
                //WebConfigurationManager.ConnectionStrings["DatabaseAmigos"].ConnectionString;

            public abstract List<TEntity> GetAll();
            public abstract TEntity GetById(TKey id);
            public abstract void Save(TEntity entity);
            public abstract void Update(TEntity entity);
            public abstract void Delete(TEntity entity);
            public abstract void DeleteById(TKey id);
        }


        public class PessoaRepository : AbstractRepository<Pessoa, int>
        {
            public IEnumerable<object> Pessoa { get; internal set; }

            ///<summary>Exclui uma pessoa pela entidade
            ///<param name="entity">Referência de Pessoa que será excluída.</param>
            ///</summary>
            public override void Delete(Pessoa pessoa)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "DELETE Pessoa Where Id=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", pessoa.Id);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            ///<summary>Exclui uma pessoa pelo ID
            ///<param name="id">Id do registro que será excluído.</param>
            ///</summary>
            public override void DeleteById(int id)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "DELETE Pessoa Where Id=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            ///<summary>Obtém todas as pessoas
            ///<returns>Retorna as pessoas cadastradas.</returns>
            ///</summary>
            public override List<Pessoa> GetAll()
            {
                string sql = "Select Id, Nome, Sobrenome, Dt_nascimento FROM Pessoa ORDER BY Nome";
                using (var conn = new SqlConnection(StringConnection))
                {
                    var cmd = new SqlCommand(sql, conn);
                    List<Pessoa> list = new List<Pessoa>();
                    Pessoa p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                p = new Pessoa();
                                p.Id = (int)reader["Id"];
                                p.Nome = reader["Nome"].ToString();
                                p.Sobrenome = reader["Sobrenome"].ToString();
                                p.Dt_nascimento = (DateTime)reader["Dt_nascimento"];

                                list.Add(p);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;

                       
                    }
                    return list;
                }
            }

            ///<summary>Obtém uma pessoa pelo ID
            ///<param name="id">Id do registro que obtido.</param>
            ///<returns>Retorna uma referência de Pessoa do registro encontrado ou null se ele não for encontrado.</returns>
            ///</summary>
            public override Pessoa GetById(int id)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "Select Id, Nome, Sobrenome, Dt_nascimento FROM Pessoa WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    Pessoa p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    p = new Pessoa();
                                    p.Id = (int)reader["Id"];
                                    p.Nome = reader["Nome"].ToString();
                                    p.Sobrenome = reader["Sobrenome"].ToString();
                                    p.Dt_nascimento = (DateTime)reader["Dt_nascimento"];
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    return p;
                }
            }

            ///<summary>Salva a pessoa no banco
            ///<param name="entity">Referência de Pessoa que será salva.</param>
            ///</summary>
            public override void Save(Pessoa entity)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "INSERT INTO Pessoa (Nome, Sobrenome, Dt_nascimento) VALUES (@Nome, @Sobrenome, @Dt_nascimento)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Nome", entity.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", entity.Sobrenome);
                    cmd.Parameters.AddWithValue("@Dt_nascimento", entity.Dt_nascimento);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            ///<summary>Atualiza a pessoa no banco
            ///<param name="entity">Referência de Pessoa que será atualizada.</param>
            ///</summary>
            public override void Update(Pessoa entity)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "UPDATE Pessoa SET Nome=@Nome, Sobrenome=@Sobrenome, Dt_nascimento=@Dt_nascimento Where Id=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@Nome", entity.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", entity.Sobrenome);
                    cmd.Parameters.AddWithValue("@Dt_nascimento", entity.Dt_nascimento);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }
    }
}