using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CitIndexFileSDK.MileageFix
{
    public class UserFixedTable
    {
        private IOperator _indexOperator = null;
        private List<UserMarkedPoint> _markedPoints;

        public UserFixedTable(IOperator indexOperator,int kmInc)
        {
            _indexOperator = indexOperator;
            string sql = "select * from IndexOri order by val(indexmeter) ";
            if (kmInc == 1)
            {
                sql += " desc";
            }
            _markedPoints = new List<UserMarkedPoint>();
            DataTable dt = _indexOperator.Query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserMarkedPoint point = new UserMarkedPoint();
                    point.ID = dt.Rows[i]["Id"].ToString();
                    point.FilePointer = long.Parse(dt.Rows[i]["IndexPoint"].ToString());
                    point.UserSetMileage = float.Parse(dt.Rows[i]["IndexMeter"].ToString());
                    _markedPoints.Add(point);
                }
            }
        }

        public List<UserMarkedPoint> MarkedPoints
        {
            get
            {
                return _markedPoints;
            }
        }

        public void Clear()
        {
           
            string cmdText = "delete from IndexOri";
            if (_indexOperator.ExcuteSql(cmdText))
            {
                _markedPoints.Clear();
            }
        }

        /// <summary>
        /// 删除用户标记点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string ID)
        {
            string sqlDelete = "delete from IndexOri where id=" + ID;
            return _indexOperator.ExcuteSql(sqlDelete);
        }
      
            /// <summary>
            /// 保存用户标记点
            /// </summary>
            /// <returns></returns>
        public bool Save()
        {
            try
            {
                if (_markedPoints != null && _markedPoints.Count > 0)
                {
                    foreach (var item in _markedPoints)
                    {
                        string getIDStr = "select max(id)+1 from IndexOri";
                        object obj = _indexOperator.ExecuteScalar(getIDStr);
                        string ID = string.Empty;
                        if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                        {
                            ID = obj.ToString();
                        }
                        else
                        {
                            ID = "1";
                        }
                        string sqlInsert = "insert into IndexOri values(" + ID + ",0,'" + item.FilePointer + "','" + item.UserSetMileage + "')";
                        _indexOperator.ExcuteSql(sqlInsert);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

        public bool Save(UserMarkedPoint point)
        {
            try
            {
                
                string getIDStr = "select max(id)+1 from IndexOri";
                object obj = _indexOperator.ExecuteScalar(getIDStr);
                string ID = string.Empty;
                if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
                {
                    ID = obj.ToString();
                }
                else
                {
                    ID = "1";
                }
                string sqlInsert = "insert into IndexOri values(" + ID + ",0,'" + point.FilePointer + "','" + point.UserSetMileage + "')";
                if(_indexOperator.ExcuteSql(sqlInsert))
                {
                    _markedPoints.Add(point);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
