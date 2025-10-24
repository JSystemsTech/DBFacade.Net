const app = {
    iconLinks: [
        {
            icon: 'github',
            href: 'https://github.com/JSystemsTech/DBFacade.Net',
            title: 'GitHub'
        },
        {
            icon: 'person-lines-fill',
            href: '/author.html',
            title: 'About the Developer'
        }
    ],
    getJson: async function (name, logError) {
        logError = logError === true;
        // Build the dynamic URL for languages.json
        var currentURL = new URL(window.location.href);
        var jsonUrl = `${currentURL.origin}/public/data/${name}.json`;        
        // Fetching the JSON from the URL
        try {
            var response = await fetch(jsonUrl);

            if (!response.ok && logError) {
                console.error(`Failed to fetch ${name}.json`);

                return null;
            }

            return await response.json();

        } catch (error) {

            if (logError) {
                console.error(`Error fetching ${name}.json: ${error}`);
            }

            return null;
        }
    },
    loadVersionsTest: async function (newLinksList) {
        const currentVersion = 'v2.0.0';
        var currentURL = new URL(window.location.href);
        let versions;
        try {
            versions = await this.getJson('versions');           


            var dropdown = document.createElement('li');
            dropdown.className = 'nav-item dropdown';
            newLinksList.prepend(dropdown);

            var toggle = document.createElement('a');
            toggle.className = 'border-0 dropdown-toggle nav-link pe-auto';
            toggle.setAttribute('data-bs-toggle', 'dropdown');
            toggle.setAttribute('aria-expanded', 'false');
            toggle.setAttribute('title', 'Version Navigation');
            toggle.setAttribute('href', '#');

            var versionsDropdownMenu = document.createElement('ul');
            versionsDropdownMenu.className = 'dropdown-menu dropdown-menu-end';
            dropdown.append(toggle);
            dropdown.append(versionsDropdownMenu);

            let currentInfo;
            versions.forEach(function (versionInfo) {

                if (currentVersion === versionInfo.version) {
                    currentInfo = versionInfo;
                }
                if (versionInfo.header !== null && versionInfo.header.trim() !== '') {
                    var header = document.createElement('li');
                    var h6 = document.createElement('h6');
                    h6.className = 'dropdown-header';
                    h6.textContent = versionInfo.header;
                    header.append(h6);
                    versionsDropdownMenu.append(header);
                }


                var listItem = document.createElement('li');
                var link = document.createElement('a');
                var url = `${currentURL.origin}${versionInfo.url}`;

                link.className = 'dropdown-item';
                link.setAttribute('href', url);
                link.textContent = versionInfo.latest ? `${versionInfo.version} (latest)` : versionInfo.version;
                listItem.append(link);
                versionsDropdownMenu.append(listItem);
                if (versionInfo.divider) {
                    var divider = document.createElement('li');
                    var hr = document.createElement('hr');
                    hr.className = 'dropdown-divider';
                    divider.append(hr);
                    versionsDropdownMenu.append(divider);
                }
            });
            var btnText = currentInfo.latest ? `${currentInfo.version} (latest)` : currentInfo.version;
            toggle.textContent = btnText;
            if (typeof currentInfo.message === 'string' && currentInfo.message != null) {                
                await this.addNavbarMessage(currentInfo.message, currentInfo.messageTheme);
            }            
        } catch (err) {
            console.log('Failed to add version navigation:', err);
        }
    },
    forEachElement: function (selector, cb, el) {
        el = el !== null ? el : document;
        if (typeof cb === 'function') {
            var elements = el.querySelectorAll(selector);
            elements.forEach(cb);
        }
    },
    addNavbarMessage: async function (message, theme) {
        var mainNav = document.getElementById('autocollapse');
        if (mainNav) {
            theme = theme === null || typeof theme !== 'string' || theme.trim() === '' ? 'secondary' : theme;

            var nav = document.createElement('nav');
            nav.classList = `navbar nav-custom bg-${theme} text-bg-${theme}`;

            var container = document.createElement('div');
            container.classList = 'container-fluid text-center';

            var text = document.createElement('span');
            text.classList = 'navbar-text w-100';
            text.textContent = message

            container.append(text);
            nav.append(container);
            mainNav.after(nav);
        }
    },
    fixIconList: async function () {
        var iconForm = document.querySelector('header nav form.icons');
        if (iconForm === null) {
            return;
        }
        var newLinksList = document.createElement('ul');
        newLinksList.className = 'navbar-nav ms-auto navbar-custom';
        var children = Array.from(iconForm.children);
        
        for (var i = 0; i < children.length; i++)
        {
            var element = children[i];
            if (element.tagName === 'A') {
                var link = document.createElement('a');
                link.setAttribute('target', '_blank');
                link.setAttribute('rel', 'noopener');
                link.setAttribute('href', element.getAttribute('href'));
                link.setAttribute('title', element.getAttribute('title'));
                link.className = 'nav-link pe-auto';
                link.innerHTML = element.innerHTML;

                var linkText = document.createElement('small');
                linkText.className = 'd-lg-none ms-2';
                linkText.textContent = element.getAttribute('title');
                link.append(linkText);

                var navItem = document.createElement('li');
                navItem.classList.add('nav-item');
                navItem.append(link);

                newLinksList.append(navItem);
            } else if (element.classList.contains('dropdown')) {
                var toggle = element.querySelector('.dropdown-toggle');
                toggle.classList.remove('btn');
                toggle.classList.add('nav-link');
                toggle.setAttribute('href', '#');

                var toggleText = document.createElement('small');
                toggleText.className = 'd-lg-none ms-2';
                toggleText.textContent = toggle.getAttribute('title');
                toggle.append(toggleText);

                var menu = element.querySelector('.dropdown-menu');

                var dropdown = document.createElement('li');
                dropdown.className = 'nav-item dropdown';
                dropdown.append(toggle);
                dropdown.append(menu);
                newLinksList.append(dropdown);
            }

            
        }
        iconForm.before(newLinksList);
        iconForm.remove();
        await this.loadVersionsTest(newLinksList);
    },
    onDocFxLoad: async function () {
        await this.fixIconList();
    },
    start: function () {

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
                    const navElement = document.querySelector('form.icons');
                    if (navElement) {
                        // Disconnect the observer once the element is found
                        observer.disconnect();

                        await this.onDocFxLoad();
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
};

app.start = app.start.bind(app);

export default app;