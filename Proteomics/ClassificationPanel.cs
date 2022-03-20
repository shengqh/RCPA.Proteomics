using RCPA.Gui;
using RCPA.Gui.FileArgument;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RCPA.Proteomics
{
  public delegate void GetDataEventHandler(object sender, EventArgs e);
  public delegate string GetNameHandler(string sourceName);

  public partial class ClassificationPanel : UserControl, IRcpaComponent
  {
    public ClassificationPanel()
    {
      InitializeComponent();
      this.Pattern = "(.*)";
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
      base.OnPaint(pe);
    }

    [Category("Description"), DescriptionAttribute("Gets or sets the description"), DefaultValue("Classifications")]
    public string Description
    {
      get { return lblDescription.Text; }
      set { lblDescription.Text = value; }
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public event GetDataEventHandler GetData;

    public GetNameHandler GetName { get; set; }

    private OpenFileArgument openFile = new OpenFileArgument("Classification Set", new string[] { "set", "xml" });
    private SaveFileArgument saveFile = new SaveFileArgument("Classification Set", "set");

    protected void OnGetData(EventArgs e)
    {
      if (GetData != null)
        GetData(this, e);
    }

    protected string OnGetName(string oldName)
    {
      if (GetName != null)
      {
        return GetName(oldName);
      }
      return oldName;
    }

    public Dictionary<string, List<string>> GetClassificationSet()
    {
      var result = new Dictionary<string, List<string>>();

      for (int i = 0; i < tvClassifications.Nodes.Count; i++)
      {
        TreeNode node = tvClassifications.Nodes[i];
        List<string> lstExp = new List<string>();
        result[node.Text] = lstExp;
        for (int j = 0; j < node.Nodes.Count; j++)
        {
          lstExp.Add(node.Nodes[j].Text);
        }
      }

      return result;
    }

    public void SetClassificationSet(Dictionary<string, List<string>> map)
    {
      var keys = map.Keys.OrderBy(m => m).ToList();
      tvClassifications.BeginUpdate();
      try
      {
        tvClassifications.Nodes.Clear();
        foreach (var key in keys)
        {
          var node = tvClassifications.Nodes.Add(key);
          var values = map[key].OrderBy(m => m).ToList();
          foreach (var v in values)
          {
            node.Nodes.Add(v);
          }
        }
      }
      finally
      {
        tvClassifications.EndUpdate();
      }
    }

    public Dictionary<string, HashSet<string>> GetClassificationMap()
    {
      var result = new Dictionary<string, HashSet<string>>();

      for (int i = 0; i < tvClassifications.Nodes.Count; i++)
      {
        TreeNode node = tvClassifications.Nodes[i];
        var lstExp = new HashSet<string>();
        result[node.Text] = lstExp;
        for (int j = 0; j < node.Nodes.Count; j++)
        {
          lstExp.Add(node.Nodes[j].Text);
        }
      }

      return result;
    }

    public string Pattern
    {
      get
      {
        return txtPattern.Text;
      }
      set
      {
        txtPattern.Text = value;
      }
    }

    public void InitializeClassificationSet(List<string> sortedExperimentals, string pattern)
    {
      this.Pattern = pattern;

      InitializeClassificationSet(sortedExperimentals);
    }

    public void InitializeClassificationSet(List<string> sortedExperimentals)
    {
      Regex reg;
      try
      {
        reg = new Regex(this.Pattern);
      }
      catch (Exception)
      {
        MessageBox.Show("Error regex pattern : " + this.Pattern);
        return;
      }

      Func<string, string> f = m =>
      {
        var oldName = OnGetName(m);
        var match = reg.Match(oldName);
        if (match.Success)
        {
          if (match.Groups.Count == 1)
          {
            return match.Groups[0].Value;
          }
          else if (match.Groups.Count == 2)
          {
            return match.Groups[1].Value;
          }
          else
          {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < match.Groups.Count; i++)
            {
              sb.Append(match.Groups[i].Value);
            }
            return sb.ToString();
          }
        }
        else
        {
          return oldName;
        }
      };

      InitializeClassificationSet(sortedExperimentals, f);
    }

    public void InitializeClassificationSet(List<string> sortedExperimentals, Func<string, string> groupFunc)
    {
      var dic = sortedExperimentals.GroupBy(m => groupFunc(m));

      InitializeClassificationSet(dic);
    }

    private Dictionary<string, List<string>> lastDic = null;

    public void InitializeClassificationSet(IEnumerable<IGrouping<string, string>> dic)
    {
      var d = new Dictionary<string, List<string>>();
      foreach (var entry in dic)
      {
        d[entry.Key] = new List<string>(entry);
      }

      InitializeClassificationSet(d);
    }

    public void InitializeClassificationSet(Dictionary<string, List<string>> dic)
    {
      lastDic = dic;

      tvClassifications.BeginUpdate();
      try
      {
        tvClassifications.Nodes.Clear();
        foreach (var key in dic.Keys)
        {
          TreeNode node = tvClassifications.Nodes.Add(key);

          var entry = dic[key];
          foreach (var value in entry)
          {
            node.Nodes.Add(value);
          }
        };
      }
      finally
      {
        tvClassifications.EndUpdate();
      }
    }

    private void btnMerge_Click(object sender, EventArgs e)
    {
      if (tvClassifications.SelectedNodes.Count <= 1)
      {
        return;
      }

      List<TreeNode> selectedNodes = new List<TreeNode>();

      foreach (TreeNode node in tvClassifications.SelectedNodes)
      {
        selectedNodes.Add(node);
      }

      using (InputTextForm form = new InputTextForm(null, null, "Input classification name", "Classification name", selectedNodes[0].Text, false))
      {
        if (form.ShowDialog() == DialogResult.OK)
        {
          List<string> experimentals = new List<string>();
          selectedNodes.ForEach(node =>
          {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
              experimentals.Add(node.Nodes[i].Text);
            }
          });
          experimentals.Sort();

          for (int i = 1; i < selectedNodes.Count; i++)
          {
            tvClassifications.Nodes.Remove(selectedNodes[i]);
          }

          selectedNodes[0].Text = form.Value;
          selectedNodes[0].Nodes.Clear();

          experimentals.ForEach(m => selectedNodes[0].Nodes.Add(m));
        }
      }
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
      if (lastDic != null)
      {
        InitializeClassificationSet(lastDic);
      }
    }

    private void btnRename_Click(object sender, EventArgs e)
    {
      if (tvClassifications.SelectedNode == null)
      {
        return;
      }

      using (InputTextForm form = new InputTextForm(null, null, "Input classification name", "Classification name", tvClassifications.SelectedNode.Text, false))
      {
        if (form.ShowDialog() == DialogResult.OK)
        {
          tvClassifications.BeginUpdate();
          try
          {
            tvClassifications.SelectedNode.Text = form.Value;
          }
          finally
          {
            tvClassifications.EndUpdate();
          }
        }
      }
    }

    private void btnCollapse_Click(object sender, EventArgs e)
    {
      if (tvClassifications.SelectedNodes.Count == 0)
      {
        return;
      }

      List<TreeNode> selectedNodes = new List<TreeNode>();
      foreach (TreeNode node in tvClassifications.SelectedNodes)
      {
        selectedNodes.Add(node);
      }

      tvClassifications.BeginUpdate();
      try
      {
        for (int i = tvClassifications.Nodes.Count - 1; i >= 0; i--)
        {
          if (selectedNodes.Contains(tvClassifications.Nodes[i]) && tvClassifications.Nodes[i].Nodes.Count > 1)
          {
            TreeNode curNode = tvClassifications.Nodes[i];
            for (int j = curNode.Nodes.Count - 1; j >= 0; j--)
            {
              TreeNode subNode = curNode.Nodes[j];
              TreeNode newNode = new TreeNode(subNode.Text);
              curNode.Nodes.Remove(subNode);
              newNode.Nodes.Add(subNode);
              tvClassifications.Nodes.Insert(i, newNode);
            }
            tvClassifications.Nodes.Remove(curNode);
          }
        }
      }
      finally
      {
        tvClassifications.EndUpdate();
      }
    }

    private void tvClassifications_BeforeSelect(object sender, TreeViewCancelEventArgs e)
    {
      if (e.Node.Parent != null)
      {
        e.Cancel = true;
      }
    }

    #region IRcpaComponent Members

    public void ValidateComponent()
    {
      if (tvClassifications.Nodes.Count == 0)
      {
        throw new Exception("Load data to classification panel first!");
      }
    }

    #endregion

    #region IOptionFile Members

    public void RemoveFromXml(System.Xml.Linq.XElement option)
    {
      option.RemoveChild(this.Name);
    }

    public void LoadFromXml(System.Xml.Linq.XElement option)
    {
      string pattern = string.Empty;
      var dic = new Dictionary<string, List<string>>();
      dic.LoadFromXml(option, this.Name, ref pattern);

      if (dic.Count == 0)
      {
        tvClassifications.Nodes.Clear();
        return;
      }

      this.Pattern = pattern;
      InitializeClassificationSet(dic);
    }

    public void SaveToXml(System.Xml.Linq.XElement option)
    {
      var dic = GetClassificationSet();

      dic.SaveToXml(option, this.Name, this.Pattern);
    }

    #endregion

    private void btnUp_Click(object sender, EventArgs e)
    {
      if (tvClassifications.SelectedNodes.Count == 0)
      {
        return;
      }

      List<TreeNode> selectedNodes = new List<TreeNode>();
      foreach (TreeNode node in tvClassifications.SelectedNodes)
      {
        selectedNodes.Add(node);
      }

      tvClassifications.BeginUpdate();
      try
      {
        for (int i = 1; i < tvClassifications.Nodes.Count; i++)
        {
          if (selectedNodes.Contains(tvClassifications.Nodes[i]) && !selectedNodes.Contains(tvClassifications.Nodes[i - 1]))
          {
            TreeNode curNode = tvClassifications.Nodes[i];
            tvClassifications.Nodes.Remove(curNode);
            tvClassifications.Nodes.Insert(i - 1, curNode);
          }
        }

        ArrayList coll = new ArrayList();
        selectedNodes.ForEach(m => coll.Add(m));
        tvClassifications.SelectedNode = selectedNodes[0];
        tvClassifications.SelectedNodes = coll;
      }
      finally
      {
        tvClassifications.EndUpdate();
      }
    }

    private void btnClassify_Click(object sender, EventArgs e)
    {
      OnGetData(new EventArgs());

      if (lastDic != null)
      {
        var exps = (from v in lastDic.Values
                    from k in v
                    select k).ToList();
        exps.Sort();

        InitializeClassificationSet(exps);
      }
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      if (tvClassifications.SelectedNodes.Count == 0)
      {
        return;
      }

      List<TreeNode> selectedNodes = new List<TreeNode>();
      foreach (TreeNode node in tvClassifications.SelectedNodes)
      {
        selectedNodes.Add(node);
      }

      tvClassifications.BeginUpdate();
      try
      {
        for (int i = tvClassifications.Nodes.Count - 2; i >= 0; i--)
        {
          if (selectedNodes.Contains(tvClassifications.Nodes[i]) && !selectedNodes.Contains(tvClassifications.Nodes[i + 1]))
          {
            TreeNode curNode = tvClassifications.Nodes[i];
            tvClassifications.Nodes.Remove(curNode);
            tvClassifications.Nodes.Insert(i + 1, curNode);
          }
        }

        ArrayList coll = new ArrayList();
        selectedNodes.ForEach(m => coll.Add(m));
        tvClassifications.SelectedNode = selectedNodes[0];
        tvClassifications.SelectedNodes = coll;
      }
      finally
      {
        tvClassifications.EndUpdate();
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (saveFile.GetFileDialog().ShowDialog() == DialogResult.OK)
      {
        XElement root = new XElement("Root");
        this.SaveToXml(root);
        root.Save(saveFile.GetFileDialog().FileName);
      }
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      if (openFile.GetFileDialog().ShowDialog() == DialogResult.OK)
      {
        XElement root = XElement.Load(openFile.GetFileDialog().FileName);
        this.LoadFromXml(root);
      }
    }
  }

  public static class DictionaryXmlExtension
  {
    public static void LoadFromXml(this Dictionary<string, List<string>> dic, System.Xml.Linq.XElement option, string nodeName)
    {
      string pattern = string.Empty;

      LoadFromXml(dic, option, nodeName, ref pattern);
    }

    public static void LoadFromXml(this Dictionary<string, List<string>> dic, System.Xml.Linq.XElement option, string nodeName, ref string pattern)
    {
      dic.Clear();

      if (option.Element(nodeName) == null && option.Element("ClassificationSet") != null)
      {
        nodeName = "ClassificationSet";
      }

      var result =
        (from item in option.Descendants(nodeName)
         select item).FirstOrDefault();

      if (null == result)
      {
        return;
      }

      if (result.Element("Pattern") == null)
      {
        pattern = "(.*)";
      }
      else
      {
        pattern = result.Element("Pattern").Value;
      }

      if (result.Element("ClassificationItem") == null)
      {
        foreach (var node in result.Descendants("Set"))
        {
          var lst = new List<string>();
          dic[node.Attribute("Key").Value] = lst;
          foreach (var subNode in node.Descendants("Value"))
          {
            lst.Add(subNode.Value);
          }
        }
      }
      else
      {
        foreach (var node in result.Descendants("ClassificationItem"))
        {
          var lst = new List<string>();
          dic[node.Element("classifiedName").Value] = lst;
          foreach (var subNode in node.Descendants("experimentName"))
          {
            lst.Add(subNode.Value);
          }
        }
      }
    }

    public static void SaveToXml(this Dictionary<string, List<string>> dic, System.Xml.Linq.XElement option, string nodeName, string pattern)
    {
      option.RemoveChild(nodeName);

      option.Add(new XElement(nodeName,
        string.IsNullOrEmpty(pattern) ? null : new XElement("Pattern", pattern),
        from d in dic
        select new XElement("Set",
          new XAttribute("Key", d.Key),
          from e in d.Value
          select new XElement("Value", e))));
    }
  }
}
