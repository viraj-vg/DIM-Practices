// FinPulse Fintech Job Aggregator Frontend Logic

document.addEventListener('DOMContentLoaded', () => {
  // State
  const state = {
    keyword: '',
    location: '',
    category: 'All',
    experienceLevel: 'All',
    locationType: 'All',
    techSkill: '',
    minSalary: 0,
    hasEquity: false,
    sourcePortal: 'All',
    sortBy: 'newest',
    page: 1,
    pageSize: 12,
    savedJobIds: JSON.parse(localStorage.getItem('finpulse_saved_jobs') || '[]'),
    showingSavedOnly: false,
    currentJobs: [],
    selectedJob: null
  };

  // DOM Elements
  const inputKeyword = document.getElementById('input-keyword');
  const inputLocation = document.getElementById('input-location');
  const btnSubmitSearch = document.getElementById('btn-submit-search');
  const categoryPills = document.querySelectorAll('#category-taxonomy-bar .cat-pill');
  const locationPills = document.querySelectorAll('#filter-location-types .sub-pill');
  const selectExpLevel = document.getElementById('select-exp-level');
  const rangeSalary = document.getElementById('range-salary');
  const salarySliderVal = document.getElementById('salary-slider-val');
  const skillPills = document.querySelectorAll('#popular-skills-pills .sub-pill');
  const checkHasEquity = document.getElementById('check-has-equity');
  const selectPortal = document.getElementById('select-portal');
  const selectSortBy = document.getElementById('select-sort-by');
  const btnResetFilters = document.getElementById('btn-reset-filters');
  const jobCardsContainer = document.getElementById('job-cards-container');
  const paginationContainer = document.getElementById('pagination-container');
  const resultsHeadline = document.getElementById('results-headline');
  const resultsSubtext = document.getElementById('results-subtext');
  const savedCountBadge = document.getElementById('saved-count-badge');
  const btnSavedJobs = document.getElementById('btn-saved-jobs');
  const btnToggleView = document.getElementById('btn-toggle-view');
  const viewToggleText = document.getElementById('view-toggle-text');
  const portalMainView = document.getElementById('portal-main-view');
  const analyticsView = document.getElementById('analytics-view');
  const btnRefreshCache = document.getElementById('btn-refresh-cache');

  // Modals
  const jobDetailModal = document.getElementById('job-detail-modal');
  const btnCloseDetailModal = document.getElementById('btn-close-detail-modal');
  const alertSubscriptionModal = document.getElementById('alert-subscription-modal');
  const btnOpenAlertModal = document.getElementById('btn-open-alert-modal');
  const btnCloseAlertModal = document.getElementById('btn-close-alert-modal');
  const formJobAlert = document.getElementById('form-job-alert');

  // Stats DOM
  const statTotalJobs = document.getElementById('stat-total-jobs');
  const statAvgSalary = document.getElementById('stat-avg-salary');
  const statMaxSalary = document.getElementById('stat-max-salary');
  const statRemotePct = document.getElementById('stat-remote-pct');

  // Update Saved Count Badge
  function updateSavedBadge() {
    savedCountBadge.textContent = state.savedJobIds.length;
  }
  updateSavedBadge();

  // Show Toast Notification
  function showToast(message, icon = 'fa-circle-check') {
    const container = document.getElementById('toast-container');
    const toast = document.createElement('div');
    toast.className = 'toast';
    toast.innerHTML = `<i class="fa-solid ${icon}" style="color: var(--accent-green);"></i> <span>${message}</span>`;
    container.appendChild(toast);
    setTimeout(() => {
      toast.style.opacity = '0';
      toast.style.transition = 'opacity 0.4s ease';
      setTimeout(() => toast.remove(), 400);
    }, 3500);
  }

  // Format Currency
  function formatMoney(amount, currency = 'USD') {
    if (!amount) return '$0';
    const sym = currency === 'GBP' ? '£' : currency === 'EUR' ? '€' : '$';
    return `${sym}${Math.round(amount / 1000)}k`;
  }

  // Format Full Currency Range
  function formatSalaryRange(min, max, currency = 'USD') {
    const sym = currency === 'GBP' ? '£' : currency === 'EUR' ? '€' : '$';
    return `${sym}${Math.round(min).toLocaleString()} - ${sym}${Math.round(max).toLocaleString()}`;
  }

  // Get Initials for Avatar
  function getInitials(name) {
    if (!name) return 'FP';
    const parts = name.split(' ');
    if (parts.length >= 2) {
      return (parts[0][0] + parts[1][0]).toUpperCase();
    }
    return name.substring(0, 2).toUpperCase();
  }

  // Fetch Jobs from Backend API
  async function fetchJobs() {
    jobCardsContainer.innerHTML = `
      <div style="text-align: center; padding: 4rem 1rem; color: var(--text-muted);">
        <i class="fa-solid fa-spinner fa-spin fa-2x" style="color: var(--accent-green); margin-bottom: 1rem;"></i>
        <p>Aggregating real-time fintech job postings across global providers...</p>
      </div>
    `;

    try {
      const params = new URLSearchParams();
      if (state.keyword) params.append('keyword', state.keyword);
      if (state.location) params.append('location', state.location);
      if (state.category && state.category !== 'All') params.append('category', state.category);
      if (state.experienceLevel && state.experienceLevel !== 'All') params.append('experienceLevel', state.experienceLevel);
      if (state.locationType && state.locationType !== 'All') params.append('locationType', state.locationType);
      if (state.techSkill) params.append('techSkill', state.techSkill);
      if (state.minSalary > 0) params.append('minSalary', state.minSalary);
      if (state.hasEquity) params.append('hasEquity', 'true');
      if (state.sourcePortal && state.sourcePortal !== 'All') params.append('sourcePortal', state.sourcePortal);
      if (state.sortBy) params.append('sortBy', state.sortBy);
      params.append('page', state.page);
      params.append('pageSize', state.pageSize);

      const response = await fetch(`/api/jobs?${params.toString()}`);
      if (!response.ok) throw new Error('Network error fetching jobs');

      const data = await response.json();
      state.currentJobs = data.jobs || [];

      // If showing saved only, filter locally
      let displayJobs = state.currentJobs;
      if (state.showingSavedOnly) {
        displayJobs = state.currentJobs.filter(j => state.savedJobIds.includes(j.id));
        resultsHeadline.textContent = `Saved Bookmarked Roles (${displayJobs.length})`;
        resultsSubtext.textContent = 'Showing roles you have bookmarked for review';
      } else {
        resultsHeadline.textContent = `${data.totalCount} Fintech Opportunities Found`;
        resultsSubtext.textContent = `Matching aggregated openings across ${Object.keys(data.sourcePortalCounts || {}).length || 6} financial tech portals`;
      }

      renderJobCards(displayJobs);
      renderPagination(data.totalCount, data.page, data.pageSize);
    } catch (err) {
      console.error(err);
      jobCardsContainer.innerHTML = `
        <div style="text-align: center; padding: 4rem 1rem; color: #f87171;">
          <i class="fa-solid fa-triangle-exclamation fa-2x" style="margin-bottom: 1rem;"></i>
          <p>Failed to load aggregated job listings. Please ensure the backend service is running.</p>
        </div>
      `;
    }
  }

  // Render Job Cards
  function renderJobCards(jobs) {
    if (!jobs || jobs.length === 0) {
      jobCardsContainer.innerHTML = `
        <div style="text-align: center; padding: 4rem 1rem; background: var(--bg-card); border-radius: var(--radius-lg); border: 1px dashed var(--border-glass);">
          <i class="fa-solid fa-folder-open fa-2x" style="color: var(--text-muted); margin-bottom: 1rem;"></i>
          <h3 style="margin-bottom: 0.5rem;">No Fintech Roles Found</h3>
          <p style="color: var(--text-muted); font-size: 0.9rem; max-width: 400px; margin: 0 auto 1.5rem auto;">Try broadening your search keywords, lowering the salary threshold, or clearing sector filters.</p>
          <button class="nav-btn" onclick="document.getElementById('btn-reset-filters').click()">Reset All Filters</button>
        </div>
      `;
      return;
    }

    jobCardsContainer.innerHTML = jobs.map(job => {
      const isSaved = state.savedJobIds.includes(job.id);
      const initials = getInitials(job.company?.name);
      const techTags = (job.techStack || []).slice(0, 4).map(tag => 
        `<span class="tech-tag" onclick="event.stopPropagation(); filterByTag('${tag}')">${tag}</span>`
      ).join('');

      return `
        <article class="job-card ${job.isFeatured ? 'featured-card' : ''}" onclick="openJobDetail('${job.id}')">
          <div class="card-top">
            <div class="company-meta">
              <div class="company-avatar">${initials}</div>
              <div class="company-text">
                <h4>${job.company?.name || 'Fintech Firm'} ${job.company?.glassdoorRating ? `<span class="rating">★ ${job.company.glassdoorRating.toFixed(1)}</span>` : ''}</h4>
                <span class="sector-tag">${job.company?.sector || 'Financial Technology'}</span>
              </div>
            </div>

            <div class="card-badges">
              ${job.isHotRole ? '<span class="hot-badge"><i class="fa-solid fa-fire"></i> Hot Role</span>' : ''}
              <button class="bookmark-btn ${isSaved ? 'saved' : ''}" onclick="event.stopPropagation(); toggleBookmark('${job.id}')" title="Save Job">
                <i class="fa-${isSaved ? 'solid' : 'regular'} fa-bookmark"></i>
              </button>
            </div>
          </div>

          <div class="card-body">
            <h3>${job.title}</h3>
            <p class="card-description">${job.description}</p>
            
            <div class="job-meta-row">
              <span class="meta-item salary-pill">
                <i class="fa-solid fa-wallet"></i> ${formatSalaryRange(job.minSalary, job.maxSalary, job.currency)} / yr
              </span>
              <span class="meta-item ${job.locationType === 1 ? 'remote-pill' : ''}">
                <i class="fa-solid fa-location-dot"></i> ${job.location}
              </span>
              ${job.hasEquity ? '<span class="meta-item" style="color: var(--accent-cyan); font-weight:600;"><i class="fa-solid fa-chart-pie"></i> Equity Grant</span>' : ''}
              ${job.hasBonus ? '<span class="meta-item" style="color: var(--accent-gold); font-weight:600;"><i class="fa-solid fa-coins"></i> Bonus Pool</span>' : ''}
            </div>

            <div class="tech-tag-row">
              ${techTags}
            </div>
          </div>

          <div class="card-footer">
            <span class="portal-source">
              <i class="fa-solid fa-link"></i> ${job.sourcePortal || 'Direct Firm Careers'}
            </span>
            <div style="display: flex; gap: 0.75rem; align-items: center;">
              <span><i class="fa-solid fa-user-group"></i> ${job.applicantCount || 20}+ applicants</span>
              <button class="action-btn-sm" onclick="event.stopPropagation(); openJobDetail('${job.id}')">View & Apply</button>
            </div>
          </div>
        </article>
      `;
    }).join('');
  }

  // Render Pagination
  function renderPagination(totalCount, currentPage, pageSize) {
    const totalPages = Math.ceil(totalCount / pageSize);
    if (totalPages <= 1) {
      paginationContainer.innerHTML = '';
      return;
    }

    let html = '';
    if (currentPage > 1) {
      html += `<button class="page-btn" onclick="goToPage(${currentPage - 1})"><i class="fa-solid fa-chevron-left"></i></button>`;
    }

    for (let i = 1; i <= totalPages; i++) {
      if (i === 1 || i === totalPages || (i >= currentPage - 1 && i <= currentPage + 1)) {
        html += `<button class="page-btn ${i === currentPage ? 'active' : ''}" onclick="goToPage(${i})">${i}</button>`;
      } else if (i === currentPage - 2 || i === currentPage + 2) {
        html += `<span style="color: var(--text-muted); padding: 0 0.5rem;">...</span>`;
      }
    }

    if (currentPage < totalPages) {
      html += `<button class="page-btn" onclick="goToPage(${currentPage + 1})"><i class="fa-solid fa-chevron-right"></i></button>`;
    }

    paginationContainer.innerHTML = html;
  }

  // Global Helpers for Inline Event Handlers
  window.goToPage = (p) => {
    state.page = p;
    fetchJobs();
    window.scrollTo({ top: 400, behavior: 'smooth' });
  };

  window.filterByTag = (tag) => {
    state.keyword = tag;
    inputKeyword.value = tag;
    state.page = 1;
    fetchJobs();
  };

  window.toggleBookmark = (jobId) => {
    const index = state.savedJobIds.indexOf(jobId);
    if (index > -1) {
      state.savedJobIds.splice(index, 1);
      showToast('Removed from saved bookmarks', 'fa-bookmark');
    } else {
      state.savedJobIds.push(jobId);
      showToast('Job saved to your bookmarks!', 'fa-bookmark');
    }
    localStorage.setItem('finpulse_saved_jobs', JSON.stringify(state.savedJobIds));
    updateSavedBadge();
    fetchJobs();
  };

  window.openJobDetail = async (jobId) => {
    try {
      const res = await fetch(`/api/jobs/${jobId}`);
      if (!res.ok) throw new Error('Could not load job details');
      const job = await res.json();
      state.selectedJob = job;

      document.getElementById('modal-company-avatar').textContent = getInitials(job.company?.name);
      document.getElementById('modal-job-title').textContent = job.title;
      document.getElementById('modal-company-name').textContent = `${job.company?.name} • ${job.company?.sector || 'Fintech'} • Glassdoor ★${job.company?.glassdoorRating || 4.5}`;
      document.getElementById('modal-salary-range').textContent = `${formatSalaryRange(job.minSalary, job.maxSalary, job.currency)} / yr`;
      document.getElementById('modal-location-badge').textContent = `${job.location}`;
      document.getElementById('modal-bonus-badge').textContent = job.bonusEstimate || 'Performance Bonus Pool';
      document.getElementById('modal-job-description').textContent = job.description;
      document.getElementById('modal-company-overview').textContent = job.company?.overview || 'Leading innovator in financial technology.';

      // Tech Stack
      document.getElementById('modal-tech-stack').innerHTML = (job.techStack || []).map(t => 
        `<span class="tech-tag" style="font-size: 0.85rem; padding: 0.3rem 0.75rem;">${t}</span>`
      ).join('');

      // Responsibilities
      document.getElementById('modal-responsibilities').innerHTML = (job.keyResponsibilities || []).map(r => 
        `<li>${r}</li>`
      ).join('');

      // Qualifications
      document.getElementById('modal-qualifications').innerHTML = (job.qualifications || []).map(q => 
        `<li>${q}</li>`
      ).join('');

      // Benefits
      document.getElementById('modal-benefits').innerHTML = (job.benefits || [
        'Top of market competitive compensation & equity package',
        'Comprehensive healthcare, dental, and optical insurance',
        'Generous retirement 401(k) / pension matching',
        'Continuous professional learning & conference budget'
      ]).map(b => `<li>${b}</li>`).join('');

      // Apply Link
      const applyBtn = document.getElementById('modal-btn-apply');
      applyBtn.href = job.externalUrl || '#';
      document.getElementById('modal-source-portal').innerHTML = `<i class="fa-solid fa-arrow-up-right-from-square"></i> Aggregated from ${job.sourcePortal}`;

      // Bookmark Button inside modal
      const isSaved = state.savedJobIds.includes(job.id);
      const modalBookmarkBtn = document.getElementById('modal-btn-bookmark');
      modalBookmarkBtn.innerHTML = `<i class="fa-${isSaved ? 'solid' : 'regular'} fa-bookmark"></i> ${isSaved ? 'Bookmarked' : 'Save Job'}`;
      modalBookmarkBtn.onclick = () => {
        window.toggleBookmark(job.id);
        const nowSaved = state.savedJobIds.includes(job.id);
        modalBookmarkBtn.innerHTML = `<i class="fa-${nowSaved ? 'solid' : 'regular'} fa-bookmark"></i> ${nowSaved ? 'Bookmarked' : 'Save Job'}`;
      };

      jobDetailModal.classList.add('open');
    } catch (err) {
      console.error(err);
      showToast('Unable to load job details', 'fa-triangle-exclamation');
    }
  };

  // Fetch Market Analytics Intelligence
  async function fetchAnalytics() {
    try {
      const res = await fetch('/api/analytics');
      if (!res.ok) return;
      const data = await res.json();

      // Top Quick Stats
      statTotalJobs.textContent = data.totalOpenings || '--';
      statAvgSalary.textContent = formatMoney(data.averageSalary) + '/yr';
      statMaxSalary.textContent = formatMoney(data.highestSalary) + '/yr';
      statRemotePct.textContent = `${data.remotePercentage}%`;

      // Sector Salaries Bar Chart
      const maxAvgSalary = Math.max(...(data.categorySalaries || []).map(c => c.avgMaxSalary), 500000);
      document.getElementById('analytics-sector-salaries').innerHTML = (data.categorySalaries || []).map(cat => {
        const pct = Math.round((cat.avgMaxSalary / maxAvgSalary) * 100);
        return `
          <div class="salary-bar-item">
            <div class="salary-bar-header">
              <span style="font-weight: 600;">${cat.categoryName} (${cat.openingsCount} jobs)</span>
              <span style="font-family: var(--font-mono); color: var(--accent-green);">$${Math.round(cat.avgMinSalary / 1000)}k - $${Math.round(cat.avgMaxSalary / 1000)}k</span>
            </div>
            <div class="salary-bar-bg">
              <div class="salary-bar-fill" style="width: ${pct}%;"></div>
            </div>
          </div>
        `;
      }).join('');

      // Top Skills
      document.getElementById('analytics-top-skills').innerHTML = `
        <div style="display: flex; flex-wrap: wrap; gap: 0.6rem;">
          ${(data.topSkills || []).map(skill => `
            <div style="background: rgba(255,255,255,0.04); border: 1px solid var(--border-glass); border-radius: var(--radius-sm); padding: 0.6rem 0.9rem; display: flex; justify-content: space-between; align-items: center; width: calc(50% - 0.3rem); cursor: pointer;" onclick="filterByTag('${skill.skill}')">
              <strong style="font-family: var(--font-mono); color: #fff;">${skill.skill}</strong>
              <span style="font-size: 0.8rem; color: var(--accent-cyan);">${skill.jobCount} roles</span>
            </div>
          `).join('')}
        </div>
      `;

      // Hiring Hubs
      document.getElementById('analytics-hiring-hubs').innerHTML = `
        <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 0.75rem;">
          ${(data.topHiringHubs || []).map(hub => `
            <div style="background: rgba(255,255,255,0.03); border: 1px solid var(--border-glass); border-radius: var(--radius-sm); padding: 0.75rem; display: flex; align-items: center; gap: 0.6rem;">
              <span style="font-size: 1.3rem;">${hub.flagEmoji}</span>
              <div>
                <strong style="display: block; font-size: 0.9rem;">${hub.hub}</strong>
                <span style="font-size: 0.78rem; color: var(--text-muted);">${hub.openingsCount} Openings</span>
              </div>
            </div>
          `).join('')}
        </div>
      `;

      // Top Companies
      document.getElementById('analytics-top-companies').innerHTML = `
        <div style="display: flex; flex-direction: column; gap: 0.6rem;">
          ${(data.topHiringCompanies || []).map(comp => `
            <div style="background: rgba(255,255,255,0.03); border: 1px solid var(--border-glass); border-radius: var(--radius-sm); padding: 0.75rem 1rem; display: flex; justify-content: space-between; align-items: center;">
              <div>
                <strong style="color: var(--text-primary); font-size: 0.95rem;">${comp.companyName}</strong>
                <span style="display: block; font-size: 0.8rem; color: var(--text-muted);">${comp.sector}</span>
              </div>
              <div style="text-align: right;">
                <span style="color: var(--accent-gold); font-size: 0.85rem; font-weight: 600;">★ ${comp.rating.toFixed(1)}</span>
                <span style="display: block; font-size: 0.8rem; color: var(--accent-green); font-weight: 600;">${comp.openingsCount} active roles</span>
              </div>
            </div>
          `).join('')}
        </div>
      `;
    } catch (e) {
      console.error('Failed to load market analytics', e);
    }
  }

  // Event Listeners
  btnSubmitSearch.addEventListener('click', () => {
    state.keyword = inputKeyword.value.trim();
    state.location = inputLocation.value.trim();
    state.page = 1;
    fetchJobs();
  });

  inputKeyword.addEventListener('keydown', (e) => {
    if (e.key === 'Enter') btnSubmitSearch.click();
  });

  inputLocation.addEventListener('keydown', (e) => {
    if (e.key === 'Enter') btnSubmitSearch.click();
  });

  // Category taxonomy bar clicks
  categoryPills.forEach(pill => {
    pill.addEventListener('click', () => {
      categoryPills.forEach(p => p.classList.remove('active'));
      pill.classList.add('active');
      state.category = pill.getAttribute('data-cat');
      state.page = 1;
      fetchJobs();
    });
  });

  // Location type pills
  locationPills.forEach(pill => {
    pill.addEventListener('click', () => {
      locationPills.forEach(p => p.classList.remove('active'));
      pill.classList.add('active');
      state.locationType = pill.getAttribute('data-val');
      state.page = 1;
      fetchJobs();
    });
  });

  // Experience level
  selectExpLevel.addEventListener('change', () => {
    state.experienceLevel = selectExpLevel.value;
    state.page = 1;
    fetchJobs();
  });

  // Salary range slider
  rangeSalary.addEventListener('input', () => {
    const val = parseInt(rangeSalary.value);
    state.minSalary = val;
    salarySliderVal.textContent = val > 0 ? `$${Math.round(val / 1000)}k+` : 'Any';
  });

  rangeSalary.addEventListener('change', () => {
    state.page = 1;
    fetchJobs();
  });

  // Popular skills pills
  skillPills.forEach(pill => {
    pill.addEventListener('click', () => {
      const skill = pill.getAttribute('data-skill');
      if (pill.classList.contains('active')) {
        pill.classList.remove('active');
        state.techSkill = '';
      } else {
        skillPills.forEach(p => p.classList.remove('active'));
        pill.classList.add('active');
        state.techSkill = skill;
      }
      state.page = 1;
      fetchJobs();
    });
  });

  // Equity checkbox
  checkHasEquity.addEventListener('change', () => {
    state.hasEquity = checkHasEquity.checked;
    state.page = 1;
    fetchJobs();
  });

  // Source portal selector
  selectPortal.addEventListener('change', () => {
    state.sourcePortal = selectPortal.value;
    state.page = 1;
    fetchJobs();
  });

  // Sort By
  selectSortBy.addEventListener('change', () => {
    state.sortBy = selectSortBy.value;
    state.page = 1;
    fetchJobs();
  });

  // Reset Filters
  btnResetFilters.addEventListener('click', () => {
    state.keyword = '';
    state.location = '';
    state.category = 'All';
    state.experienceLevel = 'All';
    state.locationType = 'All';
    state.techSkill = '';
    state.minSalary = 0;
    state.hasEquity = false;
    state.sourcePortal = 'All';
    state.sortBy = 'newest';
    state.page = 1;
    state.showingSavedOnly = false;

    inputKeyword.value = '';
    inputLocation.value = '';
    categoryPills.forEach(p => p.classList.toggle('active', p.getAttribute('data-cat') === 'All'));
    locationPills.forEach(p => p.classList.toggle('active', p.getAttribute('data-val') === 'All'));
    skillPills.forEach(p => p.classList.remove('active'));
    selectExpLevel.value = 'All';
    selectPortal.value = 'All';
    selectSortBy.value = 'newest';
    rangeSalary.value = 0;
    salarySliderVal.textContent = '$0k+';
    checkHasEquity.checked = false;

    showToast('Filters reset to default', 'fa-rotate-left');
    fetchJobs();
  });

  // Saved Jobs Toggle
  btnSavedJobs.addEventListener('click', () => {
    state.showingSavedOnly = !state.showingSavedOnly;
    btnSavedJobs.classList.toggle('btn-primary', state.showingSavedOnly);
    fetchJobs();
  });

  // Toggle View between Jobs and Market Insights
  btnToggleView.addEventListener('click', () => {
    const isAnalyticsActive = analyticsView.classList.contains('active');
    if (isAnalyticsActive) {
      analyticsView.classList.remove('active');
      portalMainView.style.display = 'block';
      viewToggleText.textContent = 'Market Insights';
      btnToggleView.classList.remove('btn-primary');
    } else {
      portalMainView.style.display = 'none';
      analyticsView.classList.add('active');
      viewToggleText.textContent = 'Job Listings';
      btnToggleView.classList.add('btn-primary');
      fetchAnalytics();
    }
  });

  // Refresh Aggregator Cache
  btnRefreshCache.addEventListener('click', async () => {
    btnRefreshCache.disabled = true;
    btnRefreshCache.innerHTML = '<i class="fa-solid fa-spinner fa-spin"></i> Syncing...';
    try {
      const res = await fetch('/api/jobs/refresh', { method: 'POST' });
      if (res.ok) {
        showToast('Fintech job cache successfully synced from all 6 providers!', 'fa-cloud-arrow-down');
        fetchJobs();
        fetchAnalytics();
      }
    } catch (e) {
      showToast('Cache refresh failed', 'fa-triangle-exclamation');
    } finally {
      btnRefreshCache.disabled = false;
      btnRefreshCache.innerHTML = '<i class="fa-solid fa-rotate"></i> <span>Sync Openings</span>';
    }
  });

  // Close Modals
  btnCloseDetailModal.addEventListener('click', () => jobDetailModal.classList.remove('open'));
  jobDetailModal.addEventListener('click', (e) => {
    if (e.target === jobDetailModal) jobDetailModal.classList.remove('open');
  });

  btnOpenAlertModal.addEventListener('click', () => alertSubscriptionModal.classList.add('open'));
  btnCloseAlertModal.addEventListener('click', () => alertSubscriptionModal.classList.remove('open'));
  alertSubscriptionModal.addEventListener('click', (e) => {
    if (e.target === alertSubscriptionModal) alertSubscriptionModal.classList.remove('open');
  });

  // Submit Job Alert Form
  formJobAlert.addEventListener('submit', async (e) => {
    e.preventDefault();
    const payload = {
      email: document.getElementById('alert-email').value,
      keyword: document.getElementById('alert-keyword').value || null,
      category: document.getElementById('alert-category').value,
      minSalary: parseFloat(document.getElementById('alert-min-salary').value) || null
    };

    try {
      const res = await fetch('/api/jobs/alerts', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      if (res.ok) {
        showToast('Alert subscribed! You will receive daily matching fintech jobs.', 'fa-envelope-circle-check');
        alertSubscriptionModal.classList.remove('open');
        formJobAlert.reset();
      }
    } catch (err) {
      showToast('Error subscribing to alert', 'fa-triangle-exclamation');
    }
  });

  // Initial Load
  fetchJobs();
  fetchAnalytics();
});
