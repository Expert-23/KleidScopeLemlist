Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Support.UI
Imports System.IO
Imports System.Text
Imports System.Threading
Public Class Scraper_QB

    'This class is the Selenium base method of doing the screen scraping via Chrome - placed into service 7/1/17

    Private mDriver As IWebDriver
    Private mPanes As ObjectModel.ReadOnlyCollection(Of IWebElement)
    Private mDirChrome As String = "C:\Users\pc\Desktop\Software and Files\Expert 23\Google_For_Selenium_78.0.3904.7000\Chrome\Application\chrome.exe"
    Private mdirVPN As String = "C:\Users\pc\Desktop\Software and Files\Expert 23\Google_For_Selenium_78.0.3904.7000\Chrome\Application\2.0.0_0.crx"
    Private handleTopWindow As String
    Private maxWaitSecs_PageLoads As Integer
    Private maxWaitSecs_Downloads As Integer

    Private mURL As String = "http://quillbot.com"
    Private mRandom As New System.Random(108)

    Private mContent As String
    Private mSentences As SortedDictionary(Of Integer, String)

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

    Private Sub Navigate_To_Starting_URL(ByVal title As String, Optional ByVal pageNum As String = "")

        Dim url As String = String.Format(mURL, title)
        'If Integer.TryParse(pageNum, 0) Then url = String.Format(mURL_page_template, title, pageNum)

        mDriver.Navigate().GoToUrl(url)

    End Sub


    Public Sub Scrape(ByRef sentences As SortedDictionary(Of Integer, Sentence),
                      Optional ByVal numVariants_subject As Integer = 10,
                      Optional ByVal numVariants_body As Integer = 3)



        Initialize()
        Utilities.Rotate_VPN(1, True, mDriver)
        Dim variations As New SortedDictionary(Of Integer, Sentence)

        For Each entry In sentences

            Dim number As Integer = entry.Key
            Dim sentnce As Sentence = entry.Value

            Dim numVariants As Integer = numVariants_subject
            If number > 0 Then numVariants = numVariants_body

            If Not String.IsNullOrWhiteSpace(sentnce.ToString) Then

                If Not Extract_Variations_For_Sentence(sentnce, numVariants) Then
                    'MA
                    Dim originalval As String = entry.Value.Variations(0)
                    Dim sorted As New SortedDictionary(Of Integer, String)
                    sorted.Add(0, originalval)
                    sentnce.Variations = sorted
                    If Not Extract_Variations_For_Sentence(sentnce, numVariants) Then
                        Debug.Print("Failed to scrape results for sentence " & number)
                    End If
                    'MA
                End If

            End If

            variations.Add(number, sentnce)

        Next

        sentences = variations

        Shut_Down_Chrome_Driver()

    End Sub


    Private Function Extract_Variations_For_Sentence(ByRef sentnce As Sentence,
                                                      ByVal numberVariations As Integer) _
                                                      As Boolean

        Dim success As Boolean = False

        mDriver.Navigate().GoToUrl(mURL)
        System.Threading.Thread.Sleep(1000)

        Try

            With sentnce

                mPanes = mDriver.FindElements(By.ClassName("Pane"))
                System.Threading.Thread.Sleep(mRandom.Next(880, 1168))

                Set_Text_To_Be_Varied(.Original)

                Get_Variations(sentnce, numberVariations)

                success = True

            End With

        Catch ex As Exception

            Debug.Print("Scraping error: " & ex.Message)

        End Try

        Return success

    End Function

    Private Sub Set_Text_To_Be_Varied(ByVal text As String)

        Dim enterText = mDriver.FindElement(By.Id("inputText"))

        enterText.Clear()
        enterText.SendKeys(text)

    End Sub

    Private Sub Get_Variations(ByRef sentnce As Sentence, ByVal numberVariations As Integer)

        With sentnce

            For i = 1 To numberVariations

                System.Threading.Thread.Sleep(mRandom.Next(1580, 1704))
                Dim variation As String = Get_Variation()

                .Variations.Add(.Variations.Count, variation)

            Next

        End With

    End Sub

    Private Function Get_Variation() As String



        Dim divs = mPanes(0).FindElements(By.ClassName("MuiGrid-root"))
        If divs IsNot Nothing AndAlso divs.Count > 10 Then
            'MA
            'divs(10).Click()
            mDriver.FindElement(By.CssSelector(".MuiButtonBase-root.MuiButton-root.MuiButton-contained.QuillButton-sc-12j9igu-0.dkavuo.quillArticleBtn.jss180.MuiButton-containedPrimary")).SendKeys(Keys.Return)
            Thread.Sleep(10000)
        End If

        System.Threading.Thread.Sleep(mRandom.Next(1580, 1704))
        Dim outbound = mPanes(1).FindElement(By.Id("articleTextArea"))

        Dim variation As String = mPanes(1).Text
        If outbound IsNot Nothing Then variation = outbound.Text

        Return variation

    End Function



