using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace FunctionGraphics
{
    public static class Compiler
    {
        readonly static string CodeTemplate = @"
using System;
class Executor
{
    public static float Execute(float x)
    {
        <code>
    }
}
";

        public static Func<float, float> GetDelegate(string code)
        {
            CompilerParameters options = new CompilerParameters
            {
                GenerateInMemory = true
            };
            CompilerResults result = new CSharpCodeProvider().CompileAssemblyFromSource(options, CodeTemplate.Replace("<code>", code));
            if (!result.Errors.HasErrors)
                return (Func<float, float>)Delegate.CreateDelegate(
                    typeof(Func<float, float>),
                    result.CompiledAssembly.GetType("Executor").GetMethod(
                        "Execute",
                        BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public));
            return null;
        }
    }
}
