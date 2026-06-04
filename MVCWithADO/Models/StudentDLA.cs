using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace MVCWithADO.Models
{


    public class StudentDLA
    {
        SqlConnection con;
        SqlCommand cmd;

        public StudentDLA()
        {
            string ConStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            con = new SqlConnection(ConStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
        }
        public List<Student> SelectStudents(int? Sid, bool? Status)
        {
            List<Student> students = new List<Student>();
            try
            {
                cmd.CommandText = "Student_Select";
                if (Sid != null && Status != null)
                {
                    cmd.Parameters.AddWithValue("@Sid", Sid);
                    cmd.Parameters.AddWithValue("@Status", Status);
                }
                else if (Sid == null && Status != null)
                {

                    cmd.Parameters.AddWithValue("@Status", Status);
                }
                else if (Sid != null && Status == null)
                {
                    cmd.Parameters.AddWithValue("@Status", Status);

                }

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Student student = new Student
                    {
                        Sid = Convert.ToInt32(dr["Sid"]),
                        Name = dr["Name"].ToString(),
                        Class = Convert.ToInt32(dr["Class"]),
                        Fees = Convert.ToInt32(dr["Fees"]),
                        Photo = dr["Photo"].ToString()
                    };
                    students.Add(student);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return students;
        }
        public int InsertStudent(Student student)
        {
            int result = 0;
            try
            {
                cmd.CommandText = "Student_Insert";
                AddParameters(student);
                con.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public void AddParameters(Student student)
        {
            cmd.Parameters.AddWithValue("@Sid", student.Sid);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Class", student.Class);
            cmd.Parameters.AddWithValue("@Fees", student.Fees);
            cmd.Parameters.AddWithValue("@Photo", student.Photo);
        }   
        public int UpdateStudent(Student student)
        {
            int Count = 0;
            try
            {
                cmd.CommandText = "Student_Update";
                cmd.Parameters.Clear();
                AddParameters(student);
                con.Open();
                
               Count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return Count;
        }
        public int DeleteStudent(int Sid)
        {
            int Count = 0;
            try
            {
                cmd.CommandText = "Student_Delete";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Sid", Sid);
                con.Open();
                Count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return Count;
        }
    }
}