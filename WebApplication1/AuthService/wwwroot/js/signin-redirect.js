
sessionStorage.setItem("Oauthtoken", document.querySelector("meta[http-equiv=refresh]").getAttribute("data-token"));

window.location.href = document.querySelector("meta[http-equiv=refresh]").getAttribute("data-url");
