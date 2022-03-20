using System;
using System.Windows.Forms;

namespace RCPA.Tools
{
  public partial class AboutForm : Form
  {
    public AboutForm()
    {
      InitializeComponent();
    }

    private void linkHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start(linkHomepage.Text);
    }

    private void linkEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("mailto: " + linkEmail.Text);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
