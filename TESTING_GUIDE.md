# 🧪 Scene Management Tools Testing Guide

## 🔍 **Current Testing Status**

After initial testing, several issues were identified that need addressing:

### **Issue 1: Tools Not Available**
- ❌ `create_scene` tool not found in MCP tool list
- ❌ Scene management tools not loaded in MCP server
- ❌ Enhanced fork features not accessible

### **Issue 2: Potential Fixes Needed**

## 🔧 **Immediate Action Required**

### **Step 1: Restart MCP Server in Unity**
1. **In Unity**: `Tools > MCP Unity > Server Window`
2. **Click "Stop Server"**
3. **Wait 5 seconds** 
4. **Click "Start Server"**
5. **Verify "Running" status**

### **Step 2: Verify Package Version**
1. **Package Manager**: `Window > Package Manager`
2. **Find "MCP Unity"** in list
3. **Check version** matches latest commit
4. **Update if needed**

### **Step 3: Test Commands**
After server restart, test these commands:

```bash
# Test 1: Basic scene creation
"Create a new 3D scene called 'TestScene'"

# Test 2: Scene information
"Show me all scenes in the project"

# Test 3: Scene saving
"Save the current scene"
```

## 🛠 **Debugging Steps**

### **If Tools Still Not Available:**

1. **Check Unity Console** for any error messages
2. **Verify package installation**: Remove and reinstall from git URL
3. **Check Node.js server** is properly built and running

### **Manual Verification in Unity:**
1. **Check if new C# files exist** in Package Manager → MCP Unity → Editor/Tools/
2. **Verify compilation** - no red errors in Console
3. **Check server logs** in MCP Unity Server Window

## 🎯 **Expected Results**

When working correctly, you should see:
- ✅ `create_scene` tool available in MCP
- ✅ `load_scene` tool available in MCP  
- ✅ `save_scene` tool available in MCP
- ✅ `unity://scenes` resource available
- ✅ Successful scene creation without dialogs
- ✅ Automatic scene saving and build settings management

## 🔍 **Troubleshooting Commands**

### **Test Basic Connectivity:**
```
"Send a console log to Unity saying 'MCP connection test'"
```

### **Test Enhanced Features:**
```
"Create a scene called 'ConnectionTest' with 3D template"
```

### **Test Resource Access:**
```
"List all available Unity scenes and their properties"
```

## 📝 **Known Issues & Solutions**

### **Issue**: "Request timed out" errors
**Solution**: Restart MCP server in Unity

### **Issue**: Scene tools not found
**Solution**: Update package to latest commit from GitHub

### **Issue**: Compilation errors
**Solution**: Ensure Newtonsoft.Json package is installed

---

## 🚀 **Next Steps After Fix**

Once the scene tools are working, we can test:
1. **Full scene workflow** - create, modify, save
2. **Build settings integration** 
3. **Different scene templates**
4. **Complex scene operations**

The enhanced MCP Unity fork should revolutionize your Unity development workflow once these initial setup issues are resolved!