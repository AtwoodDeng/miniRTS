using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;

public class DataManager : Manager {

	public DataManager() { s_Instance = this; }
	public static DataManager Instance { get { return s_Instance; } }
	private static DataManager s_Instance;

	public class ExcelHelper : IDisposable
	{
        private string fileName = null; 
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;

        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        public DataTable readXLS( string sheetName="" )
		{
			ISheet sheet = null;
			var data = new DataTable();

			int startRow = 0;
			try{
				FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
	            if (fileName.IndexOf(".xlsx") > 0 ) // 2007 version
	                workbook = new XSSFWorkbook(fs);
	            else if (fileName.IndexOf(".xls") > 0 ) // 2003 version
	                workbook = new HSSFWorkbook(fs);


	 			if (sheetName != "")
	            {
	                sheet = workbook.GetSheet(sheetName);
	                if (sheet == null) 
	                {
	                    sheet = workbook.GetSheetAt(0);
	                }
	            }
	            else
	            {
	                sheet = workbook.GetSheetAt(0);
	            }


	            if ( sheet != null )
	            {
	            	IRow firstRow = sheet.GetRow(0);
	            	int cellCount = firstRow.LastCellNum; 


            	for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (cellValue != null)
                            {
                                var column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                    }
                    startRow = sheet.FirstRowNum + 1;

	            	int rowCount = sheet.LastRowNum;

	            	for (int i = startRow; i <= rowCount ; ++i)
	            	{
	            		IRow row = sheet.GetRow(i);
	            		if (row == null) continue;

	            		DataRow dataRow = data.NewRow();
	            		for (int j = row.FirstCellNum ; j < cellCount ; ++j )
	            		{
	            			if ( row.GetCell(j) != null )
	            			{
	            				dataRow[j]= row.GetCell(j).ToString();
	            			}
	            		}
	            		data.Rows.Add(dataRow);

	            	}
	            }

	            return data;

			} catch (Exception ex)
	        {
	            // Console.WriteLine("Exception: " + ex.Message);
	            Debug.LogError("Exception: " + ex.Message );
	            return null;
	        }
		}

		public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }
	}

	List<DataTable> robotData = new List<DataTable>();
	public List<List<string>> equipmentList = new List<List<string>>();

	void Awake()
	{
		var helper = new ExcelHelper(Application.dataPath + Global.ROBOT_DATA_PATH);
		for(int i = 0 ; i < (int)Global.DataTableType.None ; ++ i )
		{
			DataTable table = helper.readXLS( ((Global.DataTableType)i).ToString() );
			robotData.Add(table);

			List<string> equipmentNames = new List<string>();
			for(int j = 0 ; j < table.Rows.Count ; ++ j)
				equipmentNames.Add(table.Rows[j]["Name"].ToString());
			equipmentList.Add(equipmentNames);
			// Debug.Log( ((Global.DataTableType)i).ToString() + " " + equipmentNames[0]);
		}
		// readXLS(Application.dataPath+"/RobotBody.xls");
	}

	public DataRow getDataRowByType(Global.DataTableType type , string Name )
	{
		DataRow[] rows = robotData[(int)type].Select("Name = '"+Name+"'");
		return rows[0];
	}

	public DataRow getDataRowByName( string Name )
	{
		Global.DataTableType type = Global.NameToType(Name);
		return getDataRowByType(type,Name);
	}

	public Robot.BodyParameter GetBodyParameter(string bodyName, DataRow row = null )
	{
		if ( row == null )
			row = getDataRowByType(Global.DataTableType.Body,bodyName);
		return GetBodyParameter(row);
	}

	public Robot.BodyParameter GetBodyParameter(DataRow row )
	{
		Robot.BodyParameter res = new Robot.BodyParameter();
		if (row == null) return res;
		res.Name = row["Name"].ToString();
		res.Health = float.Parse(row["Health"].ToString());
		res.MoveSpeed = float.Parse(row["MoveSpeed"].ToString());
		res.TurnAngleSpeed = float.Parse(row["TurnAngleSpeed"].ToString());
		res.Power = float.Parse(row["Power"].ToString());
		res.CreateTime = float.Parse(row["CreateTime"].ToString());
		return res;
	}

	public Weapon.WeaponParameter GetWeaponParameter(string weaponName, DataRow row = null )
	{
		if ( row == null )
			row = getDataRowByType(Global.DataTableType.Weapon,weaponName);
		return GetWeaponParameter(row);
	}

	public Weapon.WeaponParameter GetWeaponParameter(DataRow row )
	{
		Weapon.WeaponParameter res = new Weapon.WeaponParameter();
		if (row == null) return res;
		res.Name = row["Name"].ToString();
		res.Damage = float.Parse(row["Damage"].ToString());
		res.ShootSpeed = float.Parse(row["ShootSpeed"].ToString());
		res.ShootCoolDown = float.Parse(row["ShootCoolDown"].ToString());
		res.DamageType = (DamageType)Enum.Parse(typeof(DamageType),row["DamageType"].ToString());
		res.BulletName = row["BulletName"].ToString();
		res.CreateTime = float.Parse(row["CreateTime"].ToString());
		res.BulletPrefab = Resources.Load(Global.BULLET_PREFAB_PATH + res.BulletName) as GameObject;
		return res;
	}

	public Pusher.PusherParameter GetPusherParameter(string pusherName, DataRow row = null )
	{
		if ( row == null )
			row = getDataRowByType(Global.DataTableType.Pusher,pusherName);
		return GetPusherParameter(row);
	}

	public Pusher.PusherParameter GetPusherParameter(DataRow row)
	{
		Pusher.PusherParameter res = new Pusher.PusherParameter();
		if (row == null) return res;
		res.Name = row["Name"].ToString();
		res.CreateTime = float.Parse(row["CreateTime"].ToString());
		return res;
	}
	public Armor.ArmorParameter GetArmorParameter(string armorName, DataRow row = null )
	{
		if ( row == null )
			row = getDataRowByType(Global.DataTableType.Armor,armorName);
		return GetArmorParameter(row);
	}

	public Armor.ArmorParameter GetArmorParameter(DataRow row = null)
	{
		Armor.ArmorParameter res = new Armor.ArmorParameter();
		if (row == null) return res;
		res.Name = row["Name"].ToString();
		res.CreateTime = float.Parse(row["CreateTime"].ToString());
		return res;
	}
}
