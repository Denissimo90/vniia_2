
sessionStorage.setItem("return_url", document.querySelector("meta[http-equiv=return]").getAttribute("data-return"));

window.location.href = document.querySelector("meta[http-equiv=redirect]").getAttribute("data-redirect");