<p style="margin-bottom: 12.0pt; line-height: normal;"><span style="text-decoration: underline;"><strong><span style="font-size: 12pt; font-family: 'Segoe UI', sans-serif; color: #24292e; text-decoration: underline;">QUADTREE İLE MOUSE TIKLAMALARININ MODELLENMESİ VE DAİRESEL ARALIK SORGULAMA</span></strong></span></p>
<p style="margin-bottom: 12.0pt; line-height: normal;"><strong><span style="font-size: 12.0pt; font-family: 'Segoe UI',sans-serif; color: #24292e;">&Ouml;zet:</span></strong></p>
<p style="margin-bottom: 12.0pt; line-height: normal;"><span style="font-size: 10pt; font-family: 'times new roman', times; color: #24292e;">Projede 512 x 512 boyutlarında olan Windows Form Uygulamasında, ekranın QuadTree&rsquo; ye g&ouml;re modellenmesi ve ekranın farklı b&ouml;l&uuml;mlerinde yapılan sol tuş tıklamalarında quad tree modelinde uygun node&rsquo;una veri eklenmesi hedeflenmektedir. Ayrıca eklenen bu veri i&ccedil;in ekranda ilgili alanda &ccedil;ember &ccedil;izilecek ve bulunduğu node&rsquo;un sınırları da dikd&ouml;rtgen &ccedil;izimi ile g&ouml;sterilecektir.&nbsp;&nbsp; </span></p>
<p style="margin-bottom: 12.0pt; line-height: normal;"><span style="font-size: 10pt; font-family: 'times new roman', times; color: #24292e;">Uygulama ekranı &uuml;zerinde sağ tuşa basılıyken yapılan alan se&ccedil;imi ile quad tree &uuml;zerinde search yapılacak ve bulunan veriler kırmızı &ccedil;ember &ccedil;izilerek kullanıcıya g&ouml;sterilecektir.</span></p>
<p style="margin-bottom: 12.0pt; line-height: normal;"><strong><span style="font-size: 12.0pt; font-family: 'Segoe UI',sans-serif; color: #24292e;">Genel Yapı:</span></strong></p>
<p><span style="font-size: 10pt; line-height: 107%; font-family: 'times new roman', times; color: #24292e;">Problemin &ccedil;&ouml;z&uuml;m&uuml; C# programlama dili aracılığı ile .net Framework kullanılarak WinForm uygulaması geliştirilmiştir. Program ilk &ccedil;alışmaya başladığında 512 x 512 şeklinde bir uygulama a&ccedil;ar.</span></p>
<p style="padding-left: 40px;"><span style="font-size: 10pt; font-family: 'times new roman', times;">Ayrıca a&ccedil;ılan WinForm&rsquo;un i&ccedil;erisindeki ClientRectangle i&ccedil;in QuadTree nesnesi oluşturulur ve bu Tree&rsquo;nin root node&rsquo;nun sınırları bu rectangledır.&nbsp;</span></p>
<table style="border-collapse: collapse; width: 100%;" border="1">
<tbody>
<tr>
<td style="width: 100%;">
<p style="margin-left: 35.4pt;">///&lt;summary&gt;</p>
<p style="margin-left: 35.4pt;">///<span style="color: #70ad47;">Ekran y&uuml;klendikten sonra &ccedil;ağrılacak olan method</span></p>
<p style="margin-left: 35.4pt;">///&lt;/summary&gt;</p>
<p style="margin-left: 35.4pt;"><span style="color: #4472c4;">Private void</span> FrmMain_Load(<span style="color: #4472c4;">object </span>sender, <span style="color: #5b9bd5;">EventArgs </span>e)</p>
<p style="margin-left: 35.4pt;">{</p>
<p style="margin-left: 35.4pt;"><span style="color: #70ad47;">//Ekran y&uuml;klendikten sonra sayfa i&ccedil;erisindeki alan i&ccedil;in quadTree oluştulur.</span></p>
<p style="margin-left: 35.4pt;">quadTree = <span style="color: #4472c4;">new </span><span style="color: #5b9bd5;">QuadTree</span>(ClientRectangle);</p>
<p style="margin-left: 35.4pt;">}</p>
</td>
</tr>
</tbody>
</table>
<p><span style="font-size: 10pt; font-family: 'times new roman', times;">Form &uuml;zerindeki kullanıcı ile etkileşimi yakalamak amacı eventler kullanılmıştır;</span></p>
<table style="border-collapse: collapse; width: 100%;" border="1">
<tbody>
<tr>
<td style="width: 100%;">
<p>///&lt;summary&gt;</p>
<p>///<span style="color: #70ad47;">Ekranın herhangi bir alanında fare buttonu serbest bırakıldığında &ccedil;ağrılacak method</span></p>
<p>///&lt;/summary&gt;</p>
<p><span style="color: #4472c4;">Private void</span> FrmMain_MouseUp(<span style="color: #4472c4;">object </span>sender, <span style="color: #5b9bd5;">MouseEventArgs </span>e)&hellip;</p>
</td>
</tr>
</tbody>
</table>
<ul style="list-style-type: disc;">
<li><span style="font-size: 10pt; line-height: 107%; font-family: 'times new roman', times;">Fare buttonu kullanıcı tarafından serbest bırakıldığında;</span><span style="font-size: 10pt; font-family: 'times new roman', times;">-&gt; Sol tuş ise; tıklanan b&ouml;lgenin koordinatlarına g&ouml;re QuadTree&rsquo;deki en uygun node&nbsp; item eklenir</span><span style="font-size: 10pt; font-family: 'times new roman', times;">-&gt;<span style="line-height: 107%;">Sağ tuş ise; se&ccedil;im yapılan b&ouml;lgenin sınırları &ccedil;ıkarılır(Rectangle olarak) ve QuadTree &uuml;zerinde bu sınırlara g&ouml;re arama yapılır.</span></span></li>
</ul>
<table style="border-collapse: collapse; width: 100%;" border="1">
<tbody>
<tr>
<td style="width: 100%;">
<p style="margin-left: 71.4pt;">///&lt;summary&gt;</p>
<p style="margin-left: 71.4pt;">///<span style="color: #70ad47;">Ekranın herhangi bir alanında fare buttonu &nbsp;basıldığında &ccedil;ağrılacak olan method</span></p>
<p style="margin-left: 71.4pt;">///&lt;/summary&gt;</p>
<p style="margin-left: 71.4pt;"><span style="color: #4472c4;">Private void</span> FrmMain_MouseDown(<span style="color: #4472c4;">object </span>sender, <span style="color: #5b9bd5;">MouseEventArgs </span>e)&hellip;</p>
</td>
</tr>
</tbody>
</table>
<ul style="list-style-type: disc;">
<li><span style="font-size: 10pt; font-family: 'times new roman', times;"><span style="font-size: 10pt; font-family: 'times new roman', times;">Sağ tuşa basılmaya başlandığı ilk noktayı yakalamak i&ccedil;in kullanılır.</span></span>
<table style="border-collapse: collapse; width: 100%;" border="1">
<tbody>
<tr>
<td style="width: 100%;">
<p style="margin-left: 53.4pt;">///&lt;summary&gt;</p>
<p style="margin-left: 53.4pt;">///<span style="color: #70ad47;">Ekranın herhangi bir alanında fare hareket ettirildiğinde &ccedil;ağrılacak olan method</span></p>
<p style="margin-left: 53.4pt;">///&lt;/summary&gt;</p>
<p style="margin-left: 53.4pt;"><span style="color: #4472c4;">Private void</span> FrmMain_MouseMove(<span style="color: #4472c4;">object </span>sender,</p>
<p style="margin-left: 53.4pt;"><span style="color: #5b9bd5;">MouseEventArgs </span>e)&hellip;</p>
</td>
</tr>
</tbody>
</table>
</li>
<li><span style="font-size: 10pt; font-family: 'times new roman', times;">Sağ tuşa basılı halde fare hareket ettirildiğinde ekranda alan belirlenmesine yardımcı olur.</span></li>
</ul>
