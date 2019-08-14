﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BudStudio.DgvColumn.BLL;
using BudStudio.DgvColumn.COL;
using BudStudio.DgvColumn.SFL;

namespace BudStudio.DgvColumn.UIL.ManageDepartment
{
    /// <summary>
    /// 对象名称：“部门管理模块”修改界面控件（用户界面层）
    /// 对象说明：该界面用于修改“部门信息”对象并提交至数据库，修改操作成功后返回列表界面。
    /// 作者姓名：爱英思躺
    /// 编写日期：2012/02/29 16:07:34
    /// </summary>
    public partial class Modify : UserControl
    {
        private Department department;
        private static Modify instance;

        /// <summary>
        /// 返回一个该控件的实例。如果之前该控件已经被创建，直接返回已创建的控件。
        /// 此处采用单键模式对控件实例进行缓存，避免因界面切换重复创建和销毁对象。
        /// </summary>
        public static Modify Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Modify();
                }

                if (instance.department == null)
                    throw new CustomException("在调用本模块修改界面的实例前，必须调用Modify.LoadDataById方法，装载所要修改的关联对象。",ExceptionType.Error);

                instance.BindObjectToForm(); // 每次返回该控件的实例前，都将关联对象当前的值，绑定至界面控件进行显示。
                return instance;
            }
        }

        /// <summary>
        /// 根据关联对象的主键，从数据库中获取关联对象信息，以备进行修改操作。
        /// </summary>
        public static void LoadDataById(string departmentId)
        {
            if (instance == null)
            {
                instance = new Modify();
            }
            instance.department = DepartmentBLL.GetDataById(departmentId);
            if (instance.department == null)
                throw new CustomException("对不起，所要修改的信息不存在或已删除。");
        }

        /// <summary>
        /// 私有的控件实例化方法，创建实例只能通过该控件的Instance属性实现。
        /// </summary>
        private Modify()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// 用户单击“保存”按钮时的事件处理方法。
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            BindFormlToObject(); // 进行数据绑定
            DepartmentBLL.Update(department); // 调用“业务逻辑层”的方法，检查有效性后更新至数据库。
            FormSysMessage.ShowSuccessMsg("“部门信息”修改成功，单击“确定”按钮返回信息列表。");
            FormMain.LoadNewControl(Default.Instance); // 载入该模块的信息列表界面至主窗体显示。
        }

        /// <summary>
        /// 用户单击“取消”按钮时的事件处理方法。
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            FormMain.LoadNewControl(Default.Instance); // 载入该模块的信息列表界面至主窗体显示。
        }


        #region 界面控件与关联对象之间的绑定方法

        /// <summary>
        /// 将界面控件中的值，绑定给关联对象。
        /// </summary>
        private void BindFormlToObject()
        {
            department.DepartmentName = DataValid.GetNullOrString(TxtDepartmentName.Text);  // 部门名称
            department.ManagerName = DataValid.GetNullOrString(TxtManagerName.Text);  // 经理姓名
            department.ContactPhone = DataValid.GetNullOrString(TxtContactPhone.Text);  // 联系电话
            department.NoteDescription = DataValid.GetNullOrString(TxtNoteDescription.Text);  // 备注说明
        }

        /// <summary>
        /// 将关联对象中的值，绑定至界面控件进行显示。
        /// </summary>
        private void BindObjectToForm()
        {
            TxtDepartmentName.Text = department.DepartmentName;  // 部门名称
            TxtManagerName.Text = department.ManagerName;  // 经理姓名
            TxtContactPhone.Text = department.ContactPhone;  // 联系电话
            TxtNoteDescription.Text = department.NoteDescription;  // 备注说明
        }

        #endregion 界面控件与关联对象之间的绑定方法


    }
}
