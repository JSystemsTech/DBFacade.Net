// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See LICENSE file in the project root for full license information.

$(function () {
    var oldWindowRefresh = window.refresh;
    waitForNavbar();

    window.refresh = function (article) {
        oldWindowRefresh(article);
        waitForNavbar();
    }
    
    function loadVersionsTest() {
        const currentVersion = 'v1.0.13';
        const form = document.querySelector('.navbar-form');
        var currentURL = new URL(window.location.href);
        var versions = [
            {
                "version": "v2.0.0",
                "latestBuild": "2.0.0",
                "url": "",
                "latest": true,
                "divider": true,
                "header": "v2.0 Releases",
                "message": null,
                "messageTheme": "info"
            },
            {
                "version": "v1.0.13",
                "latestBuild": "2.0.0",
                "url": "/docsv1/index.html",
                "latest": false,
                "divider": false,
                "header": "Previous Releases",
                "message": "A new version of DbFacade is available",
                "messageTheme": "info"
            }
        ];
        try {
            const versionsDropdownMenu = document.createElement('ul');
            versionsDropdownMenu.className = 'dropdown-menu';
            versionsDropdownMenu.setAttribute('aria-labelledby', 'versionsDropdownMenu');


            let currentInfo;
            versions.forEach(function (versionInfo) {

                if (currentVersion === versionInfo.version) {
                    currentInfo = versionInfo;
                }
                if (versionInfo.header !== null) {
                    var header = document.createElement('li');
                    header.className = 'dropdown-header';
                    header.textContent = versionInfo.header;
                    versionsDropdownMenu.append(header);
                }


                var listItem = document.createElement('li');
                var link = document.createElement('a');
                var url = `${currentURL.origin}${versionInfo.url}`;
                link.setAttribute('href', url);
                link.textContent = versionInfo.latest ? `${versionInfo.version} (latest)` : versionInfo.version;
                listItem.append(link);
                versionsDropdownMenu.append(listItem);
                if (versionInfo.divider) {
                    var divider = document.createElement('li');
                    divider.setAttribute('role', 'separator');
                    divider.className = 'divider';
                    versionsDropdownMenu.append(divider);
                }
            });

            const versionsDropdown = document.createElement('div');
            versionsDropdown.className = 'dropdown';

            const versionsDropdownToggle = document.createElement('button');
            versionsDropdownToggle.className = 'btn btn-link dropdown-toggle';
            versionsDropdownToggle.setAttribute('type', 'button');
            versionsDropdownToggle.setAttribute('id', 'versionsDropdownMenu');
            versionsDropdownToggle.setAttribute('data-toggle', 'dropdown');
            versionsDropdownToggle.setAttribute('aria-haspopup', 'true');
            versionsDropdownToggle.setAttribute('aria-expanded', 'false');

            const versionsDropdownToggleText = document.createElement('span');
            var btnText = currentInfo.latest ? `${currentInfo.version} (latest)` : currentInfo.version;
            versionsDropdownToggleText.textContent = btnText;
            versionsDropdownToggle.append(versionsDropdownToggleText);
            const versionsDropdownToggleCaret = document.createElement('span');
            versionsDropdownToggleCaret.className = 'caret';
            versionsDropdownToggle.append(versionsDropdownToggleCaret);

            versionsDropdown.append(versionsDropdownToggle);
            versionsDropdown.append(versionsDropdownMenu);

            const formGroup = document.createElement('div');
            formGroup.className = 'form-group';
            formGroup.append(versionsDropdown);
            form.prepend(formGroup);

            const groups = form.querySelectorAll('.form-group');
            groups.forEach((group) => {
                group.classList.add('reset-width');
            });
            if (typeof currentInfo.message === 'string') {
                addNavbarMessage(currentInfo.message);
            }
            
        } catch (err) {
            console.log('Failed to add version navigation:', err);
        }
    }
    function addNavbarMessage(message) {
        var mainNav = document.getElementById('autocollapse');
        if (mainNav) {
            const newVersionNav = document.createElement('nav');
            newVersionNav.className = 'navbar bg-primary';
            newVersionNav.style.margin = '0';
            //navbar navbar-default
            const newVersionNavContainer = document.createElement('div');
            newVersionNavContainer.className = 'container-fluid';
            newVersionNavContainer.style.textAlign = 'center';

            const newVersionNavText = document.createElement('p');
            newVersionNavText.className = 'navbar-text';
            newVersionNavText.textContent = message;
            newVersionNavText.style.width = '100%';

            newVersionNavContainer.append(newVersionNavText);
            newVersionNav.append(newVersionNavContainer);
            mainNav.after(newVersionNav);
        }
    }
    function getJson(name, cb, logError) {
        logError = logError === true;
        // Build the dynamic URL for languages.json
        var currentURL = new URL(window.location.href);
        var jsonUrl = `${currentURL.origin}/public/data/${name}.json`;
        // Fetching the JSON from the URL
        fetch(jsonUrl)
            .then(response => {
                if (!response.ok && logError) {
                    console.error(`Failed to fetch ${name}.json`);
                    cb(null);
                }
                response.json().then(cb);
            }).catch(error => {
                if (logError) {
                    console.error(`Error fetching ${name}.json: ${error}`);
                }
                cb(null);
            });
    }
    function waitForNavbar() {
        // Select the target node to observe for changes
        const targetNode = document.getElementById("navbar");

        // If the target node is not found, display an error and exit
        if (!targetNode) {
            console.log('Navbar element not found');
            return;
        }

        // Callback function to execute when the desired element is injected
        const callback = async (mutationsList, observer) => {
            for (const mutation of mutationsList) {
                if (mutation.type === 'childList') {
                    const navElement = document.querySelector('.navbar-nav');
                    if (navElement) {

                        loadVersionsTest();

                        // Disconnect the observer once the element is found
                        observer.disconnect();

                        return;
                    }
                }
            }
        };

        // Create an observer instance with the callback function
        const observer = new MutationObserver(callback);

        // Options for the observer (which mutations to observe)
        const config = { childList: true, subtree: true };

        // Start observing the target node for configured mutations
        observer.observe(targetNode, config);
    }
});