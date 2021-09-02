Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Support.UI
Imports System.IO
Imports System.Text
Imports System.Threading
Public Class Scraper_Template

    'This class is the Selenium base method of doing the screen scraping via Chrome - placed into service 7/1/17

    Private driver As IWebDriver
    Private handleTopWindow As String
    Private maxWaitSecs_PageLoads As Integer
    Private maxWaitSecs_Downloads As Integer

    Private mURL As String = "http://www.theladders.com/jobs/search-jobs"

    Dim su As New Serialization_Utilities.ThreadSafe
    Dim startPage As Integer = 1
    Dim rnd As New System.Random(27)

    Public Enum FindBy

        unspecified
        by_class
        by_css
        by_id
        by_link
        by_name
        by_partiallink
        by_tag
        by_xpath

    End Enum

    Public Function Scrape(ByVal title As String,
                           Optional ByVal maxDepth As Integer = 2) _
                           As SortedDictionary(Of String, Tuple(Of Integer, String))


        Initialize()

        driver.Navigate().GoToUrl(mURL)

        Dim results As SortedDictionary(Of String, Tuple(Of Integer, String))
        If Not Extract_Data(title, results, maxDepth) Then
            Debug.Print("Failed to scrape results")
        End If

        Shut_Down_Chrome_Driver()

        Return results

    End Function

    Private Sub Navigate_To_Starting_URL(ByVal title As String, Optional ByVal pageNum As String = "")

        Dim url As String = String.Format(mURL, title)
        'If Integer.TryParse(pageNum, 0) Then url = String.Format(mURL_page_template, title, pageNum)

        driver.Navigate().GoToUrl(url)

    End Sub


    Private Function Extract_Data(ByVal title As String,
                                  ByRef results As SortedDictionary(Of String, Tuple(Of Integer, String)),
                                  Optional ByVal maxDepth As Integer = 2) _
                                  As Boolean

        Dim success As Boolean = False
        results = New SortedDictionary(Of String, Tuple(Of Integer, String))
        results.Add(title, New Tuple(Of Integer, String)(1, mURL))
        Debug.Print(1 & vbTab & title & vbTab & Now.ToString)

        Try

            Dim keywords = driver.FindElement(By.ClassName("keyword-input"))
            keywords.Clear()
            keywords.SendKeys(title)

            Dim submit = driver.FindElement(By.ClassName("search-submit-button"))
            If submit IsNot Nothing Then submit.Click()

            System.Threading.Thread.Sleep(1500)

            If driver.Url = mURL Then Return False

            Extract_Related_Titles(1, results, maxDepth)

            success = True

        Catch ex As Exception

            Debug.Print("Scraping error: " & ex.Message)

        End Try

        Return success

    End Function

    Private Sub Extract_Related_Titles(ByVal depth As Integer,
                                       ByRef results As SortedDictionary(Of String, Tuple(Of Integer, String)),
                                       Optional ByVal maxDepth As Integer = 2)

        If depth > maxDepth Then Exit Sub
        If results Is Nothing Then results = New SortedDictionary(Of String, Tuple(Of Integer, String))
        Thread.Sleep(rnd.Next(973, 1420))

        Dim similars = driver.FindElement(By.ClassName("simialr-titles-row"))
        If similars Is Nothing Then Exit Sub

        Dim url As String = driver.Url
        Dim titles As New SortedDictionary(Of String, String)
        For Each elem As IWebElement In similars.FindElements(By.TagName("a"))

            Try

                Dim simURL As String = elem.GetAttribute("href")
                Dim simElem = elem.FindElement(By.ClassName("white-linked-tag"))
                If simElem IsNot Nothing AndAlso simURL IsNot Nothing Then

                    Dim simTitle As String = simElem.Text

                    If Not results.ContainsKey(simTitle) Then

                        Debug.Print(depth & vbTab & simTitle & vbTab & Now.ToString)
                        results.Add(simTitle, New Tuple(Of Integer, String)(depth, simURL))
                        titles.Add(simTitle, simURL)

                    End If

                End If

            Catch ex As Exception
                Debug.Print("Scraping error: " & ex.Message)
            End Try

        Next

        depth += 1

        For Each simURL In titles.Values

            Try

                driver.Navigate().GoToUrl(simURL)

                Extract_Related_Titles(depth, results, maxDepth)

                driver.Navigate().GoToUrl(url)
                Thread.Sleep(rnd.Next(973, 1420))

            Catch ex As Exception
                Debug.Print("Page navigation error: " & ex.Message)
            End Try

        Next

    End Sub


