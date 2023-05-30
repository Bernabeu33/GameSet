#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;


namespace XLua
{
    public static partial class ObjectTranslator_Gen
    {
		public static void Init(LuaEnv luaenv, ObjectTranslator translator)
		{
		
			UnityEngineVector2_TypeID = -1;
		
			UnityEngineVector3_TypeID = -1;
		
			UnityEngineVector4_TypeID = -1;
		
			UnityEngineColor_TypeID = -1;
		
			UnityEngineQuaternion_TypeID = -1;
		
			UnityEngineRay_TypeID = -1;
		
			UnityEngineBounds_TypeID = -1;
		
			UnityEngineRay2D_TypeID = -1;
		
			UnityEngineTouchPhase_TypeID = -1;
			UnityEngineTouchPhase_EnumRef = -1;
		
			UnityEngineRenderMode_TypeID = -1;
			UnityEngineRenderMode_EnumRef = -1;
		
			UnityEngineUICanvasScalerScaleMode_TypeID = -1;
			UnityEngineUICanvasScalerScaleMode_EnumRef = -1;
		
			UnityEngineUICanvasScalerScreenMatchMode_TypeID = -1;
			UnityEngineUICanvasScalerScreenMatchMode_EnumRef = -1;
		
			UnityEngineKeyCode_TypeID = -1;
			UnityEngineKeyCode_EnumRef = -1;
		
			UnityEngineRuntimePlatform_TypeID = -1;
			UnityEngineRuntimePlatform_EnumRef = -1;
		

		
			translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector2>(translator.PushUnityEngineVector2, translator.GetUnityEngineVector2, translator.UpdateUnityEngineVector2);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector3>(translator.PushUnityEngineVector3, translator.GetUnityEngineVector3, translator.UpdateUnityEngineVector3);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.Vector4>(translator.PushUnityEngineVector4, translator.GetUnityEngineVector4, translator.UpdateUnityEngineVector4);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.Color>(translator.PushUnityEngineColor, translator.GetUnityEngineColor, translator.UpdateUnityEngineColor);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.Quaternion>(translator.PushUnityEngineQuaternion, translator.GetUnityEngineQuaternion, translator.UpdateUnityEngineQuaternion);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray>(translator.PushUnityEngineRay, translator.GetUnityEngineRay, translator.UpdateUnityEngineRay);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.Bounds>(translator.PushUnityEngineBounds, translator.GetUnityEngineBounds, translator.UpdateUnityEngineBounds);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.Ray2D>(translator.PushUnityEngineRay2D, translator.GetUnityEngineRay2D, translator.UpdateUnityEngineRay2D);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.TouchPhase>(translator.PushUnityEngineTouchPhase, translator.GetUnityEngineTouchPhase, translator.UpdateUnityEngineTouchPhase);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.RenderMode>(translator.PushUnityEngineRenderMode, translator.GetUnityEngineRenderMode, translator.UpdateUnityEngineRenderMode);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.CanvasScaler.ScaleMode>(translator.PushUnityEngineUICanvasScalerScaleMode, translator.GetUnityEngineUICanvasScalerScaleMode, translator.UpdateUnityEngineUICanvasScalerScaleMode);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.UI.CanvasScaler.ScreenMatchMode>(translator.PushUnityEngineUICanvasScalerScreenMatchMode, translator.GetUnityEngineUICanvasScalerScreenMatchMode, translator.UpdateUnityEngineUICanvasScalerScreenMatchMode);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.KeyCode>(translator.PushUnityEngineKeyCode, translator.GetUnityEngineKeyCode, translator.UpdateUnityEngineKeyCode);
			translator.RegisterPushAndGetAndUpdate<UnityEngine.RuntimePlatform>(translator.PushUnityEngineRuntimePlatform, translator.GetUnityEngineRuntimePlatform, translator.UpdateUnityEngineRuntimePlatform);
		
		}
        
