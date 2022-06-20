Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.HtmlEditControl1.CSSText = "body {font-family: courier new} span.mention {color: green; font-weight: bold;} span.mention:before {content: '@'; } span.issuelink {color: blue; font-weight: bold;} span.issuelink:before {content: '#'; }"

        Dim oCheckButton = Me.HtmlEditControl1.ToolStripItems.Add("Insert Check List")
        oCheckButton.Padding = New Padding(3)
        AddHandler oCheckButton.Click, AddressOf oCheckButton_Click

        Dim oAddMentionButton = Me.HtmlEditControl1.ToolStripItems.Add("Mention User")
        oAddMentionButton.Padding = New Padding(3)
        AddHandler oAddMentionButton.Click, AddressOf oAddMentionButton_Click

        Dim oIssueLinkButton = Me.HtmlEditControl1.ToolStripItems.Add("Link to Issue")
        oIssueLinkButton.Padding = New Padding(3)
        AddHandler oIssueLinkButton.Click, AddressOf oIssueLinkButton_Click

        Dim oGetAddedElements = Me.HtmlEditControl1.ToolStripItems.Add("Added Elements Parse")
        oGetAddedElements.Padding = New Padding(3)
        AddHandler oGetAddedElements.Click, AddressOf oGetAddedElements_Click

    End Sub

    Private Sub oGetAddedElements_Click(sender As Object, e As EventArgs)

        Dim s As New System.Text.StringBuilder

        For Each oElement In Me.HtmlEditControl1.GetItemsByAttributeValue("data-type", "Markdown")

            If oElement.TagName.ToLower <> "ul" Then
                s.AppendLine(oElement.GetAttribute("Classname") & " " & oElement.InnerHtml)
            Else
                For Each oSubElement As HtmlElement In oElement.GetElementsByTagName("li")
                    s.AppendLine("Task List Item" & oSubElement.InnerText)
                Next
            End If

        Next

        MsgBox(s.ToString())

    End Sub

    Private Sub oIssueLinkButton_Click(sender As Object, e As EventArgs)

        Dim oMention = Me.HtmlEditControl1.InsertHTMLELement("span")
        oMention.SetAttribute("ClassName", "issuelink")
        oMention.InnerHtml = "IssueLink"
        oMention.SetAttribute("data-type", "Markdown")

        Me.HtmlEditControl1.MoveCursorToElement(oMention, Zoople.HTMLEditControl._ELEM_ADJACENCY.ELEM_ADJ_AfterEnd)
        Me.HtmlEditControl1.InsertAtCursor("&nbsp;", Zoople.HTMLEditControl.ed_InsertType.ed_InsertAfterSelection)
        ' ^^^ necessary to prevent continuation of span tag if the user hits enter - on TODO list for build 298

        Me.HtmlEditControl1.SetDirty()

    End Sub

    Private Sub oAddMentionButton_Click(sender As Object, e As EventArgs)

        Dim oMention = Me.HtmlEditControl1.InsertHTMLELement("span")
        oMention.SetAttribute("ClassName", "mention")
        oMention.InnerHtml = "User"
        oMention.SetAttribute("data-type", "Markdown")

        Me.HtmlEditControl1.MoveCursorToElement(oMention, Zoople.HTMLEditControl._ELEM_ADJACENCY.ELEM_ADJ_AfterEnd)
        Me.HtmlEditControl1.InsertAtCursor("&nbsp;", Zoople.HTMLEditControl.ed_InsertType.ed_InsertAfterSelection)
        ' ^^^ necessary to prevent continuation of span tag if the user hits enter - on TODO list for build 298

        Me.HtmlEditControl1.SetDirty()

    End Sub

    Private Sub oCheckButton_Click(sender As Object, e As EventArgs)

        Dim oListParent = Me.HtmlEditControl1.InsertHTMLELement("ul")
        oListParent.SetAttribute("data-type", "Markdown")

        oListParent.InsertAdjacentElement(HtmlElementInsertionOrientation.AfterBegin, Me.HtmlEditControl1.Document.CreateElement("li")).InnerHtml = "<input type=""checkbox"" disabled="""" checked=""""> Test3"
        oListParent.InsertAdjacentElement(HtmlElementInsertionOrientation.AfterBegin, Me.HtmlEditControl1.Document.CreateElement("li")).InnerHtml = "<input type=""checkbox"" disabled=""""> Test2"
        oListParent.InsertAdjacentElement(HtmlElementInsertionOrientation.AfterBegin, Me.HtmlEditControl1.Document.CreateElement("li")).InnerHtml = "<input type=""checkbox"" disabled="""" checked=""""> Test1"

        Me.HtmlEditControl1.MoveCursorToElement(oListParent, Zoople.HTMLEditControl._ELEM_ADJACENCY.ELEM_ADJ_AfterEnd)
        Me.HtmlEditControl1.SetDirty()

    End Sub

End Class