#Region " Chrome Driver Initialization / Shut Down "

    Private Sub Initialize()

        maxWaitSecs_Downloads = 20
        maxWaitSecs_PageLoads = 30

        driver = New ChromeDriver()

        'driver.Manage.Timeouts.ImplicitlyWait(New TimeSpan(0, 0, 10))
        'driver.Manage.Timeouts.SetPageLoadTimeout(New TimeSpan(0, 3, 0))
        'driver.Manage.Timeouts.SetScriptTimeout(New TimeSpan(0, 3, 0))

        'No built in way to minimize browser while scraping - just move off into oblivion as workaround
        'driver.Manage.Window.Position = New System.Drawing.Point(-2000, -2000)

    End Sub

    Private Function Initialize_Profile() As ChromeOptions

        Dim options As New ChromeOptions()

        'options.AddUserProfilePreference("download.default_directory", dir)
        'options.AddUserProfilePreference("download.prompt_for_download", "false")

        'profile.SetPreference("browser.download.folderList", 2)
        'profile.SetPrefere("browser.download.dir", My.Settings.dirScrapeReports)

        'profile.SetPreference("browser.download.manager.alertOnEXEOpen", False)
        'profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "text/comma-separated-values, text/csv, application/csv, application/excel, application/vnd.ms-excel, application/vnd.msexcel, text/anytext")
        'profile.SetPreference("browser.download.manager.focusWhenStarting", False)
        'profile.SetPreference("browser.download.useDownloadDir", True)
        'profile.SetPreference("browser.helperApps.alwaysAsk.force", False)
        'profile.SetPreference("browser.download.manager.alertOnEXEOpen", False)
        'profile.SetPreference("browser.download.manager.closeWhenDone", True)
        'profile.SetPreference("browser.download.manager.showAlertOnComplete", False)
        'profile.SetPreference("browser.download.manager.useWindow", False)
        'profile.SetPreference("services.sync.prefs.sync.browser.download.manager.showWhenStarting", False)
        'profile.SetPreference("pdfjs.disabled", True)

        Return options

    End Function

    Private Sub Save_Interim_Results(ByVal groupName As String, ByVal pageNum As Integer, ByRef results As SortedDictionary(Of Integer, SortedDictionary(Of Integer, String)))

        Dim sb As New System.Text.StringBuilder
        Dim filename As String = String.Format("C:\WIP\ladders\titlesLadders_{0}Scraped_partial.txt", groupName)

        Dim success As Boolean
        Dim su As New Serialization_Utilities.ThreadSafe
        su.Serialize_Object_And_Save_FileSystem(results, filename, success)

    End Sub

    Private Sub Shut_Down_Chrome_Driver()

        driver.Close()
        driver.Quit()
        driver = Nothing

    End Sub

#End Region


#Region " Utilities "

    Private Sub Click_Button(ByRef driver As IWebDriver,
                             ByVal selector As String,
                             Optional ByVal findMethod As FindBy = FindBy.by_id)

        Wait_Until_Elem_Present(maxWaitSecs_PageLoads, driver, selector, findMethod)
        Find_Element(driver, selector, findMethod).Click()

    End Sub

    Private Sub Fill_Textbox(ByRef driver As IWebDriver,
                             ByVal selector As String,
                             ByVal value As String,
                             Optional ByVal findMethod As FindBy = FindBy.by_id)

        Wait_Until_Elem_Present(maxWaitSecs_PageLoads, driver, selector, findMethod)
        With Find_Element(driver, selector, findMethod)
            .Clear()
            .SendKeys(value)
        End With

    End Sub

    Private Sub Wait_Until_Elem_Present(ByVal maxSeconds As Integer,
                                        ByRef driver As IWebDriver,
                                        ByVal selector As String,
                                        Optional ByVal findMethod As FindBy = FindBy.by_id)

        For second As Integer = 0 To Integer.MaxValue

            If second > maxSeconds Then Exit For

            Try

                Dim elem As IWebElement = Find_Element(driver, selector, findMethod)
                If elem IsNot Nothing Then Exit For

            Catch ex As Exception
                Thread.Sleep(1000)
            End Try

        Next

    End Sub

    Private Function Find_Element(ByRef driver As IWebDriver,
                                  ByVal selector As String,
                                  Optional findMethod As FindBy = FindBy.by_id) _
                                  As IWebElement

        Dim elem As IWebElement

        Try

            Select Case findMethod

                Case FindBy.by_class
                    elem = driver.FindElement(By.ClassName(selector))

                Case FindBy.by_css
                    elem = driver.FindElement(By.CssSelector(selector))

                Case FindBy.by_id
                    elem = driver.FindElement(By.Id(selector))

                Case FindBy.by_link
                    elem = driver.FindElement(By.LinkText(selector))

                Case FindBy.by_name
                    elem = driver.FindElement(By.Name(selector))

                Case FindBy.by_partiallink
                    elem = driver.FindElement(By.PartialLinkText(selector))

                Case FindBy.by_tag
                    elem = driver.FindElement(By.TagName(selector))

                Case FindBy.by_xpath
                    elem = driver.FindElement(By.XPath(selector))

                Case Else
                    elem = driver.FindElement(By.Id(selector))

            End Select

        Catch ex As OpenQA.Selenium.WebDriverException
            'Debug.Print("failed to find element {0} because {1}", selector, ex.Message)
        End Try

        Return elem

    End Function


#End Region

#Region " Old Code "

    'Private Sub Login()

    '    driver.Navigate().GoToUrl("http://www.google.com")

    '    Wait_Until_Elem_Present(maxWaitSecs_PageLoads, driver, "Email")
    '    driver.FindElement(By.Id("Email")).Clear()
    '    'driver.FindElement(By.Id("Email")).SendKeys(My.Settings.userTP)

    '    driver.FindElement(By.Id("Password")).Clear()
    '    'driver.FindElement(By.Id("Password")).SendKeys(My.Settings.pwTP)

    '    Dim dblQte As String = ChrW(34)
    '    Dim ident As String = String.Format("input[type={0}submit{1}]", dblQte, dblQte)
    '    driver.FindElement(By.CssSelector(ident)).Click()

    'End Sub

    'Private Sub Wait_Until_File_Download(ByVal intialCount As Integer, maxWaitSecs As Integer)

    '    Dim nowCount As Integer = intialCount
    '    Dim secondsWaited As Integer = 0
    '    While nowCount = intialCount
    '        Thread.Sleep(100)
    '        secondsWaited += 1
    '        If secondsWaited = maxWaitSecs Then Exit While
    '        nowCount = How_Many_Downloads_Exist()
    '    End While

    'End Sub

    'Private Function How_Many_Downloads_Exist() As Integer

    '    Dim count As Integer = 0
    '    'For Each file In My.Computer.FileSystem.GetFiles(My.Settings.dirScrapeReports)

    '    '    If file.EndsWith(".csv") Then count += 1

    '    'Next

    '    Return count

    'End Function

#End Region

End Class