        static int UnityEngineVector2_TypeID = -1;
        public static void PushUnityEngineVector2(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.Vector2 val)
        {
            if (UnityEngineVector2_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector2_TypeID = thiz.getTypeId(L, typeof(UnityEngine.Vector2), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 8, UnityEngineVector2_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector2 ,value="+val);
            }
			
        }
		
        public static void GetUnityEngineVector2(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.Vector2 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue_Gen.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector2");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue_Gen.UnPack(thiz, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector2)thiz.objectCasters.GetCaster(typeof(UnityEngine.Vector2))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineVector2(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.Vector2 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector2_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector2");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector2 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineVector3_TypeID = -1;
        public static void PushUnityEngineVector3(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.Vector3 val)
        {
            if (UnityEngineVector3_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector3_TypeID = thiz.getTypeId(L, typeof(UnityEngine.Vector3), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 12, UnityEngineVector3_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector3 ,value="+val);
            }
			
        }
		
        public static void GetUnityEngineVector3(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.Vector3 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue_Gen.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector3");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue_Gen.UnPack(thiz, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector3)thiz.objectCasters.GetCaster(typeof(UnityEngine.Vector3))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineVector3(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.Vector3 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector3_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector3");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector3 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineVector4_TypeID = -1;
        public static void PushUnityEngineVector4(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.Vector4 val)
        {
            if (UnityEngineVector4_TypeID == -1)
            {
			    bool is_first;
                UnityEngineVector4_TypeID = thiz.getTypeId(L, typeof(UnityEngine.Vector4), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineVector4_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Vector4 ,value="+val);
            }
			
        }
		
        public static void GetUnityEngineVector4(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.Vector4 val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue_Gen.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Vector4");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue_Gen.UnPack(thiz, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Vector4)thiz.objectCasters.GetCaster(typeof(UnityEngine.Vector4))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineVector4(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.Vector4 val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineVector4_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Vector4");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Vector4 ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineColor_TypeID = -1;
        public static void PushUnityEngineColor(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.Color val)
        {
            if (UnityEngineColor_TypeID == -1)
            {
			    bool is_first;
                UnityEngineColor_TypeID = thiz.getTypeId(L, typeof(UnityEngine.Color), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineColor_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Color ,value="+val);
            }
			
        }
		
        public static void GetUnityEngineColor(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.Color val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue_Gen.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Color");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue_Gen.UnPack(thiz, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Color)thiz.objectCasters.GetCaster(typeof(UnityEngine.Color))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineColor(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.Color val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineColor_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Color");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Color ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineQuaternion_TypeID = -1;
        public static void PushUnityEngineQuaternion(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.Quaternion val)
        {
            if (UnityEngineQuaternion_TypeID == -1)
            {
			    bool is_first;
                UnityEngineQuaternion_TypeID = thiz.getTypeId(L, typeof(UnityEngine.Quaternion), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineQuaternion_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Quaternion ,value="+val);
            }
			
        }
		
        public static void GetUnityEngineQuaternion(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.Quaternion val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue_Gen.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Quaternion");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue_Gen.UnPack(thiz, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Quaternion)thiz.objectCasters.GetCaster(typeof(UnityEngine.Quaternion))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineQuaternion(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.Quaternion val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineQuaternion_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Quaternion");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Quaternion ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineRay_TypeID = -1;
        public static void PushUnityEngineRay(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.Ray val)
        {
            if (UnityEngineRay_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay_TypeID = thiz.getTypeId(L, typeof(UnityEngine.Ray), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineRay_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray ,value="+val);
            }
			
        }
		
        public static void GetUnityEngineRay(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.Ray val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue_Gen.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue_Gen.UnPack(thiz, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray)thiz.objectCasters.GetCaster(typeof(UnityEngine.Ray))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineRay(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.Ray val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineBounds_TypeID = -1;
        public static void PushUnityEngineBounds(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.Bounds val)
        {
            if (UnityEngineBounds_TypeID == -1)
            {
			    bool is_first;
                UnityEngineBounds_TypeID = thiz.getTypeId(L, typeof(UnityEngine.Bounds), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 24, UnityEngineBounds_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Bounds ,value="+val);
            }
			
        }
		
        public static void GetUnityEngineBounds(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.Bounds val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue_Gen.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Bounds");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue_Gen.UnPack(thiz, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Bounds)thiz.objectCasters.GetCaster(typeof(UnityEngine.Bounds))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineBounds(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.Bounds val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineBounds_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Bounds");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Bounds ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineRay2D_TypeID = -1;
        public static void PushUnityEngineRay2D(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.Ray2D val)
        {
            if (UnityEngineRay2D_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRay2D_TypeID = thiz.getTypeId(L, typeof(UnityEngine.Ray2D), out is_first);
				
            }
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 16, UnityEngineRay2D_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, val))
            {
                throw new Exception("pack fail fail for UnityEngine.Ray2D ,value="+val);
            }
			
        }
		
        public static void GetUnityEngineRay2D(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.Ray2D val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);if (!CopyByValue_Gen.UnPack(buff, 0, out val))
                {
                    throw new Exception("unpack fail for UnityEngine.Ray2D");
                }
            }
			else if (type ==LuaTypes.LUA_TTABLE)
			{
			    CopyByValue_Gen.UnPack(thiz, L, index, out val);
			}
            else
            {
                val = (UnityEngine.Ray2D)thiz.objectCasters.GetCaster(typeof(UnityEngine.Ray2D))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineRay2D(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.Ray2D val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRay2D_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.Ray2D");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  val))
                {
                    throw new Exception("pack fail for UnityEngine.Ray2D ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineTouchPhase_TypeID = -1;
		static int UnityEngineTouchPhase_EnumRef = -1;
        
        public static void PushUnityEngineTouchPhase(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.TouchPhase val)
        {
            if (UnityEngineTouchPhase_TypeID == -1)
            {
			    bool is_first;
                UnityEngineTouchPhase_TypeID = thiz.getTypeId(L, typeof(UnityEngine.TouchPhase), out is_first);
				
				if (UnityEngineTouchPhase_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.TouchPhase));
				    UnityEngineTouchPhase_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineTouchPhase_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineTouchPhase_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.TouchPhase ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineTouchPhase_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public static void GetUnityEngineTouchPhase(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.TouchPhase val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineTouchPhase_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.TouchPhase");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue_Gen.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.TouchPhase");
                }
				val = (UnityEngine.TouchPhase)e;
                
            }
            else
            {
                val = (UnityEngine.TouchPhase)thiz.objectCasters.GetCaster(typeof(UnityEngine.TouchPhase))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineTouchPhase(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.TouchPhase val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineTouchPhase_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.TouchPhase");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.TouchPhase ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineRenderMode_TypeID = -1;
		static int UnityEngineRenderMode_EnumRef = -1;
        
        public static void PushUnityEngineRenderMode(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.RenderMode val)
        {
            if (UnityEngineRenderMode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRenderMode_TypeID = thiz.getTypeId(L, typeof(UnityEngine.RenderMode), out is_first);
				
				if (UnityEngineRenderMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.RenderMode));
				    UnityEngineRenderMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineRenderMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineRenderMode_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.RenderMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineRenderMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public static void GetUnityEngineRenderMode(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.RenderMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRenderMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RenderMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue_Gen.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.RenderMode");
                }
				val = (UnityEngine.RenderMode)e;
                
            }
            else
            {
                val = (UnityEngine.RenderMode)thiz.objectCasters.GetCaster(typeof(UnityEngine.RenderMode))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineRenderMode(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.RenderMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRenderMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RenderMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.RenderMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineUICanvasScalerScaleMode_TypeID = -1;
		static int UnityEngineUICanvasScalerScaleMode_EnumRef = -1;
        
        public static void PushUnityEngineUICanvasScalerScaleMode(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.UI.CanvasScaler.ScaleMode val)
        {
            if (UnityEngineUICanvasScalerScaleMode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUICanvasScalerScaleMode_TypeID = thiz.getTypeId(L, typeof(UnityEngine.UI.CanvasScaler.ScaleMode), out is_first);
				
				if (UnityEngineUICanvasScalerScaleMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.CanvasScaler.ScaleMode));
				    UnityEngineUICanvasScalerScaleMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUICanvasScalerScaleMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUICanvasScalerScaleMode_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.CanvasScaler.ScaleMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUICanvasScalerScaleMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public static void GetUnityEngineUICanvasScalerScaleMode(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.UI.CanvasScaler.ScaleMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUICanvasScalerScaleMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.CanvasScaler.ScaleMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue_Gen.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.CanvasScaler.ScaleMode");
                }
				val = (UnityEngine.UI.CanvasScaler.ScaleMode)e;
                
            }
            else
            {
                val = (UnityEngine.UI.CanvasScaler.ScaleMode)thiz.objectCasters.GetCaster(typeof(UnityEngine.UI.CanvasScaler.ScaleMode))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineUICanvasScalerScaleMode(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.UI.CanvasScaler.ScaleMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUICanvasScalerScaleMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.CanvasScaler.ScaleMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.CanvasScaler.ScaleMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineUICanvasScalerScreenMatchMode_TypeID = -1;
		static int UnityEngineUICanvasScalerScreenMatchMode_EnumRef = -1;
        
        public static void PushUnityEngineUICanvasScalerScreenMatchMode(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.UI.CanvasScaler.ScreenMatchMode val)
        {
            if (UnityEngineUICanvasScalerScreenMatchMode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineUICanvasScalerScreenMatchMode_TypeID = thiz.getTypeId(L, typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode), out is_first);
				
				if (UnityEngineUICanvasScalerScreenMatchMode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode));
				    UnityEngineUICanvasScalerScreenMatchMode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineUICanvasScalerScreenMatchMode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineUICanvasScalerScreenMatchMode_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.UI.CanvasScaler.ScreenMatchMode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineUICanvasScalerScreenMatchMode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public static void GetUnityEngineUICanvasScalerScreenMatchMode(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.UI.CanvasScaler.ScreenMatchMode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUICanvasScalerScreenMatchMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.CanvasScaler.ScreenMatchMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue_Gen.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.UI.CanvasScaler.ScreenMatchMode");
                }
				val = (UnityEngine.UI.CanvasScaler.ScreenMatchMode)e;
                
            }
            else
            {
                val = (UnityEngine.UI.CanvasScaler.ScreenMatchMode)thiz.objectCasters.GetCaster(typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineUICanvasScalerScreenMatchMode(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.UI.CanvasScaler.ScreenMatchMode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineUICanvasScalerScreenMatchMode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.UI.CanvasScaler.ScreenMatchMode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.UI.CanvasScaler.ScreenMatchMode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineKeyCode_TypeID = -1;
		static int UnityEngineKeyCode_EnumRef = -1;
        
        public static void PushUnityEngineKeyCode(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.KeyCode val)
        {
            if (UnityEngineKeyCode_TypeID == -1)
            {
			    bool is_first;
                UnityEngineKeyCode_TypeID = thiz.getTypeId(L, typeof(UnityEngine.KeyCode), out is_first);
				
				if (UnityEngineKeyCode_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.KeyCode));
				    UnityEngineKeyCode_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineKeyCode_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineKeyCode_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.KeyCode ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineKeyCode_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public static void GetUnityEngineKeyCode(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.KeyCode val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineKeyCode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.KeyCode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue_Gen.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.KeyCode");
                }
				val = (UnityEngine.KeyCode)e;
                
            }
            else
            {
                val = (UnityEngine.KeyCode)thiz.objectCasters.GetCaster(typeof(UnityEngine.KeyCode))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineKeyCode(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.KeyCode val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineKeyCode_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.KeyCode");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.KeyCode ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        static int UnityEngineRuntimePlatform_TypeID = -1;
		static int UnityEngineRuntimePlatform_EnumRef = -1;
        
        public static void PushUnityEngineRuntimePlatform(this ObjectTranslator thiz, RealStatePtr L, UnityEngine.RuntimePlatform val)
        {
            if (UnityEngineRuntimePlatform_TypeID == -1)
            {
			    bool is_first;
                UnityEngineRuntimePlatform_TypeID = thiz.getTypeId(L, typeof(UnityEngine.RuntimePlatform), out is_first);
				
				if (UnityEngineRuntimePlatform_EnumRef == -1)
				{
				    Utils.LoadCSTable(L, typeof(UnityEngine.RuntimePlatform));
				    UnityEngineRuntimePlatform_EnumRef = LuaAPI.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX);
				}
				
            }
			
			if (LuaAPI.xlua_tryget_cachedud(L, (int)val, UnityEngineRuntimePlatform_EnumRef) == 1)
            {
			    return;
			}
			
            IntPtr buff = LuaAPI.xlua_pushstruct(L, 4, UnityEngineRuntimePlatform_TypeID);
            if (!CopyByValue_Gen.Pack(buff, 0, (int)val))
            {
                throw new Exception("pack fail fail for UnityEngine.RuntimePlatform ,value="+val);
            }
			
			LuaAPI.lua_getref(L, UnityEngineRuntimePlatform_EnumRef);
			LuaAPI.lua_pushvalue(L, -2);
			LuaAPI.xlua_rawseti(L, -2, (int)val);
			LuaAPI.lua_pop(L, 1);
			
        }
		
        public static void GetUnityEngineRuntimePlatform(this ObjectTranslator thiz, RealStatePtr L, int index, out UnityEngine.RuntimePlatform val)
        {
		    LuaTypes type = LuaAPI.lua_type(L, index);
            if (type == LuaTypes.LUA_TUSERDATA )
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRuntimePlatform_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RuntimePlatform");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
				int e;
                if (!CopyByValue_Gen.UnPack(buff, 0, out e))
                {
                    throw new Exception("unpack fail for UnityEngine.RuntimePlatform");
                }
				val = (UnityEngine.RuntimePlatform)e;
                
            }
            else
            {
                val = (UnityEngine.RuntimePlatform)thiz.objectCasters.GetCaster(typeof(UnityEngine.RuntimePlatform))(L, index, null);
            }
        }
		
        public static void UpdateUnityEngineRuntimePlatform(this ObjectTranslator thiz, RealStatePtr L, int index, UnityEngine.RuntimePlatform val)
        {
		    
            if (LuaAPI.lua_type(L, index) == LuaTypes.LUA_TUSERDATA)
            {
			    if (LuaAPI.xlua_gettypeid(L, index) != UnityEngineRuntimePlatform_TypeID)
				{
				    throw new Exception("invalid userdata for UnityEngine.RuntimePlatform");
				}
				
                IntPtr buff = LuaAPI.lua_touserdata(L, index);
                if (!CopyByValue_Gen.Pack(buff, 0,  (int)val))
                {
                    throw new Exception("pack fail for UnityEngine.RuntimePlatform ,value="+val);
                }
            }
			
            else
            {
                throw new Exception("try to update a data with lua type:" + LuaAPI.lua_type(L, index));
            }
        }
        
        
		// table cast optimze
		
        
    }
	
	public partial class StaticLuaCallbacks_Wrap
    {
	    internal static bool __tryArrayGet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int index)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.PushUnityEngineVector2(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.PushUnityEngineVector3(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.PushUnityEngineVector4(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.PushUnityEngineColor(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.PushUnityEngineQuaternion(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.PushUnityEngineRay(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.PushUnityEngineBounds(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.PushUnityEngineRay2D(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.TouchPhase[]))
			{
			    UnityEngine.TouchPhase[] array = obj as UnityEngine.TouchPhase[];
				translator.PushUnityEngineTouchPhase(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.RenderMode[]))
			{
			    UnityEngine.RenderMode[] array = obj as UnityEngine.RenderMode[];
				translator.PushUnityEngineRenderMode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.CanvasScaler.ScaleMode[]))
			{
			    UnityEngine.UI.CanvasScaler.ScaleMode[] array = obj as UnityEngine.UI.CanvasScaler.ScaleMode[];
				translator.PushUnityEngineUICanvasScalerScaleMode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode[]))
			{
			    UnityEngine.UI.CanvasScaler.ScreenMatchMode[] array = obj as UnityEngine.UI.CanvasScaler.ScreenMatchMode[];
				translator.PushUnityEngineUICanvasScalerScreenMatchMode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.KeyCode[]))
			{
			    UnityEngine.KeyCode[] array = obj as UnityEngine.KeyCode[];
				translator.PushUnityEngineKeyCode(L, array[index]);
				return true;
			}
			else if (type == typeof(UnityEngine.RuntimePlatform[]))
			{
			    UnityEngine.RuntimePlatform[] array = obj as UnityEngine.RuntimePlatform[];
				translator.PushUnityEngineRuntimePlatform(L, array[index]);
				return true;
			}
            return false;
		}
		
		internal static bool __tryArraySet(Type type, RealStatePtr L, ObjectTranslator translator, object obj, int array_idx, int obj_idx)
		{
		
			if (type == typeof(UnityEngine.Vector2[]))
			{
			    UnityEngine.Vector2[] array = obj as UnityEngine.Vector2[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector3[]))
			{
			    UnityEngine.Vector3[] array = obj as UnityEngine.Vector3[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Vector4[]))
			{
			    UnityEngine.Vector4[] array = obj as UnityEngine.Vector4[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Color[]))
			{
			    UnityEngine.Color[] array = obj as UnityEngine.Color[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Quaternion[]))
			{
			    UnityEngine.Quaternion[] array = obj as UnityEngine.Quaternion[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray[]))
			{
			    UnityEngine.Ray[] array = obj as UnityEngine.Ray[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Bounds[]))
			{
			    UnityEngine.Bounds[] array = obj as UnityEngine.Bounds[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.Ray2D[]))
			{
			    UnityEngine.Ray2D[] array = obj as UnityEngine.Ray2D[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.TouchPhase[]))
			{
			    UnityEngine.TouchPhase[] array = obj as UnityEngine.TouchPhase[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.RenderMode[]))
			{
			    UnityEngine.RenderMode[] array = obj as UnityEngine.RenderMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.CanvasScaler.ScaleMode[]))
			{
			    UnityEngine.UI.CanvasScaler.ScaleMode[] array = obj as UnityEngine.UI.CanvasScaler.ScaleMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode[]))
			{
			    UnityEngine.UI.CanvasScaler.ScreenMatchMode[] array = obj as UnityEngine.UI.CanvasScaler.ScreenMatchMode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.KeyCode[]))
			{
			    UnityEngine.KeyCode[] array = obj as UnityEngine.KeyCode[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
			else if (type == typeof(UnityEngine.RuntimePlatform[]))
			{
			    UnityEngine.RuntimePlatform[] array = obj as UnityEngine.RuntimePlatform[];
				translator.Get(L, obj_idx, out array[array_idx]);
				return true;
			}
            return false;
		}
	}
}