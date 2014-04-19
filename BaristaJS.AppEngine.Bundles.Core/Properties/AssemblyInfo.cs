using System.Reflection;
using System.Runtime.InteropServices;
using BaristaJS.AppEngine;
using BaristaJS.AppEngine.Bundles;

[assembly: AssemblyTitle("BaristaJS.AppEngine.Bundles.Core")]
[assembly: AssemblyProduct("BaristaJS")]
[assembly: AssemblyDescription("Core BaristaJS Bundle")]
[assembly: AssemblyCopyright("(c) SixConcepts LLC")]

[assembly: ComVisible(false)]
[assembly: AssemblyVersion("0.0.0.14")]
[assembly: AssemblyFileVersion("0.0.0.14")]
[assembly: BaristaJSBundle(typeof(BaristaBundle))]