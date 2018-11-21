//加入多个条码重新打印功能
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


namespace CNCRemotew
{

    public partial class BarcodePrinter : Form
    {
        string[] sql=new string[100];
        string deliver_ap_id = null; //ZBL
        string deliver_sec_id = null; //ZBL
        int deliver_total_plies = 0; //ZBL
        int check = 0;
        int total_seg_global = 0;
        int total_seg_global_print_again = 0;
        int cp001_index = 0;
        int cp001_index_print_again = 0;
        int ps001_index = 0; //ZBL
        int now_pack = 0;
        String machine_num = "";
        Boolean deliver_flag = false; //ZBL
        Boolean print_again_flag = true; //ZBL
        Boolean print_all = true;
        int[] print_seg_num = new int[100];
        StringBuilder db_config = new StringBuilder(512);
        SynchronizationContext m_SyncContext = null;
        Thread sql_thread;
        MySqlConnection mysql;
        MySqlConnection mysql1;
        string database_name;
        string database_ip;
        string database_user;
        string database_pass;
        string factory_name;
        string sheet_cnc_task;
        string sheet_element;
        string sheet_contract;
        string[] printer = new string[3];
        string print_code;
         string sheet_temporary;
        private bool istemporary=false;
        private string sqlUpdate;
        private int total_seg_global2;
        string[] args = null;
        //连接数据库
        public MySqlConnection getMySqlCon()
        {
            String mysqlStr = "Database='"+database_name+"';Data Source='" + database_ip + "';User Id='" + database_user + "';Password='" + database_pass + "';CharSet='utf8'";
            MySqlConnection mysql = new MySqlConnection(mysqlStr);               
            return mysql;
        }
        public BarcodePrinter()
        {
            InitializeComponent();
            Firstly_excu();
            m_SyncContext = SynchronizationContext.Current;
            database_ip = RemoteCNC_config.Default.database_ip;
            database_user = RemoteCNC_config.Default.database_user;
            database_pass = RemoteCNC_config.Default.database_pass;
            database_name = RemoteCNC_config.Default.database_name;
            factory_name = RemoteCNC_config.Default.Factory;
            sheet_cnc_task = RemoteCNC_config.Default.sheet_cnc_task;
            sheet_element = RemoteCNC_config.Default.sheet_element;
            sheet_contract = RemoteCNC_config.Default.sheet_contract;
            sheet_temporary = RemoteCNC_config.Default.sheet_temporary;
            this.Text = factory_name + "-条码打印程序";
            if (RemoteCNC_config.Default.choose_state == "0")
            {

                RemoteCNC_config.Default.cnc_code = "100";
                //RemoteCNC_config.Default.pakage = radioButton2.Checked;
                RemoteCNC_config.Default.Save();

            }
            if (RemoteCNC_config.Default.choose_state == "1")
            {

                RemoteCNC_config.Default.cnc_code = "120";
                //RemoteCNC_config.Default.pakage = radioButton2.Checked;
                RemoteCNC_config.Default.Save();

            }
            if (RemoteCNC_config.Default.choose_state == "2")
            {

                RemoteCNC_config.Default.cnc_code = "130";
                //RemoteCNC_config.Default.pakage = radioButton2.Checked;
                RemoteCNC_config.Default.Save();

            }
            mysql = getMySqlCon();
            sql_thread = new Thread(new ThreadStart(SqlListen));
            sql_thread.Start();
            textBox3.Text = database_ip;
        
          


            printer1.Text = RemoteCNC_config.Default.printer1;
            printer[0] = RemoteCNC_config.Default.printer1;
            printer2.Text = RemoteCNC_config.Default.printer2;
            printer[1] = RemoteCNC_config.Default.printer2;
            printer3.Text = RemoteCNC_config.Default.printer3;
            printer[2] = RemoteCNC_config.Default.printer3;
            CNC1.Text = RemoteCNC_config.Default.CNC1;
            CNC2.Text = RemoteCNC_config.Default.CNC2;
            CNC3.Text = RemoteCNC_config.Default.CNC3;
            if (RemoteCNC_config.Default.CNC状态 || RemoteCNC_config.Default.补打工位状态)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (printer[i].Length > 1)
                    {
                        TSCLIB_DLL.openport(printer[i]);                                           //Open specified printer driver
                        TSCLIB_DLL.setup("60", "40", "4", "8", "0", "1.5", "0");                           //Setup the media size and sensor type info
                        TSCLIB_DLL.sendcommand("GAP 2mm,0");
                        TSCLIB_DLL.closeport();
                    }
                }
            }

        }
        public BarcodePrinter(string[] args)
        {
            InitializeComponent();
            Firstly_excu();
            this.args = args;
            if (args[0]=="1")
            {
                MessageBox.Show("补打工位");
                RemoteCNC_config.Default.choose_state = "1";
                //RemoteCNC_config.Default.pakage = radioButton2.Checked;
                RemoteCNC_config.Default.Save();
            }
            if (args[0] == "2")
            {
                RemoteCNC_config.Default.choose_state = "2";
                //RemoteCNC_config.Default.pakage = radioButton2.Checked;
                RemoteCNC_config.Default.Save();
                MessageBox.Show("质检工位");
            }
            m_SyncContext = SynchronizationContext.Current;
            database_ip = RemoteCNC_config.Default.database_ip;
            database_user = RemoteCNC_config.Default.database_user;
            database_pass = RemoteCNC_config.Default.database_pass;
            database_name = RemoteCNC_config.Default.database_name;
            factory_name = RemoteCNC_config.Default.Factory;
            sheet_cnc_task = RemoteCNC_config.Default.sheet_cnc_task;
            sheet_element = RemoteCNC_config.Default.sheet_element;
            sheet_contract = RemoteCNC_config.Default.sheet_contract;
            sheet_temporary = RemoteCNC_config.Default.sheet_temporary;
            this.Text = factory_name + "-条码打印程序";
            if (RemoteCNC_config.Default.choose_state == "0")
            {

                RemoteCNC_config.Default.cnc_code = "100";
                //RemoteCNC_config.Default.pakage = radioButton2.Checked;
                RemoteCNC_config.Default.Save();

            }
            if (RemoteCNC_config.Default.choose_state == "1")
            {

                RemoteCNC_config.Default.cnc_code = "120";
                //RemoteCNC_config.Default.pakage = radioButton2.Checked;
                RemoteCNC_config.Default.Save();

            }
            if (RemoteCNC_config.Default.choose_state == "2")
            {

                RemoteCNC_config.Default.cnc_code = "130";
                //RemoteCNC_config.Default.pakage = radioButton2.Checked;
                RemoteCNC_config.Default.Save();

            }
            mysql = getMySqlCon();
            sql_thread = new Thread(new ThreadStart(SqlListen));
            sql_thread.Start();
            textBox3.Text = database_ip;
           


            printer1.Text = RemoteCNC_config.Default.printer1;
            printer[0] = RemoteCNC_config.Default.printer1;
            printer2.Text = RemoteCNC_config.Default.printer2;
            printer[1] = RemoteCNC_config.Default.printer2;
            printer3.Text = RemoteCNC_config.Default.printer3;
            printer[2] = RemoteCNC_config.Default.printer3;
            CNC1.Text = RemoteCNC_config.Default.CNC1;
            CNC2.Text = RemoteCNC_config.Default.CNC2;
            CNC3.Text = RemoteCNC_config.Default.CNC3;
            if (RemoteCNC_config.Default.CNC状态 || RemoteCNC_config.Default.补打工位状态)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (printer[i].Length > 1)
                    {
                        TSCLIB_DLL.openport(printer[i]);                                           //Open specified printer driver
                        TSCLIB_DLL.setup("60", "40", "4", "8", "0", "1.5", "0");                           //Setup the media size and sensor type info
                        TSCLIB_DLL.sendcommand("GAP 2mm,0");
                        TSCLIB_DLL.closeport();
                    }
                }
            }

        }

        public void SqlListen()
        {
            
            //while (this.Visible)//关闭窗体后此线程也会关闭
            while (true)
            {
                print_code = RemoteCNC_config.Default.cnc_code;
              //  Console.WriteLine("print_code" + print_code);
                String sqlSearch = "SELECT `Total_seg`,`Element_information_1`, `Element_information_2`, `Element_information_3`, `Element_information_4`, `Element_information_5`, `Element_information_6`, `Element_information_7`, `Element_information_8`, `Element_information_9`, `Element_information_10`, `Element_information_11`, `Element_information_12`, `Element_information_13`, `Element_information_14`, `Element_information_15`, `Element_information_16`, `Element_information_17`, `Element_information_18`, `Element_information_19`, `Element_information_20`, `Element_information_21`, `Element_information_22`, `Element_information_23`, `Element_information_24`, `Element_information_25`, `Element_information_26`, `Element_information_27`, `Element_information_28`, `Element_information_29`, `Element_information_30`, `Element_information_31`, `Element_information_32`, `Element_information_33`, `Element_information_34`, `Element_information_35`, `Element_information_36`, `Element_information_37`, `Element_information_38`, `Element_information_39`, `Element_information_40`, `Element_information_41`, `Element_information_42`, `Element_information_43`, `Element_information_44`, `Element_information_45`, `Element_information_46`, `Element_information_47`, `Element_information_48`, `Element_information_49`, `Element_information_50`, `Element_information_51`, `Element_information_52`, `Element_information_53`, `Element_information_54`, `Element_information_55`, `Element_information_56`, `Element_information_57`, `Element_information_58`, `Element_information_59`, `Element_information_60`, `Element_information_61`, `Element_information_62`, `Element_information_63`, `Element_information_64`, `Element_information_65`, `Element_information_66`, `Element_information_67`, `Element_information_68`, `Element_information_69`, `Element_information_70`, `Element_information_71`, `Element_information_72`, `Element_information_73`, `Element_information_74`, `Element_information_75`, `Element_information_76`, `Element_information_77`, `Element_information_78`, `Element_information_79`, `Element_information_80`, `Element_information_81`, `Element_information_82`, `Element_information_83`, `Element_information_84`, `Element_information_85`, `Element_information_86`, `Element_information_87`, `Element_information_88`, `Element_information_89`, `Element_information_90`, `Element_information_91`, `Element_information_92`, `Element_information_93`, `Element_information_94`, `Element_information_95`, `Element_information_96`, `Element_information_97`, `Element_information_98`, `Element_information_99`, `Element_information_100`, `Index`,`Machine_num` FROM `" + sheet_cnc_task + "` WHERE `Print_Barcode`="+ print_code;
                String sqlSearch_print_again = "SELECT `Total_seg`,`Element_information_1`, `Element_information_2`, `Element_information_3`, `Element_information_4`, `Element_information_5`, `Element_information_6`, `Element_information_7`, `Element_information_8`, `Element_information_9`, `Element_information_10`, `Element_information_11`, `Element_information_12`, `Element_information_13`, `Element_information_14`, `Element_information_15`, `Element_information_16`, `Element_information_17`, `Element_information_18`, `Element_information_19`, `Element_information_20`, `Element_information_21`, `Element_information_22`, `Element_information_23`, `Element_information_24`, `Element_information_25`, `Element_information_26`, `Element_information_27`, `Element_information_28`, `Element_information_29`, `Element_information_30`, `Element_information_31`, `Element_information_32`, `Element_information_33`, `Element_information_34`, `Element_information_35`, `Element_information_36`, `Element_information_37`, `Element_information_38`, `Element_information_39`, `Element_information_40`, `Element_information_41`, `Element_information_42`, `Element_information_43`, `Element_information_44`, `Element_information_45`, `Element_information_46`, `Element_information_47`, `Element_information_48`, `Element_information_49`, `Element_information_50`, `Element_information_51`, `Element_information_52`, `Element_information_53`, `Element_information_54`, `Element_information_55`, `Element_information_56`, `Element_information_57`, `Element_information_58`, `Element_information_59`, `Element_information_60`, `Element_information_61`, `Element_information_62`, `Element_information_63`, `Element_information_64`, `Element_information_65`, `Element_information_66`, `Element_information_67`, `Element_information_68`, `Element_information_69`, `Element_information_70`, `Element_information_71`, `Element_information_72`, `Element_information_73`, `Element_information_74`, `Element_information_75`, `Element_information_76`, `Element_information_77`, `Element_information_78`, `Element_information_79`, `Element_information_80`, `Element_information_81`, `Element_information_82`, `Element_information_83`, `Element_information_84`, `Element_information_85`, `Element_information_86`, `Element_information_87`, `Element_information_88`, `Element_information_89`, `Element_information_90`, `Element_information_91`, `Element_information_92`, `Element_information_93`, `Element_information_94`, `Element_information_95`, `Element_information_96`, `Element_information_97`, `Element_information_98`, `Element_information_99`, `Element_information_100`, `Index`, `Print_Barcode`,`Machine_num`  FROM `" + sheet_cnc_task + "` WHERE `Print_Barcode`<>"+ print_code; //重打条码
                String sqlSearch_temporary = "SELECT COUNT(*), `Code`,`Print_Barcode`  FROM `" + sheet_temporary + "` WHERE `Print_Barcode` IS NOT NULL"; //打散板
                //sqlSearch_temporary散板
                if (true) {   //成功监听
                    MySqlCommand mySqlCommand = getSqlCommand(sqlSearch, mysql);
                    MySqlCommand mySqlCommandPrintAgain = getSqlCommand(sqlSearch_print_again, mysql); //ZBL 重打条码的标志暂时放到Error_Type里
                    MySqlCommand mySqlCommandTemporary = getSqlCommand(sqlSearch_temporary, mysql); //ZBL

                    try
                    {
                        mysql.Open();
                        if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
                        {
                            getResultset(mySqlCommand);           //监听到接单开始加工后，算出总件数，打印门板标签 
                            getResultset_temporary(mySqlCommandTemporary);
                            getResultsetPrintAgain(mySqlCommandPrintAgain);//监听重新打印条码
                        
                        }
                      
                        
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("MySqlException Error:" + ex.ToString());
                    }
                    finally
                    {
                        mysql.Close();
                    }

                    //Console.WriteLine("全局数量："+total_seg_global);
                    if (total_seg_global != 0|| total_seg_global2 != 0)
                    {
                       
                        if (!istemporary) {
                            printlable_check(sql, total_seg_global);
                            sqlUpdate = "UPDATE `" + sheet_cnc_task + "` SET `Print_Barcode`=NULL WHERE `Index`='" + cp001_index.ToString() + "'";
                            Console.WriteLine(sqlUpdate);
                        }
                        else
                        {
                            for(int i=1;i<= total_seg_global2; i++)
                            {
                                printlable_check(sql, total_seg_global2);
                                sqlUpdate = "UPDATE `" + sheet_temporary + "` SET `Print_Barcode`=NULL WHERE `Code`='" + sql[i] + "'";
                                Console.WriteLine(sqlUpdate);
                            }
                            istemporary = false;//散板情况
                        }
                       
                        MySqlCommand mySqlCommand_setstate = getSqlCommand(sqlUpdate, mysql);
                        try
                        {
                            mysql.Open();
                            getUpdate(mySqlCommand_setstate);
                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine("MySqlException Error:" + ex.ToString());
                        }
                        finally
                        {
                            mysql.Close();
                        }

                    }
                    if (total_seg_global_print_again != 0)
                    {
                        printlable_check(sql, total_seg_global_print_again);

                        String sqlUpdate = "UPDATE `" + sheet_cnc_task + "` SET `Error_Type`=NULL,`Print_Barcode`=NULL WHERE `Index`='" + cp001_index_print_again.ToString() + "'";
                        Console.WriteLine(sqlUpdate);
                        MySqlCommand mySqlCommand_setstate = getSqlCommand(sqlUpdate, mysql);
                        try
                        {
                            mysql.Open();
                            getUpdate(mySqlCommand_setstate);
                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine("MySqlException Error:" + ex.ToString());
                        }
                        finally
                        {
                            mysql.Close();
                        }

                    }
                    if (deliver_flag)   //ZBL 判断是否有打包完成的工位工单
                    {

                        //printlable_deliver();
                        //String  sqlUpdateDeliver =
                        printlable_deliver_check(deliver_sec_id);
                        Console.WriteLine("Ap_id：" + deliver_ap_id);
                        String sqlUpdateDeliver = "UPDATE `work_package_task_list` SET `Print_Barcode`=NULL WHERE `Index`='" + ps001_index.ToString() + "'";
                        MySqlCommand mySqlCommand_setstate_deliver = getSqlCommand(sqlUpdateDeliver, mysql);
                        try
                        {
                            mysql.Open();
                            getUpdate(mySqlCommand_setstate_deliver);
                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine("MySqlException Error:" + ex.ToString());
                        }
                        finally
                        {
                            mysql.Close();
                        }
                        String seg_id_name = Regex.Split(deliver_sec_id, "S")[0];
                        String sqlUpdateOrder = "UPDATE `order_order_online` SET `Package_num_now`=" + now_pack + " WHERE `Order_id`='" + seg_id_name + "'";
                        MySqlCommand mySqlCommand_setstate_Order = getSqlCommand(sqlUpdateOrder, mysql);
                        try
                        {
                            mysql.Open();
                            getUpdate(mySqlCommand_setstate_Order);
                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine("MySqlException Error:" + ex.ToString());
                        }
                        finally
                        {
                            mysql.Close();
                        }
                        deliver_flag = false;
                        deliver_total_plies = 0; //ZBL
                        // ps001_index = 0;
                        deliver_ap_id = null; //ZBL
                        deliver_sec_id = null; //ZBL
                    }
                }        
                
                Thread.Sleep(5);
            }
        }

        public static MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);
            return mySqlCommand;
        }

        public void getResultset(MySqlCommand mySqlCommand)
        {
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
               
                if (reader.Read())
                {
                    int total_seg = reader.GetInt32(0);
                    machine_num = reader.IsDBNull(102) ? "NULL" : reader.GetString(102);

                    total_seg_global = total_seg;
                    print_all = true;
                    Console.WriteLine("总件数：" + total_seg);
                    string a = reader.GetString(1);
                    for (int i = 1; i <= total_seg; i++)
                    {
                        sql[i] = reader.GetString(i).Split('&')[4];
                        Console.WriteLine("条码：" + reader.GetString(i));
                        //printlable_check(reader.GetString(i),i);
                    }
                    /*String sqlUpdate = "update `work_cnc_task_list` set `State`='7' where `Index`="+reader.GetInt32(61).ToString();
                    MySqlCommand mySqlCommand_setstate = getSqlCommand(sqlUpdate, mysql);
                    mysql.Open();
                    getUpdate(mySqlCommand);
                    mysql.Close();*/
                    Console.WriteLine("111：" + reader.GetInt32(101));
                    cp001_index = reader.GetInt32(101);

                }
                else
                    total_seg_global = 0;
            }
            catch (Exception)
            {
                Console.WriteLine("查询失败了！1");
            }
            finally
            {
                reader.Close();
            }
        }
        public void getResultset_temporary(MySqlCommand mySqlCommand)//散板 hcj
        {
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {

                if (reader.Read()&& reader.GetInt32(0)!=0)
                {
                    if (reader.HasRows)
                    {
                        

                        /*
                        for (int i = 1; i <= total_seg; i++)
                        {
                            sql[i] = reader.GetString(1);
                            Console.WriteLine("散板：" + sql[i]);
                            //printlable_check(reader.GetString(i),i);
                        }*/
                        if (radioButton2.Checked && (reader.GetString(2).Contains("R")|| reader.GetString(2)==print_code))
                        {
                            int total_seg = 1;
                            istemporary = true;//散板情况
                            total_seg_global2 = total_seg;
                            print_all = true;
                            Console.WriteLine("总件数：" + total_seg + reader.GetString(2));
                            sql[1] = reader.GetString(1);
                            Console.WriteLine("散板：" + sql[0]);
                        }
                        if (radioButton3.Checked && reader.GetString(2).Contains("Z"))
                        {
                            int total_seg = 1;
                            istemporary = true;//散板情况
                            total_seg_global2 = total_seg;
                            print_all = true;
                            Console.WriteLine("总件数：" + total_seg + reader.GetString(2));
                            sql[1] = reader.GetString(1);
                            Console.WriteLine("散板：" + sql[0]);
                        }

                    }

                }
                else
                    total_seg_global2 = 0;
            }
            catch (Exception)
            {
                Console.WriteLine("查询失败了！1");
            }
            finally
            {
                reader.Close();
            }
        }
        public void getResultsetPrintAgain(MySqlCommand mySqlCommand)   //ZBL
        {
            MySqlDataReader reader = mySqlCommand.ExecuteReader();      
            try
            {
               
                if (reader.Read())
                {
                    if (reader.HasRows)
                    {

                        //int total_seg = reader.GetInt32(0);
                        String print_seg_num_str = reader.GetString(102);
                      
                        if (radioButton1.Checked && print_seg_num_str.Contains("_"))
                        {
                            print_all = false;
                            String[] print_seg_num_str_arr = print_seg_num_str.Split('_');
                            total_seg_global_print_again = print_seg_num_str_arr.Length;
                            for (int i = 1; i <= total_seg_global_print_again; i++)
                            {
                                print_seg_num[i] = Convert.ToInt32(print_seg_num_str_arr[i-1]);
                                sql[i] = reader.GetString(print_seg_num[i]).Split('&')[4];
                                Console.WriteLine("编号：" + i + print_seg_num[i]);
                                Console.WriteLine("条码：" + reader.GetString(i));

                            }
                        }
                        if(radioButton1.Checked && !print_seg_num_str.Contains("R") && !print_seg_num_str.Contains("_"))
                        {
                            if (!print_seg_num_str.Equals("100")&&!print_seg_num_str.Equals("120"))
                            {
                                print_all = false;
                                print_seg_num[1] = Convert.ToInt32(print_seg_num_str);
                                total_seg_global_print_again = 1;
                                sql[1] = reader.GetString(print_seg_num[1]).Split('&')[4];
                                Console.WriteLine("条码：" + reader.GetString(print_seg_num[1]));
                            }
                        }
                        if (radioButton2.Checked&&print_seg_num_str.Contains("R"))
                        {
                            print_all = false;
                            String[] print_seg_num_str_arr = print_seg_num_str.Split('R');
                            total_seg_global_print_again = print_seg_num_str_arr.Length-1;
                            for (int i = 1; i <= total_seg_global_print_again; i++)
                            {
                                print_seg_num[i] = Convert.ToInt32(print_seg_num_str_arr[i]);
                                sql[i] = reader.GetString(print_seg_num[i]).Split('&')[4];
                                Console.WriteLine("编号：" + i + print_seg_num[i]);
                                Console.WriteLine("条码：" + reader.GetString(i));

                            }
                        }
                        if (radioButton3.Checked && print_seg_num_str.Contains("Z"))
                        {
                            print_all = false;
                            String[] print_seg_num_str_arr = print_seg_num_str.Split('Z');
                            total_seg_global_print_again = print_seg_num_str_arr.Length - 1;
                            for (int i = 1; i <= total_seg_global_print_again; i++)
                            {
                                print_seg_num[i] = Convert.ToInt32(print_seg_num_str_arr[i]);
                                sql[i] = reader.GetString(print_seg_num[i]).Split('&')[4];
                                Console.WriteLine("编号：" + i + print_seg_num[i]);
                                Console.WriteLine("条码：" + reader.GetString(i));

                            }
                        }

                        machine_num = reader.IsDBNull(103) ? "NULL" : reader.GetString(103);

                        Console.WriteLine("总件数：" + total_seg_global_print_again);
                        

                        cp001_index_print_again = reader.GetInt32(101);
                    }

                }
                else
                    total_seg_global_print_again = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询失败了！2" + ex.ToString());
            }
            finally
            {
                reader.Close();
            }
        }
        public void getResultsetDeliever(MySqlCommand mySqlCommand)   //ZBL
        {
            MySqlDataReader reader = mySqlCommand.ExecuteReader();       
            try
            {
                
                if (reader.Read())
                {
                  
                    if (reader.HasRows)
                    {
                        deliver_flag = true;
                        int total_seg = reader.GetInt32(1); //ZBL
                               //total_seg_global = total_seg;
                        Console.WriteLine("总件数：" + total_seg);
                        Console.WriteLine("Ap_id：" + deliver_ap_id);
                       //  cp001_index = reader.GetInt32(61);
                        deliver_total_plies = reader.GetInt32(1); //ZBL
                        deliver_ap_id = reader.GetString(0); //ZBL
                        deliver_sec_id = reader.GetString(2); //ZBL
                        ps001_index = reader.GetInt32(3);//ZBL

                    }

                }                
                   // total_seg_global = 0;
            }
            catch (Exception)
            {
                Console.WriteLine("查询失败了！3");
            }
            finally
            {
                reader.Close();
            }
        }
        

        public void printlable_check(String[] barcode_rec,int total)
        {
            for (int i = 1; i <= total; i++)
            {
                String order_item_search = "SELECT `Part_num`,`Brand` FROM `order_order_online` WHERE `Order_id`=(SELECT `Order_id` FROM `order_element_online` WHERE `Code`='" + barcode_rec[i] + "')";
                MySqlCommand mySqlCommand_order_item_search = getSqlCommand(order_item_search, mysql);
                int order_item_num = 0;
                String dealer = "";
                try
                {
                    mysql.Open();
                    MySqlDataReader reader1 = mySqlCommand_order_item_search.ExecuteReader();
                    if (reader1.Read())
                    {
                        if (reader1.HasRows)
                        {
                            order_item_num = reader1.GetInt32(0);
                            dealer = reader1.IsDBNull(1) ? "无" : reader1.GetString(1);
                        }
                    }
                    reader1.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("MySqlException Error:" + ex.ToString());
                }
                finally
                {
                    mysql.Close();
                }
                int same_ele_num = 0;
                String same_ele_search="";
                if (istemporary == true)
                {
                     same_ele_search = "SELECT COUNT(*) FROM `order_element_online` WHERE " +
                       "`Board_height`=(SELECT `Board_height` FROM `order_element_online` WHERE `Code`='" + barcode_rec[i] + "') " +
                       " AND `Board_width`=(SELECT `Board_width` FROM `order_element_online` WHERE `Code`='" + barcode_rec[i] + "')" +
                       "AND `Board_type`=(SELECT `Board_type` FROM `order_element_online` WHERE `Code`='" + barcode_rec[i] + "') " +
                       "AND `Board_thick`= (SELECT `Board_thick` FROM `order_element_online` WHERE `Code`= '" + barcode_rec[i] + "') " +
                       "AND `Color`= (SELECT `Color` FROM `order_element_online` WHERE `Code`= '" + barcode_rec[i] + "')" +
                     // "AND `Edge_type`= (SELECT `Edge_type` FROM `order_element_online` WHERE `Code`= '" + barcode_rec[i] + "')" +
                       "AND `Order_id`=(SELECT `Order_id` FROM `order_element_online` WHERE `Code`='" + barcode_rec[i] + "')";
                }
                else 
                {
                    same_ele_search = "SELECT COUNT(*) FROM `order_element_online` WHERE " +
                      "`Board_height`=(SELECT `Board_height` FROM `order_element_online` WHERE `Code`='" + barcode_rec[i] + "') " +
                      " AND `Board_width`=(SELECT `Board_width` FROM `order_element_online` WHERE `Code`='" + barcode_rec[i] + "')" +
                      "AND `Board_type`=(SELECT `Board_type` FROM `order_element_online` WHERE `Code`='" + barcode_rec[i] + "') " +
                       "AND `Board_thick`= (SELECT `Board_thick` FROM `order_element_online` WHERE `Code`= '" + barcode_rec[i] + "') " +
                      "AND `Color`= (SELECT `Color` FROM `order_element_online` WHERE `Code`= '" + barcode_rec[i] + "')" +
                   //    "AND `Edge_type`= (SELECT `Edge_type` FROM `order_element_online` WHERE `Code`= '" + barcode_rec[i] + "')" +
                      "AND `Order_id`=(SELECT `Order_id` FROM `order_element_online` WHERE `Code`='" + barcode_rec[i] + "')";
                }
                MySqlCommand mySqlCommand_same_ele_search = getSqlCommand(same_ele_search, mysql);
                   
                    try
                    {
                        mysql.Open();
                        MySqlDataReader reader1 = mySqlCommand_same_ele_search.ExecuteReader();
                        if (reader1.Read())
                        {
                            if (reader1.HasRows)
                            {
                                same_ele_num = reader1.GetInt32(0);
                            }
                        }
                        reader1.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine("MySqlException Error:" + ex.ToString());
                    }
                    finally
                    {
                        mysql.Close();
                    }

                String itemSearch = "SELECT `Board_type`,`Board_height`,`Board_width`,`Board_thick`,`Color`,`Edge_type`,`Bar_type`,`Archaize`,`Hole`,`Open_way`,`Order_id`,`Double_color`,`Straightening_device` FROM `" + sheet_element + "` WHERE `Code`='" + barcode_rec[i] + "' and `Element_type_id` in (1,3,9,4,5,6)";
                Console.WriteLine("搜索到"+itemSearch);

                //mysql1 = getMySqlCon();
                MySqlCommand mySqlCommand_item = getSqlCommand(itemSearch, mysql);
                try
                {
                    mysql.Open();
                    if (print_all)
                    {
                        getResultset_item(mySqlCommand_item, barcode_rec[i], i, order_item_num, dealer, same_ele_num);
                    }
                    else
                    {
                        getResultset_item(mySqlCommand_item, barcode_rec[i], print_seg_num[i], order_item_num, dealer, same_ele_num);
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("MySqlException Error:" + ex.ToString());
                }
                finally
                {
                    mysql.Close();
                }               
              
            }
        }

        public void printlable_deliver_check(String seg_id)
        {
            string sql_bord_search = "select `Board_type`,`Color`,`Board_height`,`Board_width`,`Board_thick`,COUNT(*) from `order_element_online` where `Package_work_order_ap_id`='" + deliver_ap_id + "' GROUP BY 1";
            MySqlCommand mySqlCommand_bord_search = getSqlCommand(sql_bord_search, mysql);
            mysql.Open();
            MySqlDataReader reader_bord_search = mySqlCommand_bord_search.ExecuteReader();
            String[] board_info_num = new String[60];
            int total_board_num = 0;
            int total_board_type = 0;
            try
            {
                
                for (int i = 0; reader_bord_search.Read();i++ )
                {
                    if (reader_bord_search.HasRows)
                    {
                        board_info_num[i] = reader_bord_search.GetString(0) + " " + reader_bord_search.GetString(1) + " " + reader_bord_search.GetInt32(2).ToString() + "×" + reader_bord_search.GetInt32(3).ToString() + "×" + reader_bord_search.GetInt32(4).ToString() + "　×" + reader_bord_search.GetInt32(5).ToString();
                        total_board_type++;
                        total_board_num += reader_bord_search.GetInt32(5);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询失败了总件数！" + ex.ToString());
                MessageBox.Show("order_order_online中单块信息查询错误！Ap_id:" + deliver_ap_id);
            }
            finally
            {
                reader_bord_search.Close();
                mysql.Close();
            }

     
        }
        //hcj-cncprint
        public void getResultset_item(MySqlCommand mySqlCommand, String barcode_item, int cnc_num, int order_total_num, String dealer, int same_ele_num)
        {
            Console.WriteLine("准备打印!" + barcode_item);
            MySqlDataReader reader1 = mySqlCommand.ExecuteReader();
            try
            {
                while (reader1.Read())
                {
                    if (reader1.HasRows)
                    {   
                        int printer_num = 1000;
                        string barcode = barcode_item;
                        string door_type = reader1.IsDBNull(0) ? " 门型:无" : "门型:" + reader1.GetString(0);
                        string door_open_way = reader1.IsDBNull(9) ? " 不开" : " " + reader1.GetString(9);               
                        string bean = reader1.IsDBNull(6)|| reader1.GetString(6)=="0" ? " 压条:无" : " 压条:" + reader1.GetString(6);       
                        string color = reader1.IsDBNull(4) ? " 颜色:无" : "颜色:" + reader1.GetString(4) + bean;
                        string edge = reader1.IsDBNull(5) ? " 边型:无" : "" + reader1.GetString(5);
                        string Straightening_device = reader1.IsDBNull(12) || reader1.GetString(12) == "0" ? "" : "(LZ)";//拉直器
                        string hole = reader1.IsDBNull(8) ? " 打孔:无" : "打孔:" + reader1.GetString(8)+"  "+ edge;
                        string double_color = reader1.IsDBNull(11) ? "套色: 无" : "套色:" + reader1.GetString(11);
                        string size = "规格:" + reader1.GetInt32(1).ToString() + "*" + reader1.GetInt32(2).ToString() + "×" + same_ele_num.ToString() + door_open_way + " " + Straightening_device;

                        string archaize = reader1.IsDBNull(7) ? "做旧: 无" : "做旧:" + reader1.GetString(7);
                        string cnc_no = "No:" + cnc_num.ToString(); 
                        string cnc_station = machine_num;
                        string order_id = "单号:" + reader1.GetString(10);
                        Console.WriteLine("最终打印:" + barcode + "：" + door_type + size + color + edge + bean + archaize + cnc_no + "共：" + order_total_num);
                        Console.WriteLine(door_type);

                        if(CNC1.Text.Contains(machine_num.ToString()))
                            printer_num = 0;
                        if(CNC2.Text.Contains(machine_num.ToString()))
                            printer_num = 1;
                        if(CNC3.Text.Contains(machine_num.ToString()))
                            printer_num = 2;
                        if (printer_num != 1000)
                        {
                            if (radioButton1.Checked)//CNC
                            {
                                TSCLIB_DLL.openport(printer[printer_num]);
                                TSCLIB_DLL.setup("60", "40", "4", "8", "0", "1.5", "0");                           //Setup the media size and sensor type info
                                TSCLIB_DLL.sendcommand("GAP 2mm,0");
                                TSCLIB_DLL.clearbuffer();
                            }
                            else//补打工位和散板
                            {
                                TSCLIB_DLL.openport(printer1.Text);
                                TSCLIB_DLL.setup("60", "40", "4", "8", "0", "1.5", "0");                           //Setup the media size and sensor type info
                                TSCLIB_DLL.sendcommand("GAP 2mm,0");
                                TSCLIB_DLL.clearbuffer();
                            }
                           
                            if (dealer.Length <= 2)
                            {
                                TSCLIB_DLL.windowsfont(10, 10, 35, 0, 0, 0, "黑体", dealer);
                            }
                            else
                            {
                                TSCLIB_DLL.windowsfont(10, 10, 30, 0, 0, 0, "黑体", dealer.Substring(0, 2));
                                TSCLIB_DLL.windowsfont(10, 40, 30, 0, 0, 0, "黑体", dealer.Substring(2));
                            }
                           if (!radioButton1.Checked && istemporary == true)
                            {
                                TSCLIB_DLL.windowsfont(420, 0, 50, 0, 0, 0, "ARIAL", "散");  //Draw windows font

                            }
                            else
                            {
                                TSCLIB_DLL.windowsfont(420, 0, 80, 0, 0, 0, "ARIAL", cnc_station);  //Draw windows font
                            }
                            TSCLIB_DLL.barcode("90", "0", "93", "80", "0", "0", "2", "5", barcode); //Drawing barcode
                           
                            TSCLIB_DLL.windowsfont(10, 80, 35, 0, 0, 0, "ARIAL", barcode);  //Draw windows font
                            TSCLIB_DLL.windowsfont(350, 80, 35, 0, 0, 0, "ARIAL", cnc_no);  //Draw windows font
                            TSCLIB_DLL.windowsfont(10, 110, 35, 0, 0, 0, "ARIAL", size);
                            TSCLIB_DLL.windowsfont(10, 140, 35, 0, 0, 0, "ARIAL", door_type);  //Draw windows font
                            TSCLIB_DLL.windowsfont(10, 170, 35, 0, 0, 0, "ARIAL", color);  //Draw windows font
                            TSCLIB_DLL.windowsfont(10, 200, 35, 0, 0, 0, "ARIAL", archaize);  //Draw windows font
                            TSCLIB_DLL.windowsfont(250, 200, 35, 0, 0, 0, "ARIAL", double_color);
                            TSCLIB_DLL.windowsfont(10, 230, 35, 0, 0, 0, "ARIAL", hole);
                            //TSCLIB_DLL.windowsfont(20, 240, 35, 0, 0, 0, "ARIAL", edge);  //Draw windows font
                            TSCLIB_DLL.windowsfont(10, 265, 35, 0, 0, 0, "ARIAL", order_id + "  共" + order_total_num.ToString() + "块" );  //Draw windows font

                            //TSCLIB_DLL.downloadpcx("UL.PCX", "UL.PCX");                                         //Download PCX file into printer
                            //TSCLIB_DLL.sendcommand("PUTPCX 100,400,\"UL.PCX\"");                                //Drawing PCX graphic
                            TSCLIB_DLL.printlabel("1", "1");                                                    //Print labels
                            TSCLIB_DLL.closeport();
                        }
                        else
                            MessageBox.Show("没有可用打印机！！请检查打印机设置");
                    }
                    else
                    {
                        MessageBox.Show("order_element_online中没有该条码！" + barcode_item);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("order_element_online中查询失败！" + barcode_item);
                Console.WriteLine("查询失败了--02！");
            }
            finally
            {
                reader1.Close();
            }
        }

       
        public void getUpdate(MySqlCommand mySqlCommand)
        {
            try
            {
                mySqlCommand.ExecuteNonQuery();
                Console.WriteLine("成功！");
            }
            catch (Exception ex)
            {
                String message = ex.Message;
                Console.WriteLine("修改数据失败了！" + message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
            
                
        
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //db_config = null;
            //db_config.Append("Database='db_hanju';Data Source='");
            //db_config.Append(textBox3.Text.ToString());
            //db_config.Append("';User Id='XLS';Password='12345678';CharSet='utf8'");
            //Console.WriteLine(db_config.ToString());
            //// mysql.Close();
            //mysql = getMySqlCon();
            ////MySqlConnection mysql = new MySqlConnection(db_config.ToString()); 
            //mysql_state = true;
        }
        
       

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string Password = Interaction.InputBox("请输入管理员密码", "数据库设置", "", 100, 100);
            if (Password == "0")
            {
                database_ip = Interaction.InputBox("请输入服务器IP", "数据库服务器修改", database_ip, 100, 100);
                RemoteCNC_config.Default.database_ip = database_ip;
                textBox3.Text = database_ip;
                database_name = Interaction.InputBox("请输入数据库名", "数据库服务器修改", database_name, 100, 100);
                RemoteCNC_config.Default.database_name = database_name;
                database_user = Interaction.InputBox("请输入登陆用户名", "数据库服务器修改", database_user, 100, 100);
                RemoteCNC_config.Default.database_user = database_user;
                database_pass = Interaction.InputBox("请输入登陆密码", "数据库服务器修改", database_pass, 100, 100);
                RemoteCNC_config.Default.database_pass = database_pass;
                RemoteCNC_config.Default.Save();
                mysql = getMySqlCon();
            }
    
               
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            RemoteCNC_config.Default.printer1 = printer1.Text;
            RemoteCNC_config.Default.printer2 = printer2.Text;
            RemoteCNC_config.Default.printer3 = printer3.Text;
            RemoteCNC_config.Default.CNC1 = CNC1.Text;
            RemoteCNC_config.Default.CNC2 = CNC2.Text;
            RemoteCNC_config.Default.CNC3 = CNC3.Text;
            RemoteCNC_config.Default.Save();
            System.Environment.Exit(0);
        }

        private void Firstly_excu()
        {
          

            if (RemoteCNC_config.Default.choose_state == "0")
            {
                radioButton1.Checked = true;
                radioButton1.Visible = true;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                RemoteCNC_config.Default.CNC状态 = radioButton1.Checked;
                RemoteCNC_config.Default.补打工位状态 = radioButton2.Checked;
                RemoteCNC_config.Default.质检工位状态 = radioButton3.Checked;
            }
            if (RemoteCNC_config.Default.choose_state == "1")
            {
                radioButton2.Checked = true;
                radioButton1.Visible = false;
                radioButton2.Visible = true;
                radioButton3.Visible = false;
            }
            if (RemoteCNC_config.Default.choose_state == "2")
            {
                radioButton3.Checked = true;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = true;
            }
            RemoteCNC_config.Default.CNC状态 = radioButton1.Checked;
            RemoteCNC_config.Default.补打工位状态 = radioButton2.Checked;
            RemoteCNC_config.Default.质检工位状态 = radioButton3.Checked;
            RemoteCNC_config.Default.Save();
        }

        private void BarcodePrinter_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void CNC1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            string Password = Interaction.InputBox("请输入管理员密码", "选择工位", "", 100, 100);
            if (Password == "0")
            {
                radioButton1.Visible = true;
                radioButton1.Checked = true;
                radioButton2.Visible = false;
                radioButton2.Checked = false;
                radioButton3.Visible = false;
                radioButton3.Checked = false;

                RemoteCNC_config.Default.choose_state = "0";
                RemoteCNC_config.Default.cnc_code ="100";
                RemoteCNC_config.Default.Save();
            }
            if (Password == "1")
            {
                radioButton1.Visible = false;
                radioButton1.Checked = false;
                radioButton2.Visible = true;
                radioButton2.Checked = true;
                radioButton3.Visible = false;
                radioButton3.Checked = false;

                RemoteCNC_config.Default.choose_state = "1";
                RemoteCNC_config.Default.cnc_code = "120";
                RemoteCNC_config.Default.Save();
            }
            if (Password == "2")
            {
                radioButton1.Visible = false;
                radioButton1.Checked = false;
                radioButton2.Visible = false;
                radioButton2.Checked = false;
                radioButton3.Visible = true;
                radioButton3.Checked = true;
                RemoteCNC_config.Default.choose_state = "2";
                RemoteCNC_config.Default.cnc_code = "130";
                RemoteCNC_config.Default.Save();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
