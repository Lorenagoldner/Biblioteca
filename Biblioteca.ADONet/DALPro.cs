using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace Biblioteca.ADONet
{
    public static class DALPro //Classe estática, não preicsa criar uma instância. Todos os métodos e propriedades dentro dela são acessados diretamente pelo nome da classe: DALPro.GetConnection()
    {
        public static string? ConnectionString { get; set; } //Campo público e estático que guarda a string de conexão com o banco de dados

        private static readonly Dictionary<Type, PropertyInfo[]> _cacheProps = //_cacheProps é um dicionário privado e somente leitura
            new Dictionary<Type, PropertyInfo[]>();//Chave(Type) é o tipo de classe que está sendo mapeado(ex: employee), valor(PropertyInfo) é um array das propriedades desse tipo

        public static SqlConnection GetConnection() //método estático que retorna uma nova conexão SQL usando a connection string. Cada vez que o SqlConnection é chamado é criado uma nova instância
        {                                           //de SqlConnection.                     
            return new SqlConnection(ConnectionString); // Dictionary é uma coleção de pares chave-valor
        }                                               // Cache é um armazenamento temporário usado para acelerar o acessoa a informações. 

        // --------------------------------------------------
        // CREATE COMMAND
        // --------------------------------------------------
        private static SqlCommand CreateCommand(
            string sql, // quety que será executada
            SqlTransaction trans = null, // transação opcional
            Dictionary<string, object> parameters = null) //parametros da query
        {
            SqlCommand cmd; // variável que irá armazernar o comando SQL
            
            if (trans != null)// se voi passada uma transaçºao o comando é criado usando a query SQL, a conexão da transação, a própria transação).
                cmd = new SqlCommand(sql, trans.Connection, trans);
            else // se não ouver transação, cria uma conexão, abre a conexão, cria o comando usando essa conexão
            {
                SqlConnection cn = GetConnection();
                cn.Open();
                cmd = new SqlCommand(sql, cn);// SqlCommand(String command Text, SqlConnection connection - a conexão com o banco) (Crie um comando SQl que executa a query "sql" usando a conexão "cn"
            }

            if (parameters != null) // Se existir um dicionário de parâmetros, ele percorre todos eles.
            {
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);//?? Significa "Se o valor for null usar outrao valor. no caso DBNull.Value
            }                               // Os bancos de dados não usam null do C#, eles usam o valor DBNull.Value.

            return cmd;
        }
        /*Exemplo de uso simples:

            CreateCommand("SELECT * FROM users");

            Ou com parâmetros:

            CreateCommand(
                "SELECT * FROM users WHERE id = @id",
                 null,
                new Dictionary<string, object> { { "@id", 5 } }
            );*/

        // --------------------------------------------------
        // EXECUTE NON QUERY non query é uma query que não retorna resultados, como INSERT, UPDATE ou DELETE
        // --------------------------------------------------
        public static int Execute(
            string sql,
            Dictionary<string, object> parameters = null,
            SqlTransaction trans = null)
        {
            using SqlCommand cmd = CreateCommand(sql, trans, parameters);

            int result = cmd.ExecuteNonQuery();

            if (trans == null)
                cmd.Connection.Close();

            return result;
        }

        // --------------------------------------------------
        // EXECUTE SCALAR uma query que retorna um único valor, como COUNT(*) ou SELECT de uma coluna específica
        // --------------------------------------------------
        public static object ExecuteScalar(
            string sql,
            Dictionary<string, object> parameters = null,
            SqlTransaction trans = null)
        {
            using SqlCommand cmd = CreateCommand(sql, trans, parameters);

            object result = cmd.ExecuteScalar();

            if (trans == null)
                cmd.Connection.Close();

            return result;
        }

        // --------------------------------------------------
        // QUERY GENERIC uma query genérica que retorna uma lista de objetos do tipo T, onde T é uma classe com propriedades que correspondem às colunas da query. O método usa reflexão para mapear os resultados da query para as propriedades da classe T.
        // --------------------------------------------------
        public static List<T> Query<T>(
            string sql,
            Dictionary<string, object> parameters = null,
            SqlTransaction trans = null) where T : new()
        {
            List<T> list = new();

            using SqlCommand cmd = CreateCommand(sql, trans, parameters);
            using SqlDataReader dr = cmd.ExecuteReader();

            PropertyInfo[] props;

            if (!_cacheProps.TryGetValue(typeof(T), out props))
            {
                props = typeof(T).GetProperties();
                _cacheProps[typeof(T)] = props;
            }

            while (dr.Read())
            {
                T obj = new T();

                foreach (var prop in props)
                {
                    try
                    {
                        int idx = dr.GetOrdinal(prop.Name);

                        if (!dr.IsDBNull(idx))
                            prop.SetValue(obj, dr[idx]);
                    }
                    catch { }
                }

                list.Add(obj);
            }

            if (trans == null)
                cmd.Connection.Close();

            return list;
        }

        // --------------------------------------------------
        // DATATABLE FOR UPDATE uma query que retorna um DataTable preenchido com os resultados da query, e também configura um SqlDataAdapter para permitir que o DataTable seja atualizado e as mudanças sejam refletidas no banco de dados. O método aceita uma query SQL, um SqlDataAdapter passado por referência (que será configurado dentro do
        // --------------------------------------------------
        public static DataTable DataTableForUpdate(
            string sql,
            ref SqlDataAdapter da,
            SqlTransaction trans = null)
        {
            SqlConnection cn;

            if (trans != null)
                cn = trans.Connection;
            else
            {
                cn = GetConnection();
                cn.Open();
            }

            da = new SqlDataAdapter(sql, cn);

            if (trans != null)
                da.SelectCommand.Transaction = trans;

            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            cb.QuotePrefix = "[";
            cb.QuoteSuffix = "]";

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (trans == null)
                cn.Close();

            return dt;
        }

        // --------------------------------------------------
        // STORED PROCEDURE uma query que executa uma stored procedure e retorna os resultados em um DataTable. O método aceita o nome da stored procedure, um dicionário de parâmetros (
        // --------------------------------------------------
        public static DataTable ExecuteSP(
            string spName,
            Dictionary<string, object> parameters = null,
            SqlTransaction trans = null)
        {
            SqlConnection cn;

            if (trans != null)
                cn = trans.Connection;
            else
            {
                cn = GetConnection();
                cn.Open();
            }

            SqlCommand cmd = new SqlCommand(spName, cn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (trans != null)
                cmd.Transaction = trans;

            if (parameters != null)
            {
                foreach (var p in parameters)
                    cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (trans == null)
                cn.Close();

            return dt;
        }

        // --------------------------------------------------
        // TRANSACTIONS  que permitem agrupar várias operações de banco de dados em uma única unidade de trabalho, garantindo que todas as operações sejam concluídas com sucesso ou revertidas em caso de falha. O método BeginTransaction
        // --------------------------------------------------
        public static SqlTransaction BeginTransaction()
        {
            SqlConnection cn = GetConnection();
            cn.Open();
            return cn.BeginTransaction();
        }

        public static void Commit(SqlTransaction trans)
        {
            SqlConnection cn = trans.Connection;

            trans.Commit();

            if (cn.State == ConnectionState.Open)
                cn.Close();
        }

        public static void Rollback(SqlTransaction trans)
        {
            SqlConnection cn = trans.Connection;

            trans.Rollback();

            if (cn.State == ConnectionState.Open)
                cn.Close();
        }
    }
}