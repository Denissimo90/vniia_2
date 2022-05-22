
sessionStorage.setItem("access_token", document.querySelector("meta[http-equiv=token]").getAttribute("data-token"));
sessionStorage.setItem("return_url", document.querySelector("meta[http-equiv=return]").getAttribute("data-return"));

window.location.href = document.querySelector("meta[http-equiv=refresh]").getAttribute("data-url");
