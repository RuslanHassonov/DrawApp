﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DrawApp
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="2018_Hassonov_Ruslan_C#")]
	public partial class SQLServer_DrawAppDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSAVED_COLOR(SAVED_COLOR instance);
    partial void UpdateSAVED_COLOR(SAVED_COLOR instance);
    partial void DeleteSAVED_COLOR(SAVED_COLOR instance);
    partial void InsertSAVED_SHAPE(SAVED_SHAPE instance);
    partial void UpdateSAVED_SHAPE(SAVED_SHAPE instance);
    partial void DeleteSAVED_SHAPE(SAVED_SHAPE instance);
    #endregion
		
		public SQLServer_DrawAppDataContext() : 
				base(global::DrawApp.Properties.Settings.Default._2018_Hassonov_Ruslan_C_ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SQLServer_DrawAppDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SQLServer_DrawAppDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SQLServer_DrawAppDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SQLServer_DrawAppDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SAVED_COLOR> SAVED_COLORs
		{
			get
			{
				return this.GetTable<SAVED_COLOR>();
			}
		}
		
		public System.Data.Linq.Table<SAVED_SHAPE> SAVED_SHAPEs
		{
			get
			{
				return this.GetTable<SAVED_SHAPE>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SAVED_COLOR")]
	public partial class SAVED_COLOR : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Color_ID;
		
		private System.Nullable<int> _Red;
		
		private System.Nullable<int> _Green;
		
		private System.Nullable<int> _Blue;
		
		private EntitySet<SAVED_SHAPE> _SAVED_SHAPEs;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnColor_IDChanging(int value);
    partial void OnColor_IDChanged();
    partial void OnRedChanging(System.Nullable<int> value);
    partial void OnRedChanged();
    partial void OnGreenChanging(System.Nullable<int> value);
    partial void OnGreenChanged();
    partial void OnBlueChanging(System.Nullable<int> value);
    partial void OnBlueChanged();
    #endregion
		
		public SAVED_COLOR()
		{
			this._SAVED_SHAPEs = new EntitySet<SAVED_SHAPE>(new Action<SAVED_SHAPE>(this.attach_SAVED_SHAPEs), new Action<SAVED_SHAPE>(this.detach_SAVED_SHAPEs));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Color_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Color_ID
		{
			get
			{
				return this._Color_ID;
			}
			set
			{
				if ((this._Color_ID != value))
				{
					this.OnColor_IDChanging(value);
					this.SendPropertyChanging();
					this._Color_ID = value;
					this.SendPropertyChanged("Color_ID");
					this.OnColor_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Red", DbType="Int")]
		public System.Nullable<int> Red
		{
			get
			{
				return this._Red;
			}
			set
			{
				if ((this._Red != value))
				{
					this.OnRedChanging(value);
					this.SendPropertyChanging();
					this._Red = value;
					this.SendPropertyChanged("Red");
					this.OnRedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Green", DbType="Int")]
		public System.Nullable<int> Green
		{
			get
			{
				return this._Green;
			}
			set
			{
				if ((this._Green != value))
				{
					this.OnGreenChanging(value);
					this.SendPropertyChanging();
					this._Green = value;
					this.SendPropertyChanged("Green");
					this.OnGreenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Blue", DbType="Int")]
		public System.Nullable<int> Blue
		{
			get
			{
				return this._Blue;
			}
			set
			{
				if ((this._Blue != value))
				{
					this.OnBlueChanging(value);
					this.SendPropertyChanging();
					this._Blue = value;
					this.SendPropertyChanged("Blue");
					this.OnBlueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="SAVED_COLOR_SAVED_SHAPE", Storage="_SAVED_SHAPEs", ThisKey="Color_ID", OtherKey="Color_ID")]
		public EntitySet<SAVED_SHAPE> SAVED_SHAPEs
		{
			get
			{
				return this._SAVED_SHAPEs;
			}
			set
			{
				this._SAVED_SHAPEs.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_SAVED_SHAPEs(SAVED_SHAPE entity)
		{
			this.SendPropertyChanging();
			entity.SAVED_COLOR = this;
		}
		
		private void detach_SAVED_SHAPEs(SAVED_SHAPE entity)
		{
			this.SendPropertyChanging();
			entity.SAVED_COLOR = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SAVED_SHAPE")]
	public partial class SAVED_SHAPE : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Shape_ID;
		
		private string _Shape;
		
		private System.Nullable<int> _Width;
		
		private System.Nullable<int> _Height;
		
		private System.Nullable<int> _Color_ID;
		
		private EntityRef<SAVED_COLOR> _SAVED_COLOR;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnShape_IDChanging(int value);
    partial void OnShape_IDChanged();
    partial void OnShapeChanging(string value);
    partial void OnShapeChanged();
    partial void OnWidthChanging(System.Nullable<int> value);
    partial void OnWidthChanged();
    partial void OnHeightChanging(System.Nullable<int> value);
    partial void OnHeightChanged();
    partial void OnColor_IDChanging(System.Nullable<int> value);
    partial void OnColor_IDChanged();
    #endregion
		
		public SAVED_SHAPE()
		{
			this._SAVED_COLOR = default(EntityRef<SAVED_COLOR>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Shape_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Shape_ID
		{
			get
			{
				return this._Shape_ID;
			}
			set
			{
				if ((this._Shape_ID != value))
				{
					this.OnShape_IDChanging(value);
					this.SendPropertyChanging();
					this._Shape_ID = value;
					this.SendPropertyChanged("Shape_ID");
					this.OnShape_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Shape", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Shape
		{
			get
			{
				return this._Shape;
			}
			set
			{
				if ((this._Shape != value))
				{
					this.OnShapeChanging(value);
					this.SendPropertyChanging();
					this._Shape = value;
					this.SendPropertyChanged("Shape");
					this.OnShapeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Width", DbType="Int")]
		public System.Nullable<int> Width
		{
			get
			{
				return this._Width;
			}
			set
			{
				if ((this._Width != value))
				{
					this.OnWidthChanging(value);
					this.SendPropertyChanging();
					this._Width = value;
					this.SendPropertyChanged("Width");
					this.OnWidthChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Height", DbType="Int")]
		public System.Nullable<int> Height
		{
			get
			{
				return this._Height;
			}
			set
			{
				if ((this._Height != value))
				{
					this.OnHeightChanging(value);
					this.SendPropertyChanging();
					this._Height = value;
					this.SendPropertyChanged("Height");
					this.OnHeightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Color_ID", DbType="Int")]
		public System.Nullable<int> Color_ID
		{
			get
			{
				return this._Color_ID;
			}
			set
			{
				if ((this._Color_ID != value))
				{
					if (this._SAVED_COLOR.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnColor_IDChanging(value);
					this.SendPropertyChanging();
					this._Color_ID = value;
					this.SendPropertyChanged("Color_ID");
					this.OnColor_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="SAVED_COLOR_SAVED_SHAPE", Storage="_SAVED_COLOR", ThisKey="Color_ID", OtherKey="Color_ID", IsForeignKey=true)]
		public SAVED_COLOR SAVED_COLOR
		{
			get
			{
				return this._SAVED_COLOR.Entity;
			}
			set
			{
				SAVED_COLOR previousValue = this._SAVED_COLOR.Entity;
				if (((previousValue != value) 
							|| (this._SAVED_COLOR.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._SAVED_COLOR.Entity = null;
						previousValue.SAVED_SHAPEs.Remove(this);
					}
					this._SAVED_COLOR.Entity = value;
					if ((value != null))
					{
						value.SAVED_SHAPEs.Add(this);
						this._Color_ID = value.Color_ID;
					}
					else
					{
						this._Color_ID = default(Nullable<int>);
					}
					this.SendPropertyChanged("SAVED_COLOR");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591