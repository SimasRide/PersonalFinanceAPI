import Logo from './Logo'

function Footer() {
  return (
    <footer className="footer">
      <div className="footer-inner">
        <div className="footer-grid">
          <div className="footer-brand">
            <a className="brand" href="#top">
              <Logo />
              <span>GREAT BANK</span>
            </a>
            <p>A Portuguese banking technology company, headquartered in Lisbon.</p>
          </div>

          <div className="footer-links">
            <div className="footer-col">
              <span>Product</span>
              <a href="#analytics">Analytics</a>
              <a href="#vaults">Vaults</a>
            </div>
            <div className="footer-col">
              <span>Company</span>
              <a href="#about">About</a>
              <a href="#security">Security</a>
            </div>
          </div>
        </div>

        <div className="footer-bottom">
          <span>© 2026 Great Bank. Lisbon, Portugal.</span>
          <span>NIS2 &amp; DORA compliant</span>
        </div>
      </div>
    </footer>
  )
}

export default Footer