#Region " Chrome Driver Initialization / Shut Down "

    Private Sub Initialize()

        maxWaitSecs_Downloads = 20
        maxWaitSecs_PageLoads = 30

        Dim options As New ChromeOptions

        options.AddExtension(mdirVPN)
        options.BinaryLocation = mDirChrome

        mDriver = New ChromeDriver(options)

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
        'Dim filename As String = String.Format("C:\WIP\Scraped_partial.txt", groupName)

        'Dim success As Boolean
        'Dim su As New Serialization_Utilities.ThreadSafe
        'su.Serialize_Object_And_Save_FileSystem(results, filename, success)

    End Sub

    Private Sub Shut_Down_Chrome_Driver()

        mDriver.Close()
        mDriver.Quit()
        mDriver = Nothing

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

    'Public Function Scrape(ByVal sentences As SortedDictionary(Of Integer, String),
    '                       Optional ByVal maxVariants As Integer = 3) _
    '                       As SortedDictionary(Of Integer, SortedDictionary(Of Integer, String))


    '    Initialize()

    '    'driver.Navigate().GoToUrl(mURL)

    '    Dim variations As New SortedDictionary(Of Integer, SortedDictionary(Of Integer, String))

    '    For Each entry In sentences

    '        Dim content As String = entry.Value
    '        Dim sentenceNumber As Integer = entry.Key
    '        Dim variants As Integer = 10
    '        If sentenceNumber > 0 Then variants = maxVariants

    '        If Not String.IsNullOrWhiteSpace(content) Then

    '            Dim results As SortedDictionary(Of Integer, String)
    '            If Not Extract_Data(content, results, variants) Then
    '                Debug.Print("Failed to scrape results for sentence " & sentenceNumber)
    '                variations.Add(sentenceNumber, New SortedDictionary(Of Integer, String))
    '            Else
    '                variations.Add(sentenceNumber, results)
    '            End If

    '        End If

    '    Next

    '    Shut_Down_Chrome_Driver()

    '    Return variations

    'End Function

    'Private Function Extract_Data(ByVal content As String,
    '                              ByRef results As SortedDictionary(Of Integer, String),
    '                              Optional ByVal maxDepth As Integer = 3) _
    '                              As Boolean

    '    Dim success As Boolean = False
    '    results = New SortedDictionary(Of Integer, String)

    '    My.Computer.Clipboard.SetText(content)

    '    'Debug.Print(1 & vbTab & title & vbTab & Now.ToString)

    '    mDriver.Navigate().GoToUrl(mURL)
    '    System.Threading.Thread.Sleep(1000)

    '    'Dim alertCancel As IAlert = driver.SwitchTo().Alert()
    '    'alertCancel.Dismiss()

    '    Try

    '        Dim panes = mDriver.FindElements(By.ClassName("Pane"))

    '        System.Threading.Thread.Sleep(mRandom.Next(880, 1168))

    '        Dim enterText = mDriver.FindElement(By.Id("inputText"))

    '        enterText.Clear()
    '        enterText.SendKeys(content)

    '        'Dim divs = panes(0).FindElements(By.ClassName("MuiGrid-root"))

    '        For i = 1 To maxDepth

    '            System.Threading.Thread.Sleep(mRandom.Next(1580, 1704))

    '            'Dim submit = driver.FindElements(By.ClassName("false"))
    '            'If submit IsNot Nothing Then submit(0).Click()
    '            Dim divs = panes(0).FindElements(By.ClassName("MuiGrid-root"))
    '            If divs IsNot Nothing AndAlso divs.Count > 10 Then
    '                divs(10).Click()
    '            End If

    '            System.Threading.Thread.Sleep(mRandom.Next(1580, 1704))

    '            Dim outbound = panes(1).FindElement(By.Id("articleTextArea"))

    '            Dim result As String = panes(1).Text
    '            If outbound IsNot Nothing Then result = outbound.Text

    '            results.Add(results.Count, result)

    '        Next

    '        success = True

    '    Catch ex As Exception

    '        Debug.Print("Scraping error: " & ex.Message)

    '    End Try

    '    Return success

    'End Function



    'Private Sub Extract_Related_Titles(ByVal depth As Integer,
    '                                   ByRef results As SortedDictionary(Of String, Tuple(Of Integer, String)),
    '                                   Optional ByVal maxDepth As Integer = 2)

    '    If depth > maxDepth Then Exit Sub
    '    If results Is Nothing Then results = New SortedDictionary(Of String, Tuple(Of Integer, String))
    '    Thread.Sleep(rnd.Next(973, 1420))

    '    Dim similars = driver.FindElement(By.ClassName("simialr-titles-row"))
    '    If similars Is Nothing Then Exit Sub

    '    Dim url As String = driver.Url
    '    Dim titles As New SortedDictionary(Of String, String)
    '    For Each elem As IWebElement In similars.FindElements(By.TagName("a"))

    '        Try

    '            Dim simURL As String = elem.GetAttribute("href")
    '            Dim simElem = elem.FindElement(By.ClassName("white-linked-tag"))
    '            If simElem IsNot Nothing AndAlso simURL IsNot Nothing Then

    '                Dim simTitle As String = simElem.Text

    '                If Not results.ContainsKey(simTitle) Then

    '                    Debug.Print(depth & vbTab & simTitle & vbTab & Now.ToString)
    '                    results.Add(simTitle, New Tuple(Of Integer, String)(depth, simURL))
    '                    titles.Add(simTitle, simURL)

    '                End If

    '            End If

    '        Catch ex As Exception
    '            Debug.Print("Scraping error: " & ex.Message)
    '        End Try

    '    Next

    '    depth += 1

    '    For Each simURL In titles.Values

    '        Try

    '            driver.Navigate().GoToUrl(simURL)

    '            Extract_Related_Titles(depth, results, maxDepth)

    '            driver.Navigate().GoToUrl(url)
    '            Thread.Sleep(rnd.Next(973, 1420))

    '        Catch ex As Exception
    '            Debug.Print("Page navigation error: " & ex.Message)
    '        End Try

    '    Next

    'End Sub


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

