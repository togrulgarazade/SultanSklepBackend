// Load Header
fetch('../pages/header.html')
.then(response => response.text())
.then(html => {
  document.getElementById('navbar-container').innerHTML = html;
});

// Load Footer
fetch('../pages/footer.html')
.then(response => response.text())
.then(html => {
  document.getElementById('footer-container').innerHTML = html;
});

// Load Preloader
fetch ('../pages/page-preloader.html')
.then(response => response.text())
.then (html =>{
  document.getElementById('preloder-container').innerHTML = html;
});

//Load Hamburger
fetch('../pages/hamburger.html')
.then(response => response.text())
.then(html =>{
  document.getElementById('hamburger-container').innerHTML = html;
});