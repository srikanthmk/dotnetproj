using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Future_is_mine.Models;
using System.Data;

namespace Future_is_mine.SQLOperations
{
    public class Employee_CURD
    {
        private SqlConnection _sqlConnection;
        private SqlCommand _SqlCommand;
        private DataSet _dataset;
        private SqlDataAdapter _SqlDataAdapter;
        private string status;

        public void connection()
        {
            string connection = ConfigurationManager.ConnectionStrings["GetConnection"].ToString();
            _sqlConnection = new SqlConnection(connection);
        }
        public List<JobDetails> GetJodDetails(string query)
        {
            connection();
            List<JobDetails> _JobDetails = new List<JobDetails>();

            SqlCommand com = new SqlCommand(query, _sqlConnection);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            da.Fill(dt);
            _sqlConnection.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                _JobDetails.Add(

                    new JobDetails
                    {

                        JobTitle = Convert.ToString(dr["job_info"]),
                        jobDescription = HttpUtility.HtmlDecode((dr["job_description"]).ToString()),
                        JobType = Convert.ToString(dr["jobtype"]),
                        jobLocation = Convert.ToString(dr["location"]),
                        JobId = Convert.ToInt32(dr["job_id"])

                    }
                    );
            }

            return _JobDetails;


        }

        public string AddEmployee(string query)
        {
            try
            {
                connection();
                _sqlConnection.Open();
                _SqlCommand = new SqlCommand(query, _sqlConnection);
                int result = Convert.ToInt32(_SqlCommand.ExecuteScalar());
                switch (result)
                {
                    case -1:
                        status=  "success";
                        break;
                    case -2:
                        status = "fail";
                        break;
                    
                }
            }
            catch (SqlException Ex)
            {
              status =   Ex.ToString();
            }
            catch (Exception Ex) {
                status = Ex.ToString();
            }
            finally
            {
                _sqlConnection.Close();
            }
            return status;

        }

        public DataSet GetDetails(string query)
        {
            _dataset = new DataSet();
           
            try
            {
                connection();
                _sqlConnection.Open();
                using (_SqlCommand = new SqlCommand(query))
                {
                    
                    using (_SqlDataAdapter = new SqlDataAdapter())
                    {
                        _SqlCommand.Connection = _sqlConnection;
                        _SqlDataAdapter.SelectCommand = _SqlCommand;
                        _SqlCommand.ExecuteNonQuery();
                        _SqlDataAdapter.Fill(_dataset);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (_sqlConnection != null && _sqlConnection.State == ConnectionState.Open)
                {
                    _sqlConnection.Close();
                }
            }
            return _dataset;
        }
        public bool UpdateData(string query)
        {

            _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["GetConnection"].ConnectionString;
            _SqlCommand = new SqlCommand(query);

            try
            {
                _sqlConnection.Open();
                _SqlCommand.Connection = _sqlConnection;
                _SqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _sqlConnection.Close();
                _sqlConnection.Dispose();
            }
        }

    }
}